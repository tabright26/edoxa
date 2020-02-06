// Filename: LeagueOfLegendsService.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Games.LeagueOfLegends.Abstactions;

using Microsoft.Extensions.Configuration;

using RiotSharp;
using RiotSharp.Endpoints.Interfaces;

namespace eDoxa.Games.LeagueOfLegends.Services
{
    public sealed class LeagueOfLegendsService : ILeagueOfLegendsService
    {
        public LeagueOfLegendsService(IConfiguration configuration)
        {
            RiotApi = RiotApi.GetInstance(configuration["LeagueOfLegends:ApiKey"], 500, 30000);
        }

        private RiotApi RiotApi { get; }

        public ISummonerEndpoint Summoner => RiotApi.Summoner;

        public IMatchEndpoint Match => RiotApi.Match;
    }
}
