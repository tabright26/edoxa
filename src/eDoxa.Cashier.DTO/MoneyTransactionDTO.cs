// Filename: MoneyTransactionDTO.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Domain;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace eDoxa.Cashier.DTO
{
    [JsonObject]
    public class MoneyTransactionDTO
    {
        [JsonProperty("id")] public Guid Id { get; set; }

        [JsonProperty("timestamp")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Timestamp { get; set; }

        [JsonProperty("amount")] public decimal Amount { get; set; }

        [JsonProperty("type")] public TransactionType Type { get; set; }

        [JsonProperty("description")] public string Description { get; set; }
    }
}