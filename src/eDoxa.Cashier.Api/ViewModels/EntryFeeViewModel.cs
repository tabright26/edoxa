﻿// Filename: EntryFeeViewModel.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Cashier.Api.ViewModels
{
    [JsonObject]
    public class EntryFeeViewModel
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}