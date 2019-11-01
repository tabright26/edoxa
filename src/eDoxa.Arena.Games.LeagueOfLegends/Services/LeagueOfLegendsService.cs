// Filename: LeagueOfLegendsService.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Games.LeagueOfLegends.Abstactions;

using Microsoft.Extensions.Options;

using RiotSharp;
using RiotSharp.Endpoints.Interfaces;

namespace eDoxa.Arena.Games.LeagueOfLegends.Services
{
    public sealed class LeagueOfLegendsService : ILeagueOfLegendsService
    {
        public LeagueOfLegendsService(IOptions<LeagueOfLegendsOptions> options)
        {
            RiotApi = RiotApi.GetDevelopmentInstance(options.Value.ApiKey);
        }

        private RiotApi RiotApi { get; }

        public ISummonerEndpoint Summoner => RiotApi.Summoner;
    }
}
