// Filename: ParticipantViewModel.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.Api.ViewModels
{
    [JsonObject]
    public class ParticipantViewModel
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
        public MatchViewModel[] Matches { get; set; }
    }
}
