// Filename: BucketResponse.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Cashier.Api.Areas.Challenges.Responses
{
    [JsonObject]
    public class BucketResponse
    {
        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("prize")]
        public decimal Prize { get; set; }
    }
}
