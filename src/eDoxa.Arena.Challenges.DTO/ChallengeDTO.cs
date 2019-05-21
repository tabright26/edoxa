// Filename: ChallengeDTO.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Enumerations;

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.DTO
{
    [JsonObject]
    public class ChallengeDTO
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("game")]
        public Game Game { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("setup")]
        public ChallengeSetupDTO Setup { get; set; }

        [JsonProperty("scoring")]
        public ScoringDTO Scoring { get; set; }

        [JsonProperty("payout")]
        public PayoutDTO Payout { get; set; }

        [JsonProperty("scoreboard")]
        public ScoreboardDTO Scoreboard { get; set; }

        [JsonProperty("participants")]
        public ParticipantListDTO Participants { get; set; }
    }
}
