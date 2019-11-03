// Filename: StatResponse.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using Newtonsoft.Json;

namespace eDoxa.Challenges.Api.Areas.Challenges.Responses
{
    [JsonObject]
    public class StatResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("weighting")]
        public float Weighting { get; set; }

        [JsonProperty("score")]
        public decimal Score { get; set; }
    }
}
