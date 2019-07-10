// Filename: PayoutViewModel.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.Api.ViewModels
{
    [JsonObject]
    public class PayoutViewModel
    {
        [JsonProperty("prizePool")]
        public PrizePoolViewModel PrizePool { get; set; }

        [JsonProperty("buckets")]
        public BucketViewModel[] Buckets { get; set; }
    }
}
