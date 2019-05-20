// Filename: ChallengeTimelineDTO.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace eDoxa.Arena.Challenges.DTO
{
    [JsonObject]
    public class ChallengeTimelineDTO
    {
        [JsonProperty("createdAt")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("publishedAt")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? PublishedAt { get; set; }

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