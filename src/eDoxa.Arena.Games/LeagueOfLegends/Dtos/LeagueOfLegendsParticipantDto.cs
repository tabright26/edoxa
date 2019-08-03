// Filename: LeagueOfLegendsParticipantDto.cs
// Date Created: 2019-06-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

#nullable disable

using Newtonsoft.Json;

namespace eDoxa.Arena.Games.LeagueOfLegends.Dtos
{
    public class LeagueOfLegendsParticipantDto
    {
        [JsonProperty("stats")]
        public LeagueOfLegendsParticipantStatsDto Stats { get; set; }

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
        public LeagueOfLegendsParticipantTimelineDto Timeline { get; set; }

        [JsonProperty("championId")]
        public long ChampionId { get; set; }
    }
}
