// ------------------------------------------------------------------------------------------------------
// <copyright file="NearblocksAccount.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Nearblocks.Interfaces.Models
{
    /// <summary>
    /// Nearblocks account data.
    /// </summary>
    public class NearblocksAccount
    {
        /// <summary>
        /// Account balance.
        /// </summary>
        [JsonPropertyName("balance")]
        public string? Balance { get; set; }
    }
}