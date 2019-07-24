// Filename: LeagueOfLegendsPlayerDto.cs
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
    public class LeagueOfLegendsPlayerDto
    {
        [JsonProperty("currentPlatformId")]
        public string CurrentPlatformId { get; set; }

        [JsonProperty("platformId")]
        public string PlatformId { get; set; }

        [JsonProperty("currentAccountId")]
        public string CurrentAccountId { get; set; }

        [JsonProperty("summonerId")]
        public string SummonerId { get; set; }

        [JsonProperty("accountId")]
        public string AccountId { get; set; }
    }
}
