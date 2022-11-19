// ------------------------------------------------------------------------------------------------------
// <copyright file="NearblocksTransactionReceiptExecutionOutcome.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Nearblocks.Interfaces.Models
{
    /// <summary>
    /// Nearblocks transaction receipt execution outcome data.
    /// </summary>
    public class NearblocksTransactionReceiptExecutionOutcome
    {
        /// <summary>
        /// Status.
        /// </summary>
        /// <example>FAILURE</example>
        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }
}