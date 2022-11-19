// ------------------------------------------------------------------------------------------------------
// <copyright file="NearblocksNearPrice.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Nearblocks.Interfaces.Models
{
    /// <summary>
    /// Nearblocks Near price data.
    /// </summary>
    public class NearblocksNearPrice
    {
        /// <summary>
        /// USD Near price.
        /// </summary>
        [JsonPropertyName("usd")]
        public decimal Usd { get; set; }
    }
}