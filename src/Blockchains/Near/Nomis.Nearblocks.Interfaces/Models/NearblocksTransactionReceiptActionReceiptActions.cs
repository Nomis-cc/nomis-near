// ------------------------------------------------------------------------------------------------------
// <copyright file="NearblocksTransactionReceiptActionReceiptActions.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Nearblocks.Interfaces.Models
{
    /// <summary>
    /// Nearblocks transaction receipt action receipt action data.
    /// </summary>
    public class NearblocksTransactionReceiptActionReceiptAction
    {
        /// <summary>
        /// Action kind.
        /// </summary>
        /// <example>FUNCTION_CALL</example>
        [JsonPropertyName("action_kind")]
        public string? ActionKind { get; set; }

        /// <summary>
        /// Args.
        /// </summary>
        [JsonPropertyName("args")]
        public Dictionary<string, object> Args { get; set; } = new();
    }
}