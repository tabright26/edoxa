// Filename: LeagueOfLegendsRequest.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Arena.Games.LeagueOfLegends.Requests
{
    [DataContract]
    public sealed class LeagueOfLegendsRequest
    {
        public LeagueOfLegendsRequest(string summonerName)
        {
            SummonerName = summonerName;
        }

        [DataMember(Name = "summonerName")]
        public string SummonerName { get; }
    }
}
