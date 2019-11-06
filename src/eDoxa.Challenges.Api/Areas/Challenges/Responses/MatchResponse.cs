// Filename: MatchResponse.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;

using Newtonsoft.Json;

namespace eDoxa.Challenges.Api.Areas.Challenges.Responses
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
