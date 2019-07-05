// Filename: CurrencyViewModel.cs
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
    public class CurrencyViewModel
    {
        [JsonProperty("currency")]
        public Currency Type { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
