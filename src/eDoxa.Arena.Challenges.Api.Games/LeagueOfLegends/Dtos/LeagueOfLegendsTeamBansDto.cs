// Filename: LeagueOfLegendsTeamBansDto.cs
// Date Created: 2019-06-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Newtonsoft.Json;

namespace eDoxa.Arena.Challenges.Api.Games.LeagueOfLegends.Dtos
{
    public class LeagueOfLegendsTeamBansDto
    {
        [JsonProperty("pickTurn")]
        public long PickTurn { get; set; }

        [JsonProperty("championId")]
        public long ChampionId { get; set; }
    }
}
