// Filename: BucketViewModel.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.Api.ViewModels
{
    [JsonObject]
    public class BucketViewModel
    {
        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("prize")]
        public decimal Prize { get; set; }
    }
}
