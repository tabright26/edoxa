// Filename: StatResponse.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Challenges.Responses
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
