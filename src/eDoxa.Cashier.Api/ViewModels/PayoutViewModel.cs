// Filename: PayoutViewModel.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using Newtonsoft.Json;

namespace eDoxa.Cashier.Api.ViewModels
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
