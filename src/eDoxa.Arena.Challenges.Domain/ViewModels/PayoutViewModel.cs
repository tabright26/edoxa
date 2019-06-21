// Filename: PayoutViewModel.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.Domain.ViewModels
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
