// Filename: ILeagueOfLegendsService.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using RiotSharp.Endpoints.Interfaces;

namespace eDoxa.Games.LeagueOfLegends.Abstactions
{
    public interface ILeagueOfLegendsService
    {
        ISummonerEndpoint Summoner { get; }
    }
}
