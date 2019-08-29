// Filename: TimelineResponse.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace eDoxa.Arena.Challenges.Api.Areas.Challenges.Responses
{
    [JsonObject]
    public class TimelineResponse
    {
        [JsonProperty("createdAt")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

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
