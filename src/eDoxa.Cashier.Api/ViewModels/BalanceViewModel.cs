// Filename: BalanceViewModel.cs
// Date Created: 2019-07-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

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
