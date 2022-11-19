// ------------------------------------------------------------------------------------------------------
// <copyright file="NearblocksFungibleToken.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Nearblocks.Interfaces.Models
{
    /// <summary>
    /// Nearblocks fungible token data.
    /// </summary>
    public class NearblocksFungibleToken
    {
        /// <summary>
        /// Amount.
        /// </summary>
        /// <example>2.892354481609</example>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Contract.
        /// </summary>
        /// <example>meta-token.near</example>
        [JsonPropertyName("contract")]
        public string? Contract { get; set; }

        /// <summary>
        /// Meta.
        /// </summary>
        [JsonPropertyName("ft_meta")]
        public NearblocksFungibleTokenMeta? Meta { get; set; }
    }
}