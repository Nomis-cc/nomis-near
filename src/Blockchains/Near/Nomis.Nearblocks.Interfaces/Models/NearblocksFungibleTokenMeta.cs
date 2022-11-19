// ------------------------------------------------------------------------------------------------------
// <copyright file="NearblocksFungibleTokenMeta.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Nearblocks.Interfaces.Models
{
    /// <summary>
    /// Nearblocks fungible token meta data.
    /// </summary>
    public class NearblocksFungibleTokenMeta
    {
        /// <summary>
        /// Contract.
        /// </summary>
        /// <example>meta-token.near</example>
        [JsonPropertyName("contract")]
        public string? Contract { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        /// <example>Wrapped NEAR fungible token</example>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Decimals.
        /// </summary>
        /// <example>24</example>
        [JsonPropertyName("decimals")]
        public int Decimals { get; set; }

        /// <summary>
        /// Symbol.
        /// </summary>
        /// <example>wNEAR</example>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        /// <summary>
        /// Icon.
        /// </summary>
        [JsonPropertyName("icon")]
        public string? Icon { get; set; }

        /// <summary>
        /// Spec.
        /// </summary>
        [JsonPropertyName("spec")]
        public string? Spec { get; set; }

        /// <summary>
        /// Reference.
        /// </summary>
        [JsonPropertyName("reference")]
        public string? Reference { get; set; }

        /// <summary>
        /// Reference hash.
        /// </summary>
        [JsonPropertyName("reference_hash")]
        public string? ReferenceHash { get; set; }

        /// <summary>
        /// Price.
        /// </summary>
        /// <example>1.8867239</example>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Twitter.
        /// </summary>
        [JsonPropertyName("twitter")]
        public string? Twitter { get; set; }

        /// <summary>
        /// Facebook.
        /// </summary>
        [JsonPropertyName("facebook")]
        public string? Facebook { get; set; }

        /// <summary>
        /// Telegram.
        /// </summary>
        [JsonPropertyName("telegram")]
        public string? Telegram { get; set; }

        /// <summary>
        /// Reddit.
        /// </summary>
        [JsonPropertyName("reddit")]
        public string? Reddit { get; set; }

        /// <summary>
        /// Website.
        /// </summary>
        [JsonPropertyName("website")]
        public string? Website { get; set; }
    }
}