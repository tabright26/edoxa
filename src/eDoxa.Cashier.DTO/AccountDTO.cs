﻿// Filename: AccountDTO.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain;

using Newtonsoft.Json;

namespace eDoxa.Cashier.DTO
{
    [JsonObject]
    public class AccountDTO
    {
        [JsonProperty("currency")]
        public AccountCurrency Currency { get; set; }

        [JsonProperty("balance")]
        public decimal Balance { get; set; }

        [JsonProperty("pending")]
        public decimal Pending { get; set; }
    }
}