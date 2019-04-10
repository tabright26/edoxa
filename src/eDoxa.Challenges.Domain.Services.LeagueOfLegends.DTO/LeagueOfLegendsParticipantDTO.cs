// Filename: LeagueOfLegendsParticipantDTO.cs
// Date Created: 2019-03-25
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using Newtonsoft.Json;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.DTO
{
    public class LeagueOfLegendsParticipantDTO
    {
        [JsonProperty("stats")]
        public LeagueOfLegendsParticipantStatsDTO Stats { get; set; }

        [JsonProperty("spell1Id")]
        public long Spell1Id { get; set; }

        [JsonProperty("participantId")]
        public long ParticipantId { get; set; }

        [JsonProperty("highestAchievedSeasonTier", NullValueHandling = NullValueHandling.Ignore)]
        public string HighestAchievedSeasonTier { get; set; }

        [JsonProperty("spell2Id")]
        public long Spell2Id { get; set; }

        [JsonProperty("teamId")]
        public long TeamId { get; set; }

        [JsonProperty("timeline")]
        public LeagueOfLegendsParticipantTimelineDTO Timeline { get; set; }

        [JsonProperty("championId")]
        public long ChampionId { get; set; }
    }
}