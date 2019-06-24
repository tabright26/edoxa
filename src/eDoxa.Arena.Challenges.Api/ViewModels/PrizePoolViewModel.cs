// Filename: PrizePoolViewModel.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.Api.ViewModels
{
    [JsonObject]
    public class PrizePoolViewModel
    {
        [JsonProperty("currency")]
        public Currency Currency { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
