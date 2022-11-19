// ------------------------------------------------------------------------------------------------------
// <copyright file="NearblocksService.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;

using GraphQL;
using Nomis.Blockchain.Abstractions.Converters;
using Nomis.Domain.Scoring.Entities;
using Nomis.Nearblocks.Calculators;
using Nomis.Nearblocks.Interfaces;
using Nomis.Nearblocks.Interfaces.Models;
using Nomis.Nearblocks.Interfaces.Requests;
using Nomis.Nearblocks.Settings;
using Nomis.ScoringService.Interfaces;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Exceptions;
using Nomis.Utils.Wrapper;

namespace Nomis.Nearblocks
{
    /// <inheritdoc cref="INearblocksService"/>
    internal sealed class NearblocksService :
        INearblocksService,
        ISingletonService
    {
        private readonly IScoringService _scoringService;
        private readonly HttpClient _restClient;

        /// <summary>
        /// Initialize <see cref="NearblocksService"/>.
        /// </summary>
        /// <param name="nearblocksSettings"><see cref="NearblocksSettings"/>.</param>
        /// <param name="client"><see cref="INearblocksGraphQLClient"/>.</param>
        /// <param name="scoringService"><see cref="IScoringService"/>.</param>
        public NearblocksService(
            NearblocksSettings nearblocksSettings,
            INearblocksGraphQLClient client,
            IScoringService scoringService)
        {
            _scoringService = scoringService;
            Client = client;
            _restClient = new()
            {
                BaseAddress = new(nearblocksSettings.RestApiBaseUrl ??
                                  throw new ArgumentNullException(nameof(nearblocksSettings), $"{nameof(nearblocksSettings.RestApiBaseUrl)} does not exist."))
            };
        }

        /// <inheritdoc />
        public ulong ChainId => 111116;

        /// <inheritdoc/>
        public INearblocksGraphQLClient Client { get; }

        /// <inheritdoc/>
        public async Task<Result<NearWalletScore>> GetWalletStatsAsync(string address)
        {
            string? balanceString = (await GetBalanceAsync(address)).Balance?.Replace(",", string.Empty);
            decimal.TryParse(balanceString, NumberStyles.AllowDecimalPoint, new NumberFormatInfo { NumberDecimalSeparator = "." }, out decimal balance);
            decimal usdBalance = await GetUsdBalanceAsync(balance);
            var transactions = (await GetNearblocksTransactionsAsync(new()
            {
                Address = address,
                Limit = 1000,
                Offset = 0
            })).Data;
            var fungibleTokens = (await GetNearblocksFungibleTokensAsync(new(address))).Data;
            var tokens = (await GetNearblocksNonFungibleTokenEventsAsync(new(address))).Data;

            var walletStats = new NearStatCalculator(
                    address,
                    balance,
                    usdBalance,
                    transactions,
                    fungibleTokens,
                    tokens)
                .GetStats();

            double score = walletStats.GetScore();
            var scoringData = new ScoringData(address, address, ChainId, score, JsonSerializer.Serialize(walletStats));
            await _scoringService.SaveScoringDataToDatabaseAsync(scoringData);

            return await Result<NearWalletScore>.SuccessAsync(
                new()
                {
                    Address = address,
                    Stats = walletStats,
                    Score = score
                }, "Got Near wallet score.");
        }

        private async Task<List<TResult>> GetDataAsync<TResult>(GraphQLRequest query, string responseAlias)
        {
            var response = await Client.SendQueryAsync<JsonObject>(query);
            var data = JsonSerializer.Deserialize<List<TResult>>(response.Data[responseAlias]?.ToJsonString(new()) !, new JsonSerializerOptions
            {
                Converters = { new BigIntegerConverter() }
            }) ?? new List<TResult>();

            return data;
        }

        private async Task<Result<List<NearblocksTransaction>>> GetNearblocksTransactionsAsync(
            GetNearblocksTransactionsRequest request)
        {
            var query = new GraphQLRequest
            {
                Query = @"
                    query ($address: String, $limit: Int, $offset: Int, $order: [transactions_order_by!]) {
                      transactions(
                        limit: $limit
                        offset: $offset
                        order_by: $order
                        where: {_and: [{receipts: {_or: [{predecessor_account_id: {_eq: $address}}, {receiver_account_id: {_eq: $address}}]}}]}
                      ) {
                        ...transactionsFull
                        block {
                          ...blocks
                        }
                        receipts(order_by: {included_in_block_timestamp: asc}) {
                          ...receiptsFull
                        }
                      }
                    }

                    fragment blocks on blocks {
                      block_height
                      block_hash
                      block_timestamp
                      author_account_id
                    }

                    fragment receiptsFull on receipts {
                      execution_outcome {
                        status
                      }
                      action_receipt_actions{
                        action_kind
                        args
                      }
                      receipt_id
                      included_in_block_hash
                      included_in_block_timestamp
                      predecessor_account_id
                      receiver_account_id
                      receipt_kind
                      originated_from_transaction_hash
                    }

                    fragment transactionsFull on transactions {
                      transaction_hash
                      included_in_block_hash
                      block_timestamp
                      signer_account_id
                      receiver_account_id
                      status
                      receipt_conversion_tokens_burnt
                    }
                ",
                Variables = request
            };

            var result = new List<NearblocksTransaction>();
            var data = await GetDataAsync<NearblocksTransaction>(query, "transactions");
            result.AddRange(data);
            while (data.Count > 0 && data.Count == request.Limit)
            {
                request = new GetNearblocksTransactionsRequest
                {
                    Address = request.Address,
                    Limit = request.Limit,
                    Offset = request.Offset + request.Limit
                };
                query.Variables = request;
                data = await GetDataAsync<NearblocksTransaction>(query, "transactions");
                result.AddRange(data);
            }

            return await Result<List<NearblocksTransaction>>.SuccessAsync(result, "Transactions received.");
        }

        private async Task<Result<List<NearblocksFungibleToken>>> GetNearblocksFungibleTokensAsync(
            GetNearblocksFungibleTokensRequest request)
        {
            var query = new GraphQLRequest
            {
                Query = @"
                    query ($id: String, $limit: Int, $offset: Int) {
                      ft_inventory(args: {address: $id, take: $limit, skip: $offset}, where: {ft_meta: {}}) {
                        amount
                        contract
                        ft_meta {
                          ...ftMetaFull
                        }
                      }
                    }

                    fragment ftMetaFull on ft_meta {
                      contract
                      name
                      decimals
                      symbol
                      icon
                      spec
                      reference
                      reference_hash
                      price
                      description
                      twitter
                      facebook
                      telegram
                      reddit
                      website
                    }
                ",
                Variables = request
            };

            var result = new List<NearblocksFungibleToken>();
            var data = await GetDataAsync<NearblocksFungibleToken>(query, "ft_inventory");
            result.AddRange(data);
            while (data.Count > 0 && data.Count == request.Limit)
            {
                request = new GetNearblocksFungibleTokensRequest(request.Id, request.Limit, request.Offset + request.Limit);
                query.Variables = request;
                data = await GetDataAsync<NearblocksFungibleToken>(query, "ft_inventory");
                result.AddRange(data);
            }

            return await Result<List<NearblocksFungibleToken>>.SuccessAsync(result, "Fungible tokens received.");
        }

#pragma warning disable IDE0051 // Remove unused private members
#pragma warning disable RCS1213 // Remove unused member declaration.
        private async Task<Result<List<NearblocksFungibleTokenEvent>>> GetNearblocksFungibleTokenEventsAsync(
#pragma warning restore RCS1213 // Remove unused member declaration.
#pragma warning restore IDE0051 // Remove unused private members
            GetNearblocksFungibleTokenEventsRequest request)
        {
            var query = new GraphQLRequest
            {
                Query = @"
            query ($limit: Int, $offset: Int, $id: String) {
              assets__fungible_token_events(
                limit: $limit
                offset: $offset
                order_by: {emitted_at_block_timestamp: desc}
                where: {_or: [{emitted_by_contract_account_id: {_eq: $id}}, {token_new_owner_account_id: {_eq: $id}}, {token_old_owner_account_id: {_eq: $id}}]}
              ) {
                amount
                emitted_for_receipt_id
                emitted_at_block_timestamp
                emitted_by_contract_account_id
                event_kind
                token_old_owner_account_id
                token_new_owner_account_id
                receipt {
                  execution_outcome {
                    status
                  }
                  action_receipt_actions{
                    action_kind
                    args
                  }
                  transaction {
                    transaction_hash
                    included_in_block_hash
                    block_timestamp
                    signer_account_id
                    receiver_account_id
                    status
                    block {
                      block_height
                      block_hash
                      block_timestamp
                      author_account_id
                    }
                  }
                }
                ft_meta {
                  ...ftMetaFull
                }
              }
            }

            fragment ftMetaFull on ft_meta {
              contract
              name
              decimals
              symbol
              icon
              spec
              reference
              reference_hash
              price
              description
              twitter
              facebook
              telegram
              reddit
              website
            }
                ",
                Variables = request
            };

            var result = new List<NearblocksFungibleTokenEvent>();
            var data = await GetDataAsync<NearblocksFungibleTokenEvent>(query, "assets__fungible_token_events");
            result.AddRange(data);
            while (data.Count > 0 && data.Count == request.Limit)
            {
                request = new GetNearblocksFungibleTokenEventsRequest(request.Id, request.Limit, request.Offset + request.Limit);
                query.Variables = request;
                data = await GetDataAsync<NearblocksFungibleTokenEvent>(query, "assets__fungible_token_events");
                result.AddRange(data);
            }

            return await Result<List<NearblocksFungibleTokenEvent>>.SuccessAsync(result, "Fungible token events received.");
        }

        private async Task<Result<List<NearblocksNonFungibleTokenEvent>>> GetNearblocksNonFungibleTokenEventsAsync(
            GetNearblocksNonFungibleTokenEventsRequest request)
        {
            var query = new GraphQLRequest
            {
                Query = @"
                    query ($limit: Int, $offset: Int, $id: String) {
                      assets__non_fungible_token_events(
                        limit: $limit
                        offset: $offset
                        order_by: {emitted_at_block_timestamp: desc}
                        where: {_or: [{emitted_by_contract_account_id: {_eq: $id}}, {token_new_owner_account_id: {_eq: $id}}, {token_old_owner_account_id: {_eq: $id}}]}
                      ) {
                        emitted_for_receipt_id
                        emitted_at_block_timestamp
                        emitted_by_contract_account_id
                        token_id
                        event_kind
                        token_old_owner_account_id
                        token_new_owner_account_id
                        receipt {
                          execution_outcome {
                            status
                          }
                          action_receipt_actions{
                            action_kind
                            args
                          }
                          transaction {
                            transaction_hash
                            included_in_block_hash
                            block_timestamp
                            signer_account_id
                            receiver_account_id
                            status
                            block {
                              block_height
                              block_hash
                              block_timestamp
                              author_account_id
                            }
                          }
                        }
                        nft_meta {
                          ...nftMetaFull
                        }
                      }
                    }

                    fragment nftMetaFull on nft_meta {
                      contract
                      name
                      symbol
                      spec
                      base_uri
                      reference
                      reference_hash
                      description
                      twitter
                      facebook
                      telegram
                      reddit
                      website
                    }
                ",
                Variables = request
            };

            var result = new List<NearblocksNonFungibleTokenEvent>();
            var data = await GetDataAsync<NearblocksNonFungibleTokenEvent>(query, "assets__non_fungible_token_events");
            result.AddRange(data);
            while (data.Count > 0 && data.Count == request.Limit)
            {
                request = new GetNearblocksNonFungibleTokenEventsRequest(request.Id, request.Limit, request.Offset + request.Limit);
                query.Variables = request;
                data = await GetDataAsync<NearblocksNonFungibleTokenEvent>(query, "assets__non_fungible_token_events");
                result.AddRange(data);
            }

            return await Result<List<NearblocksNonFungibleTokenEvent>>.SuccessAsync(result, "Non fungible token events received.");
        }

        private async Task<NearblocksAccount> GetBalanceAsync(string address)
        {
            string request =
                $"/api/account/balance?address={address}";

            var response = await _restClient.GetAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<NearblocksAccount>()
                   ?? throw new CustomException("Can't get account balance.");
        }

        private async Task<decimal> GetUsdBalanceAsync(decimal balance)
        {
            var response = await _restClient.GetAsync("/api/near-price");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<NearblocksNearPrice>()
                       ?? throw new CustomException("Can't get USD balance.");
            return balance * data.Usd;
        }
    }
}