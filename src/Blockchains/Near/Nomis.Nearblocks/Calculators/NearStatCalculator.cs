// ------------------------------------------------------------------------------------------------------
// <copyright file="NearStatCalculator.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;

using Nomis.Blockchain.Abstractions.Calculators;
using Nomis.Blockchain.Abstractions.Models;
using Nomis.Nearblocks.Extensions;
using Nomis.Nearblocks.Interfaces.Extensions;
using Nomis.Nearblocks.Interfaces.Models;

namespace Nomis.Nearblocks.Calculators
{
    /// <summary>
    /// Near wallet stats calculator.
    /// </summary>
    internal sealed class NearStatCalculator :
        IStatCalculator<NearWalletStats, NearTransactionIntervalData>
    {
        private readonly string _address;
        private readonly decimal _balance;
        private readonly decimal _usdBalance;
        private readonly IEnumerable<NearblocksTransaction> _transactions;
        private readonly IEnumerable<NearblocksFungibleToken> _fungibleTokens;
        private readonly IEnumerable<NearblocksNonFungibleTokenEvent> _tokens;

        public NearStatCalculator(
            string address,
            decimal balance,
            decimal usdBalance,
            IEnumerable<NearblocksTransaction> transactions,
            IEnumerable<NearblocksFungibleToken> fungibleTokens,
            IEnumerable<NearblocksNonFungibleTokenEvent> tokens)
        {
            _address = address;
            _balance = balance;
            _usdBalance = usdBalance;
            _transactions = transactions;
            _fungibleTokens = fungibleTokens;
            _tokens = tokens;
        }

        public NearWalletStats GetStats()
        {
            if (!_transactions.Any())
            {
                return new()
                {
                    NoData = true
                };
            }

            var intervals = IStatCalculator<NearWalletStats, NearTransactionIntervalData>
                .GetTransactionsIntervals(_transactions.Select(x => x.BlockTimestamp.ToString().ToNearDateTime())).ToList();
            if (intervals.Count == 0)
            {
                return new()
                {
                    NoData = true
                };
            }

            var monthAgo = DateTime.Now.AddMonths(-1);
            var yearAgo = DateTime.Now.AddYears(-1);

            var soldTokens = _tokens
                .Where(x => x.EventKind?.Equals("TRANSFER", StringComparison.OrdinalIgnoreCase) == true && x.From?.Equals(_address, StringComparison.InvariantCultureIgnoreCase) == true)
                .ToList();
            decimal soldSum = soldTokens.Count == 0 ? 0 : soldTokens.GetDepositSum();

            var soldTokensIds = soldTokens.Select(x => x.GetTokenUid());
            var buyTokens = _tokens
                .Where(t => (t.EventKind?.Equals("MINT", StringComparison.InvariantCultureIgnoreCase) == true ||
                             (t.EventKind?.Equals("TRANSFER", StringComparison.InvariantCultureIgnoreCase) == true && t.To?.Equals(_address, StringComparison.InvariantCultureIgnoreCase) == true))
                            && soldTokensIds.Contains(t.GetTokenUid()))
                .ToList();
            decimal buySum = buyTokens.Count == 0 ? 0 : buyTokens.GetDepositSum();

            var buyNotSoldTokens = _tokens
                .Where(t => (t.EventKind?.Equals("MINT", StringComparison.InvariantCultureIgnoreCase) == true ||
                             (t.EventKind?.Equals("TRANSFER", StringComparison.InvariantCultureIgnoreCase) == true && t.To?.Equals(_address, StringComparison.InvariantCultureIgnoreCase) == true))
                            && !soldTokensIds.Contains(t.GetTokenUid()))
                .ToList();
            decimal buyNotSoldSum = buyNotSoldTokens.Count == 0 ? 0 : buyNotSoldTokens.GetDepositSum();

            int holdingTokens = buyNotSoldTokens.Count;
            decimal nftWorth = buySum == 0 ? 0 : soldSum / buySum * buyNotSoldSum;

            int contractsCreated = _transactions.GetDeployedContractsCount();
            var totalTokens = _fungibleTokens.Select(x => x.Meta?.Contract).Distinct();

            var turnoverIntervalsDataList = _transactions
                .Select(x => new TurnoverIntervalsData(
                x.BlockTimestamp.ToString().ToNearDateTime(),
                new BigInteger(x.GetDepositSum()),
                x.ReceiverAccountId?.Equals(_address, StringComparison.InvariantCultureIgnoreCase) != true));
            var turnoverIntervals = IStatCalculator<NearWalletStats, NearTransactionIntervalData>
                .GetTurnoverIntervals(turnoverIntervalsDataList, _transactions.Min(x => x.BlockTimestamp.ToString().ToNearDateTime())).ToList();

            return new()
            {
                Balance = _balance,
                BalanceUSD = _usdBalance,
                WalletAge = IStatCalculator<NearWalletStats, NearTransactionIntervalData>
                    .GetWalletAge(_transactions.Select(x => x.BlockTimestamp.ToString().ToNearDateTime())),
                TotalTransactions = _transactions.Count(),
                TotalRejectedTransactions = _transactions.Count(t => t.Receipts.Any(r => r.ExecutionOutcome?.Status?.Equals("FAILURE", StringComparison.InvariantCultureIgnoreCase) == true)),
                MinTransactionTime = intervals.Min(),
                MaxTransactionTime = intervals.Max(),
                AverageTransactionTime = intervals.Average(),
                WalletTurnover = turnoverIntervals.Sum(x => Math.Abs((decimal)x.AmountSum)),
                TurnoverIntervals = turnoverIntervals,
                BalanceChangeInLastMonth = IStatCalculator<NearWalletStats, NearTransactionIntervalData>.GetBalanceChangeInLastMonth(turnoverIntervals),
                BalanceChangeInLastYear = IStatCalculator<NearWalletStats, NearTransactionIntervalData>.GetBalanceChangeInLastYear(turnoverIntervals),
                LastMonthTransactions = _transactions.Count(x => x.BlockTimestamp.ToString().ToNearDateTime() > monthAgo),
                LastYearTransactions = _transactions.Count(x => x.BlockTimestamp.ToString().ToNearDateTime() > yearAgo),
                TimeFromLastTransaction = (int)((DateTime.UtcNow - _transactions.OrderBy(x => x.BlockTimestamp).Last().BlockTimestamp.ToString().ToNearDateTime()).TotalDays / 30),
                NftHolding = holdingTokens,
                NftTrading = soldSum - buySum,
                NftWorth = nftWorth,
                DeployedContracts = contractsCreated,
                TokensHolding = totalTokens.Count()
            };
        }
    }
}