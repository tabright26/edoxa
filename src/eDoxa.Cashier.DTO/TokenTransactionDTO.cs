// Filename: TokenTransactionDTO.cs
// Date Created: 2019-04-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Newtonsoft.Json;

namespace eDoxa.Cashier.DTO
{
    [JsonObject]
    public class TokenTransactionDTO
    {
        [JsonProperty("id")] public Guid Id { get; set; }

        [JsonProperty("timestamp")] public DateTime Timestamp { get; set; }

        [JsonProperty("amount")] public long Amount { get; set; }
    }
}