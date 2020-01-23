// Filename: LeagueOfLegendsService.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Games.LeagueOfLegends.Abstactions;
using eDoxa.Grpc.Protos.Games.Options;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.Extensions.Options;

using RiotSharp;
using RiotSharp.Endpoints.Interfaces;

namespace eDoxa.Games.LeagueOfLegends.Services
{
    public sealed class LeagueOfLegendsService : ILeagueOfLegendsService
    {
        public LeagueOfLegendsService(IOptionsSnapshot<GamesApiOptions> optionsSnapshot)
        {
            RiotApi = RiotApi.GetInstance(optionsSnapshot.Value.Configuration.ApiKeys[Game.LeagueOfLegends.Name], 500, 30000);
        }

        private RiotApi RiotApi { get; }

        public ISummonerEndpoint Summoner => RiotApi.Summoner;

        public IMatchEndpoint Match => RiotApi.Match;
    }
}
