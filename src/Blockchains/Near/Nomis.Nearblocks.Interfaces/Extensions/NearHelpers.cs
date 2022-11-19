// ------------------------------------------------------------------------------------------------------
// <copyright file="NearHelpers.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Globalization;
using System.Numerics;

using Nomis.Nearblocks.Interfaces.Models;

namespace Nomis.Nearblocks.Interfaces.Extensions
{
    /// <summary>
    /// Extension methods for Near.
    /// </summary>
    public static class NearHelpers
    {
        /// <summary>
        /// Convert string amount value to exact amount with decimals.
        /// </summary>
        /// <param name="amount">Raw amount.</param>
        /// <param name="decimals">Token decimals.</param>
        /// <returns>Returns total amount of tokens.</returns>
        public static decimal GetExactAmount(this string? amount, int? decimals)
        {
            string? result = amount;
            if (decimals > 0 && !string.IsNullOrWhiteSpace(amount))
            {
                int sub = amount.Length - (int)decimals;
                while (sub < 0)
                {
                    amount = "0" + amount;
                    sub++;
                }

                result = amount.Insert(sub, ".");
                if (sub == 0)
                {
                    result = "0" + result;
                }
            }

            decimal.TryParse(
                result,
                NumberStyles.AllowDecimalPoint,
                new NumberFormatInfo { NumberDecimalSeparator = "." },
                out decimal res);

            return res;
        }

        /// <summary>
        /// Convert Wei value to Near.
        /// </summary>
        /// <param name="valueInWei">Wei.</param>
        /// <returns>Returns total Near.</returns>
        public static decimal ToNear(this in BigInteger valueInWei)
        {
            return (decimal)valueInWei;
        }

        /// <summary>
        /// Get token UID based on it EmittedByContractAccountId and TokenId.
        /// </summary>
        /// <param name="token">Token info.</param>
        /// <returns>Returns token UID.</returns>
        public static string GetTokenUid(this NearblocksNonFungibleTokenEvent token)
        {
            return token.EmittedByContractAccountId + "_" + token.TokenId;
        }

        /// <summary>
        /// Get deposits sum for non fungible token events (like "MINT" or "TRANSFER").
        /// </summary>
        /// <param name="nftEvents">Collection of <see cref="NearblocksNonFungibleTokenEvent"/>.</param>
        /// <returns>Returns sum of deposits.</returns>
        public static decimal GetDepositSum(
            this IEnumerable<NearblocksNonFungibleTokenEvent> nftEvents)
        {
            return nftEvents
                .SelectMany(t => t.Receipt!.ActionReceiptActions)
                .SelectMany(a => a.Args)
                .Where(a => a.Key == "deposit")
                .Select(a => a.Value)
                .Select(a => a.ToString().GetExactAmount(24))
                .Sum();
        }

        /// <summary>
        /// Get deposits sum for token transaction.
        /// </summary>
        /// <param name="transaction"><see cref="NearblocksTransaction"/>.</param>
        /// <returns>Returns sum of deposits.</returns>
        public static decimal GetDepositSum(
            this NearblocksTransaction transaction)
        {
            return transaction
                .Receipts
                .SelectMany(t => t.ActionReceiptActions)
                .SelectMany(a => a.Args)
                .Where(a => a.Key == "deposit")
                .Select(a => a.Value)
                .Select(a => a.ToString().GetExactAmount(24))
                .Sum();
        }

        /// <summary>
        /// Get deployed contracts count.
        /// </summary>
        /// <param name="transactions">Collection of <see cref="NearblocksTransaction"/>.</param>
        /// <returns>Returns deployed contracts count.</returns>
        public static int GetDeployedContractsCount(
            this IEnumerable<NearblocksTransaction> transactions)
        {
            return transactions
                .SelectMany(x => x.Receipts)
                .Count(a =>
                    a.ExecutionOutcome?.Status?.Equals("SUCCESS_VALUE", StringComparison.InvariantCultureIgnoreCase) ==
                    true && a.ActionReceiptActions.Any(t =>
                        t.ActionKind?.Equals("DEPLOY_CONTRACT", StringComparison.InvariantCultureIgnoreCase) == true));
        }
    }
}