// Filename: BalanceResponse.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Cashier.Responses
{
    [JsonObject]
    public class BalanceResponse
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("available")]
        public decimal Available { get; set; }

        [JsonProperty("pending")]
        public decimal Pending { get; set; }
    }
}
