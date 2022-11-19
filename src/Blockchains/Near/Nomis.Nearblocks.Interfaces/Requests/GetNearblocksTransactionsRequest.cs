// ------------------------------------------------------------------------------------------------------
// <copyright file="GetNearblocksTransactionsRequest.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

namespace Nomis.Nearblocks.Interfaces.Requests
{
    /// <summary>
    /// Request for getting the Nearblocks transactions.
    /// </summary>
    public class GetNearblocksTransactionsRequest
    {
        /// <summary>
        /// Address.
        /// </summary>
        /// <example>rekt.poolv1.near</example>
        public string? Address { get; set; }

        /// <summary>
        /// Limit.
        /// </summary>
        /// <example>1000</example>
        public int Limit { get; set; } = 1000;

        /// <summary>
        /// Offset.
        /// </summary>
        /// <example>0</example>
        public int Offset { get; set; } = 0;
    }
}