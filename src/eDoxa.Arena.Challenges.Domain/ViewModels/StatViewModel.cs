// Filename: StatViewModel.cs
// Date Created: 2019-06-21
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
    public class StatViewModel
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
