// ------------------------------------------------------------------------------------------------------
// <copyright file="GetNearblocksFungibleTokensRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Nearblocks.Interfaces.Requests
{
    /// <summary>
    /// Request for getting the Nearblocks transactions.
    /// </summary>
    public class GetNearblocksFungibleTokensRequest
    {
        /// <summary>
        /// Initialize <see cref="GetNearblocksFungibleTokensRequest"/>
        /// </summary>
        /// <param name="address">Account id.</param>
        /// <param name="limit">Limit.</param>
        /// <param name="offset">Offset.</param>
        public GetNearblocksFungibleTokensRequest(
            string address,
            int limit = 1000,
            int offset = 0)
        {
            Id = address;
            Limit = limit;
            Offset = offset;
        }

        /// <summary>
        /// Account id.
        /// </summary>
        /// <example>tomazo.near</example>
        public string Id { get; }

        /// <summary>
        /// Limit.
        /// </summary>
        /// <example>1000</example>
        public int Limit { get; }

        /// <summary>
        /// Offset.
        /// </summary>
        /// <example>0</example>
        public int Offset { get; }
    }
}