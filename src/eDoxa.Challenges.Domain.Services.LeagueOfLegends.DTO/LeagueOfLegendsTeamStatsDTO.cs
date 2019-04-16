// Filename: LeagueOfLegendsTeamStatsDTO.cs
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
    [JsonObject]
    public class LeagueOfLegendsTeamStatsDTO
    {
        [JsonProperty("firstDragon")]
        public bool FirstDragon { get; set; }

        [JsonProperty("bans")]
        public LeagueOfLegendsTeamBansDTO[] Banses { get; set; }

        [JsonProperty("firstInhibitor")]
        public bool FirstInhibitor { get; set; }

        [JsonProperty("win")]
        public string Win { get; set; }

        [JsonProperty("firstRiftHerald")]
        public bool FirstRiftHerald { get; set; }

        [JsonProperty("firstBaron")]
        public bool FirstBaron { get; set; }

        [JsonProperty("baronKills")]
        public long BaronKills { get; set; }

        [JsonProperty("riftHeraldKills")]
        public long RiftHeraldKills { get; set; }

        [JsonProperty("firstBlood")]
        public bool FirstBlood { get; set; }

        [JsonProperty("teamId")]
        public long TeamId { get; set; }

        [JsonProperty("firstTower")]
        public bool FirstTower { get; set; }

        [JsonProperty("vilemawKills")]
        public long VilemawKills { get; set; }

        [JsonProperty("inhibitorKills")]
        public long InhibitorKills { get; set; }

        [JsonProperty("towerKills")]
        public long TowerKills { get; set; }

        [JsonProperty("dominionVictoryScore")]
        public long DominionVictoryScore { get; set; }

        [JsonProperty("dragonKills")]
        public long DragonKills { get; set; }
    }
}