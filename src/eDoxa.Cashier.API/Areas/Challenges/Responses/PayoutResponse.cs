// Filename: PayoutResponse.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using Newtonsoft.Json;

namespace eDoxa.Cashier.Api.Areas.Challenges.Responses
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
