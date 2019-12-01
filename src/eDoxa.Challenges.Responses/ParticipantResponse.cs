// Filename: ParticipantResponse.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Newtonsoft.Json;

namespace eDoxa.Challenges.Responses
{
    [JsonObject]
    public class ParticipantResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("score")]
        public decimal? Score { get; set; }

        [JsonProperty("challengeId")]
        public Guid ChallengeId { get; set; }

        [JsonProperty("matches")]
        public MatchResponse[] Matches { get; set; }
    }
}
