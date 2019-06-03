﻿// Filename: ChallengeTimelineDTO.cs
// Date Created: 2019-06-01
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

namespace eDoxa.Arena.Challenges.DTO
{
    [JsonObject]
    public class ChallengeTimelineDTO
    {
        [JsonProperty("startedAt")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? StartedAt { get; set; }

        [JsonProperty("endedAt")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? EndedAt { get; set; }

        [JsonProperty("closedAt")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? ClosedAt { get; set; }
    }
}
