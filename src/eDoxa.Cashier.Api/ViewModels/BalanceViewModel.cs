﻿// Filename: BalanceViewModel.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;

using Newtonsoft.Json;

namespace eDoxa.Cashier.Api.ViewModels
{
    [JsonObject]
    public class BalanceViewModel
    {
        [JsonProperty("currency")]
        public Currency Currency { get; set; }

        [JsonProperty("available")]
        public decimal Available { get; set; }

        [JsonProperty("pending")]
        public decimal Pending { get; set; }
    }
}