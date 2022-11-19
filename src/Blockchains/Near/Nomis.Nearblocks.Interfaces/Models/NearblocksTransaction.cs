// ------------------------------------------------------------------------------------------------------
// <copyright file="NearblocksTransaction.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;
using System.Text.Json.Serialization;

namespace Nomis.Nearblocks.Interfaces.Models
{
    /// <summary>
    /// Nearblocks transaction data.
    /// </summary>
    public class NearblocksTransaction
    {
        /// <summary>
        /// Transaction hash.
        /// </summary>
        /// <example>GdzPZiErWkUFYrUjcXxRuiJahok7XLdHUwC37EhJWr5L</example>
        [JsonPropertyName("transaction_hash")]
        public string? TransactionHash { get; set; }

        /// <summary>
        /// Included in block hash.
        /// </summary>
        /// <example>4G8upBtp4GAFUh4xgu53aruNsCnjEtHkp4a3r2xfe2s6</example>
        [JsonPropertyName("included_in_block_hash")]
        public string? IncludedInBlockHash { get; set; }

        /// <summary>
        /// Block timestamp.
        /// </summary>
        /// <example>1606981986664956200</example>
        [JsonPropertyName("block_timestamp")]
        public BigInteger BlockTimestamp { get; set; }

        /// <summary>
        /// Signer account id.
        /// </summary>
        /// <example>bot.pulse.near</example>
        [JsonPropertyName("signer_account_id")]
        public string? SignerAccountId { get; set; }

        /// <summary>
        /// Receiver account id.
        /// </summary>
        /// <example>rekt.poolv1.near</example>
        [JsonPropertyName("receiver_account_id")]
        public string? ReceiverAccountId { get; set; }

        /// <summary>
        /// Status.
        /// </summary>
        /// <example>SUCCESS_RECEIPT_ID</example>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        /// Transaction block data.
        /// </summary>
        [JsonPropertyName("block")]
        public NearblocksTransactionBlock? Block { get; set; }

        /// <summary>
        /// Transaction receipts data.
        /// </summary>
        [JsonPropertyName("receipts")]
        public List<NearblocksTransactionReceipt> Receipts { get; set; } = new();
    }
}