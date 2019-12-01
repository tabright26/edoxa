// Filename: CreateTransactionRequest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace eDoxa.Cashier.Requests
{
    [JsonObject]
    public sealed class CreateTransactionRequest
    {
        [JsonConstructor]
        public CreateTransactionRequest(
            Guid id,
            string type,
            string currency,
            decimal amount,
            IDictionary<string, string> metadata = null
        )
        {
            Id = id;
            Type = type;
            Currency = currency;
            Amount = amount;
            Metadata = metadata ?? new Dictionary<string, string>();
        }

        public CreateTransactionRequest()
        {
            // Required by Fluent Validation.
        }

        [JsonProperty("id")]
        public Guid Id { get; private set; }

        [JsonProperty("type")]
        public string Type { get; private set; }

        [JsonProperty("currency")]
        public string Currency { get; private set; }

        [JsonProperty("amount")]
        public decimal Amount { get; private set; }

        [JsonProperty("metadata")]
        public IDictionary<string, string> Metadata { get; private set; }
    }
}
