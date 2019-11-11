// Filename: PayoutResponse.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Cashier.Responses
{
    [JsonObject]
    public class PayoutResponse
    {
        [JsonProperty("prizePool")]
        public PrizePoolResponse PrizePool { get; set; }

        [JsonProperty("buckets")]
        public BucketResponse[] Buckets { get; set; }
    }
}
