// ------------------------------------------------------------------------------------------------------
// <copyright file="INearblocksService.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using Nomis.Blockchain.Abstractions;
using Nomis.Nearblocks.Interfaces.Models;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

namespace Nomis.Nearblocks.Interfaces
{
    /// <summary>
    /// Service for interaction with Nearblocks API.
    /// </summary>
    public interface INearblocksService :
        IBlockchainDescriptor,
        IInfrastructureService
    {
        /// <inheritdoc cref="INearblocksGraphQLClient"/>
        public INearblocksGraphQLClient Client { get; }

        /*/// <summary>
        /// Get the Nearblocks transactions by account id.
        /// </summary>
        /// <param name="request">Nearblocks votes request.</param>
        /// <returns>Returns Nearblocks votes by voter.</returns>
        public Task<Result<List<NearblocksTransaction>>> GetNearblocksTransactionsAsync(
            GetNearblocksTransactionsRequest request);*/

        /// <summary>
        /// Get Near wallet stats by address.
        /// </summary>
        /// <param name="address">Near wallet address.</param>
        /// <returns>Returns <see cref="NearWalletScore"/> result.</returns>
        public Task<Result<NearWalletScore>> GetWalletStatsAsync(string address);
    }
}