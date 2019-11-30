// Filename: BucketResponse.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Cashier.Responses
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
