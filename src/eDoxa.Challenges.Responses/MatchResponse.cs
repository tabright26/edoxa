// Filename: MatchResponse.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Newtonsoft.Json;

namespace eDoxa.Challenges.Responses
{
    [JsonObject]
    public class MatchResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("score")]
        public decimal Score { get; set; }

        [JsonProperty("participantId")]
        public Guid ParticipantId { get; set; }

        [JsonProperty("stats")]
        public StatResponse[] Stats { get; set; }
    }
}
