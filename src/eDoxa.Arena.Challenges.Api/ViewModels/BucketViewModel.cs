// Filename: BucketViewModel.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

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
