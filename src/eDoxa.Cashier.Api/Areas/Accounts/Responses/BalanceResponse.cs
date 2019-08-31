// Filename: BalanceResponse.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using eDoxa.Cashier.Domain.AggregateModels;

using Newtonsoft.Json;

namespace eDoxa.Cashier.Api.Areas.Accounts.Responses
{
    [JsonObject]
    public class BalanceResponse
    {
        [JsonProperty("currency")]
        public Currency Currency { get; set; }

        [JsonProperty("available")]
        public decimal Available { get; set; }

        [JsonProperty("pending")]
        public decimal Pending { get; set; }
    }
}
