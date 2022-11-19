// ------------------------------------------------------------------------------------------------------
// <copyright file="NearblocksTransactionBlock.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;
using System.Text.Json.Serialization;

namespace Nomis.Nearblocks.Interfaces.Models
{
    /// <summary>
    /// Nearblocks transaction block data.
    /// </summary>
    public class NearblocksTransactionBlock
    {
        /// <summary>
        /// Block height.
        /// </summary>
        /// <example>24234059</example>
        [JsonPropertyName("block_height")]
        public long BlockHeight { get; set; }

        /// <summary>
        /// Block hash.
        /// </summary>
        /// <example>4G8upBtp4GAFUh4xgu53aruNsCnjEtHkp4a3r2xfe2s6</example>
        [JsonPropertyName("block_hash")]
        public string? BlockHash { get; set; }

        /// <summary>
        /// Block timestamp.
        /// </summary>
        /// <example>1606981985621021400</example>
        [JsonPropertyName("block_timestamp")]
        public BigInteger BlockTimestamp { get; set; }

        /// <summary>
        /// Author account id.
        /// </summary>
        /// <example>buildlinks.poolv1.near</example>
        [JsonPropertyName("author_account_id")]
        public string? AuthorAccountId { get; set; }
    }
}