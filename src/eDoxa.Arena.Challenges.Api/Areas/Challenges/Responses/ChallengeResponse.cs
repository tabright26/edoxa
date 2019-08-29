// Filename: ChallengeResponse.cs
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
    public class ChallengeResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("game")]
        public string Game { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("bestOf")]
        public int BestOf { get; set; }

        [JsonProperty("entries")]
        public int Entries { get; set; }

        [JsonProperty("synchronizedAt")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? SynchronizedAt { get; set; }

        [JsonProperty("timeline")]
        public TimelineResponse Timeline { get; set; }

        [JsonProperty("scoring")]
        public ScoringResponse Scoring { get; set; }

        [JsonProperty("participants")]
        public ParticipantResponse[] Participants { get; set; }
    }
}
