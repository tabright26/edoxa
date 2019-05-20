// Filename: LeagueOfLegendsTeamBansDTO.cs
// Date Created: 2019-03-25
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using Newtonsoft.Json;

namespace eDoxa.Arena.Services.LeagueOfLegends.DTO
{
    public class LeagueOfLegendsTeamBansDTO
    {
        [JsonProperty("pickTurn")]
        public long PickTurn { get; set; }

        [JsonProperty("championId")]
        public long ChampionId { get; set; }
    }
}