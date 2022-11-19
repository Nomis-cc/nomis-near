// ------------------------------------------------------------------------------------------------------
// <copyright file="IWalletScore.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions.Models;

namespace Nomis.Blockchain.Abstractions
{
    /// <summary>
    /// Wallet score.
    /// </summary>
    /// <typeparam name="TWalletStats"><see cref="IWalletStats{TTransactionIntervalData}"/>.</typeparam>
    /// <typeparam name="TTransactionIntervalData"><see cref="ITransactionIntervalData"/>.</typeparam>
    public interface IWalletScore<TWalletStats, TTransactionIntervalData>
        where TWalletStats : IWalletStats<TTransactionIntervalData>
        where TTransactionIntervalData : class, ITransactionIntervalData
    {
        /// <summary>
        /// Wallet address.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Nomis Score in range of [0; 1].
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Additional stat data used in score calculations.
        /// </summary>
        public TWalletStats? Stats { get; set; }
    }
}