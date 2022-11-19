// ------------------------------------------------------------------------------------------------------
// <copyright file="NearblocksFungibleTokenEvent.cs" company="Nomis">
// Copyright (c) Nomis, 2022. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Numerics;
using System.Text.Json.Serialization;

using Nomis.Nearblocks.Interfaces.Extensions;

namespace Nomis.Nearblocks.Interfaces.Models
{
    /// <summary>
    /// Nearblocks fungible token event data.
    /// </summary>
    public class NearblocksFungibleTokenEvent
    {
        /// <summary>
        /// Amount.
        /// </summary>
        [JsonPropertyName("amount")]
        public string? Amount { get; set; }

        /// <summary>
        /// Amount exact.
        /// </summary>
        public decimal AmountExact => Amount?.GetExactAmount(Meta?.Decimals) ?? 0;

        /// <summary>
        /// Emitted for receipt id.
        /// </summary>
        [JsonPropertyName("emitted_for_receipt_id")]
        public string? EmittedForReceiptId { get; set; }

        /// <summary>
        /// Emitted at block timestamp.
        /// </summary>
        [JsonPropertyName("emitted_at_block_timestamp")]
        public BigInteger EmittedAtBlockTimestamp { get; set; }

        /// <summary>
        /// Emitted by contract account id.
        /// </summary>
        [JsonPropertyName("emitted_by_contract_account_id")]
        public string? EmittedByContractAccountId { get; set; }

        /// <summary>
        /// Event kind.
        /// </summary>
        [JsonPropertyName("event_kind")]
        public string? EventKind { get; set; }

        /// <summary>
        /// Token old owner account id.
        /// </summary>
        [JsonPropertyName("token_old_owner_account_id")]
        public string? From { get; set; }

        /// <summary>
        /// Token new owner account id.
        /// </summary>
        [JsonPropertyName("token_new_owner_account_id")]
        public string? To { get; set; }

        /// <summary>
        /// Receipt data.
        /// </summary>
        [JsonPropertyName("receipt")]
        public NearblocksTokenEventReceipt? Receipt { get; set; }

        /// <summary>
        /// Token meta.
        /// </summary>
        [JsonPropertyName("ft_meta")]
        public NearblocksFungibleTokenMeta? Meta { get; set; }
    }
}