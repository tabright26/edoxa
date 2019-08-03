// Filename: LeagueOfLegendsMatchDTO.cs
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

namespace eDoxa.Arena.Challenges.Api.Games.LeagueOfLegends.Dtos
{
    public class LeagueOfLegendsMatchDto
    {
        [JsonProperty("gameId")]
        public long GameId { get; set; }

        [JsonProperty("participantIdentities")]
        public LeagueOfLegendsParticipantIdentityDto[] ParticipantIdentities { get; set; }

        [JsonProperty("platformId")]
        public string PlatformId { get; set; }

        [JsonProperty("gameMode")]
        public string GameMode { get; set; }

        [JsonProperty("mapId")]
        public long MapId { get; set; }

        [JsonProperty("gameType")]
        public string GameType { get; set; }

        [JsonProperty("participants")]
        public LeagueOfLegendsParticipantDto[] Participants { get; set; }

        [JsonProperty("gameCreation")]
        public long GameCreation { get; set; }
    }
}
