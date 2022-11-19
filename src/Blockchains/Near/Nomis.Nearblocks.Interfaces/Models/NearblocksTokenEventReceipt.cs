// ------------------------------------------------------------------------------------------------------
// <copyright file="NearblocksTokenEventReceipt.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Nearblocks.Interfaces.Models
{
    /// <summary>
    /// Nearblocks non token event receipt data.
    /// </summary>
    public class NearblocksTokenEventReceipt
    {
        /// <summary>
        /// Execution outcome.
        /// </summary>
        [JsonPropertyName("execution_outcome")]
        public NearblocksTransactionReceiptExecutionOutcome? ExecutionOutcome { get; set; }

        /// <summary>
        /// Action receipt actions.
        /// </summary>
        [JsonPropertyName("action_receipt_actions")]
        public List<NearblocksTransactionReceiptActionReceiptAction> ActionReceiptActions { get; set; } = new();

        /// <summary>
        /// Transaction.
        /// </summary>
        [JsonPropertyName("transaction")]
        public NearblocksTransaction? Transaction { get; set; }
    }
}