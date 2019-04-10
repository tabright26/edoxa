// Filename: LeagueOfLegendsMatchDTO.cs
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
    public class LeagueOfLegendsMatchDTO
    {
        [JsonProperty("gameId")]
        public long GameId { get; set; }

        [JsonProperty("participantIdentities")]
        public LeagueOfLegendsParticipantIdentityDTO[] ParticipantIdentities { get; set; }

        [JsonProperty("platformId")]
        public string PlatformId { get; set; }

        [JsonProperty("gameMode")]
        public string GameMode { get; set; }

        [JsonProperty("mapId")]
        public long MapId { get; set; }

        [JsonProperty("gameType")]
        public string GameType { get; set; }

        [JsonProperty("participants")]
        public LeagueOfLegendsParticipantDTO[] Participants { get; set; }

        [JsonProperty("gameCreation")]
        public long GameCreation { get; set; }
    }
}