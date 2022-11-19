// ------------------------------------------------------------------------------------------------------
// <copyright file="NearblocksNonFungibleTokenEventMeta.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Nearblocks.Interfaces.Models
{
    /// <summary>
    /// Nearblocks non fungible token event meta data.
    /// </summary>
    public class NearblocksNonFungibleTokenEventMeta
    {
        /// <summary>
        /// Contract.
        /// </summary>
        /// <example>astropup.near</example>
        [JsonPropertyName("contract")]
        public string? Contract { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        /// <example>AstroPup Collectibles</example>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Symbol.
        /// </summary>
        /// <example>Pups</example>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        /// <summary>
        /// Spec.
        /// </summary>
        /// <example>nft-1.0.0</example>
        [JsonPropertyName("spec")]
        public string? Spec { get; set; }

        /// <summary>
        /// Base URI.
        /// </summary>
        /// <example>https://nearnaut.mypinata.cloud/ipfs</example>
        [JsonPropertyName("base_uri")]
        public string? BaseUri { get; set; }

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