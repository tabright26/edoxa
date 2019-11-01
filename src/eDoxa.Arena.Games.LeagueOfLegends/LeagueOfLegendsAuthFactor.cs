// Filename: LeagueOfLegendsAuthFactor.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Games.Domain.AggregateModels.AuthFactorAggregate;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Games.LeagueOfLegends
{
    public sealed class LeagueOfLegendsAuthFactor : AuthFactor
    {
        public LeagueOfLegendsAuthFactor(int summonerProfileIconId, PlayerId playerId) : base(playerId, summonerProfileIconId)
        {
        }
    }
}
