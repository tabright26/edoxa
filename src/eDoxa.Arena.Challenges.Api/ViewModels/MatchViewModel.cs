// Filename: MatchViewModel.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace eDoxa.Arena.Challenges.Api.ViewModels
{
    [JsonObject]
    public class MatchViewModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("timestamp")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Timestamp { get; set; }

        [JsonProperty("totalScore")]
        public decimal TotalScore { get; set; }

        [JsonProperty("stats")]
        public StatViewModel[] Stats { get; set; }
    }
}
