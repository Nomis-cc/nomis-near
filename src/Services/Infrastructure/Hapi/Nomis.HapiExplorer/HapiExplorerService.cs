// ------------------------------------------------------------------------------------------------------
// <copyright file="HapiExplorerService.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

using Microsoft.Extensions.Options;
using Nethereum.ENS;
using Nethereum.Web3;
using Nomis.HapiExplorer.Converters;
using Nomis.HapiExplorer.Interfaces;
using Nomis.HapiExplorer.Interfaces.Responses;
using Nomis.HapiExplorer.Settings;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Exceptions;
using Nomis.Utils.Wrapper;

namespace Nomis.HapiExplorer
{
    /// <inheritdoc cref="IHapiExplorerService"/>
    internal sealed class HapiExplorerService :
        IHapiExplorerService,
        ITransientService
    {
        private readonly HapiExplorerSettings _settings;
        private readonly HttpClient _client;

        /// <summary>
        /// Initialize <see cref="HapiExplorerService"/>.
        /// </summary>
        /// <param name="settings"><see cref="HapiExplorerSettings"/>.</param>
        public HapiExplorerService(
            IOptions<HapiExplorerSettings> settings)
        {
            _settings = settings.Value;
            _client = new()
            {
                BaseAddress = new(settings.Value.ApiBaseUrl!)
            };
        }

        /// <inheritdoc/>
        public async Task<Result<HapiProxyRiskScoreResponse>> GetWalletRiskScoreAsync(string network, string address)
        {
            if (string.IsNullOrWhiteSpace(network))
            {
                throw new CustomException("Network must be set", statusCode: HttpStatusCode.BadRequest);
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new CustomException("Wallet address must be set", statusCode: HttpStatusCode.BadRequest);
            }

            if (address.EndsWith(".eth", StringComparison.CurrentCultureIgnoreCase))
            {
                var web3 = new Web3(_settings.BlockchainProviderUrl);
                address = await new ENSService(web3).ResolveAddressAsync(address);
            }

            string request =
                $"/check/{network}/{address}";
            var response = await _client.GetAsync(request);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new CustomException("The wallet risk score not found.", statusCode: HttpStatusCode.NotFound);
            }

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<HapiProxyRiskScoreResponse>(new JsonSerializerOptions
            {
                Converters =
                {
                    new HapiProxyCaseStatusConverter(),
                    new HapiProxyReporterRoleConverter(),
                    new HapiProxyCategoryConverter()
                }
            }) ?? throw new CustomException("Can't get risk score.");

            return await Result<HapiProxyRiskScoreResponse>.SuccessAsync(result!, "Got wallet risk score.");
        }
    }
}