// Filename: LeagueOfLegendsMatchReferenceDTO.cs
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

namespace eDoxa.Arena.Challenges.Api.Temp.LeagueOfLegends.Dtos
{
    public class LeagueOfLegendsMatchReferenceDto
    {
        [JsonProperty("lane")]
        public string Lane { get; set; }

        [JsonProperty("gameId")]
        public long GameId { get; set; }

        [JsonProperty("champion")]
        public long Champion { get; set; }

        [JsonProperty("platformId")]
        public string PlatformId { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("queue")]
        public long Queue { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("season")]
        public long Season { get; set; }
    }
}
