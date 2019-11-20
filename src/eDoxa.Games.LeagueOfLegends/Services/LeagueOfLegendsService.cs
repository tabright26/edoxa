// Filename: LeagueOfLegendsService.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Games.LeagueOfLegends.Abstactions;

using Microsoft.Extensions.Options;

using RiotSharp;
using RiotSharp.Endpoints.Interfaces;

namespace eDoxa.Games.LeagueOfLegends.Services
{
    public sealed class LeagueOfLegendsService : ILeagueOfLegendsService
    {
        public LeagueOfLegendsService(IOptions<LeagueOfLegendsOptions> options)
        {
            RiotApi = RiotApi.GetInstance(options.Value.ApiKey, 500, 30000);
        }

        private RiotApi RiotApi { get; }

        public ISummonerEndpoint Summoner => RiotApi.Summoner;

        public IMatchEndpoint Match => RiotApi.Match;
    }
}
