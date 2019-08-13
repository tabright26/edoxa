// Filename: TransactionViewModel.cs
// Date Created: 2019-07-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace eDoxa.Cashier.Api.ViewModels
{
    [JsonObject]
    public class TransactionViewModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("timestamp")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Timestamp { get; set; }

        [JsonProperty("currency")]
        public Currency Currency { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("type")]
        public TransactionType Type { get; set; }

        [JsonProperty("status")]
        public TransactionStatus Status { get; set; }
    }
}
