// Filename: LeagueOfLegendsAuthenticationFactor.cs
// Date Created: 2019-11-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Games.Domain;
using eDoxa.Games.Domain.AggregateModels;

using Newtonsoft.Json;

namespace eDoxa.Games.LeagueOfLegends
{
    [JsonObject]
    public sealed class LeagueOfLegendsAuthenticationFactor : IAuthenticationFactor
    {
        [JsonConstructor]
        public LeagueOfLegendsAuthenticationFactor(
            int currentSummonerProfileIconId,
            string currentSummonerProfileIconBase64,
            int expectedSummonerProfileIconId,
            string expectedSummonerProfileIconBase64
        )
        {
            CurrentSummonerProfileIconId = currentSummonerProfileIconId;
            CurrentSummonerProfileIconBase64 = currentSummonerProfileIconBase64;
            ExpectedSummonerProfileIconId = expectedSummonerProfileIconId;
            ExpectedSummonerProfileIconBase64 = expectedSummonerProfileIconBase64;
        }

        [JsonProperty("currentSummonerProfileIconId")]
        public int CurrentSummonerProfileIconId { get; }

        [JsonProperty("currentSummonerProfileIconBase64")]
        public string CurrentSummonerProfileIconBase64 { get; }

        [JsonProperty("expectedSummonerProfileIconId")]
        public int ExpectedSummonerProfileIconId { get; }

        [JsonProperty("expectedSummonerProfileIconBase64")]
        public string ExpectedSummonerProfileIconBase64 { get; }
    }
}
