// Filename: LeagueOfLegendsRequest.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Games.LeagueOfLegends.Requests
{
    [JsonObject]
    public sealed class LeagueOfLegendsRequest
    {
        [JsonConstructor]
        public LeagueOfLegendsRequest(string summonerName)
        {
            SummonerName = summonerName;
        }

        public LeagueOfLegendsRequest()
        {
            // Required by Fluent Validation.
        }

        [JsonProperty("summonerName")]
        public string SummonerName { get; }
    }
}
