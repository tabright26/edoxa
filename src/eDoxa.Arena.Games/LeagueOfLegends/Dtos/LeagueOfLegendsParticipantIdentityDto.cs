// Filename: LeagueOfLegendsParticipantIdentityDto.cs
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
    public class LeagueOfLegendsParticipantIdentityDto
    {
        [JsonProperty("player")]
        public LeagueOfLegendsPlayerDto Player { get; set; }

        [JsonProperty("participantId")]
        public long ParticipantId { get; set; }
    }
}
