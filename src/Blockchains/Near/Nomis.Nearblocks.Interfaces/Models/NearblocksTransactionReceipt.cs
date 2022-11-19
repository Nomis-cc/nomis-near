// ------------------------------------------------------------------------------------------------------
// <copyright file="NearblocksTransactionReceipt.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;
using System.Text.Json.Serialization;

namespace Nomis.Nearblocks.Interfaces.Models
{
    /// <summary>
    /// Nearblocks transaction receipt data.
    /// </summary>
    public class NearblocksTransactionReceipt
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
        /// Receipt id.
        /// </summary>
        /// <example>DPExCaqRqvVZY4vk36oqwLtA45Nqm241bnNdWk89qp9F</example>
        [JsonPropertyName("receipt_id")]
        public string? ReceiptId { get; set; }

        /// <summary>
        /// Included in block hash.
        /// </summary>
        /// <example>GfeigkVF8Pa74i1b16Ey9kXr3HoKuCEW4uhfQtqiGxso</example>
        [JsonPropertyName("included_in_block_hash")]
        public string? IncludedInBlockHash { get; set; }

        /// <summary>
        /// Included in block timestamp.
        /// </summary>
        /// <example>1606981986664956200</example>
        [JsonPropertyName("included_in_block_timestamp")]
        public BigInteger IncludedInBlockTimestamp { get; set; }

        /// <summary>
        /// Predecessor account id.
        /// </summary>
        /// <example>bot.pulse.near</example>
        [JsonPropertyName("predecessor_account_id")]
        public string? PredecessorAccountId { get; set; }

        /// <summary>
        /// Receiver account id.
        /// </summary>
        /// <example>rekt.poolv1.near</example>
        [JsonPropertyName("receiver_account_id")]
        public string? ReceiverAccountId { get; set; }

        /// <summary>
        /// Receipt kind.
        /// </summary>
        /// <example>ACTION</example>
        [JsonPropertyName("receipt_kind")]
        public string? ReceiptKind { get; set; }

        /// <summary>
        /// Originated from transaction hash.
        /// </summary>
        /// <example>GdzPZiErWkUFYrUjcXxRuiJahok7XLdHUwC37EhJWr5L</example>
        [JsonPropertyName("originated_from_transaction_hash")]
        public string? OriginatedFromTransactionHash { get; set; }
    }
}