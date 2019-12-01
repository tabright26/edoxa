// Filename: PrizePoolResponse.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Cashier.Responses
{
    [JsonObject]
    public class PrizePoolResponse
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
