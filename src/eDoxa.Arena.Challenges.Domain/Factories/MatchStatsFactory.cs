// Filename: MatchStatsFactory.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.Abstractions.Adapters;
using eDoxa.Arena.Challenges.Domain.Abstractions.Factories;
using eDoxa.Arena.Challenges.Domain.Adapters;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Services.LeagueOfLegends.Abstractions;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.ValueObjects;

namespace eDoxa.Arena.Challenges.Domain.Factories
{
    public sealed class MatchStatsFactory : IMatchStatsFactory
    {
        private readonly ILeagueOfLegendsMatchService _leagueOfLegendsMatchService;

        public MatchStatsFactory(ILeagueOfLegendsMatchService leagueOfLegendsMatchService)
        {
            _leagueOfLegendsMatchService = leagueOfLegendsMatchService;
        }

        public async Task<IMatchStatsAdapter> CreateAdapter(Game game, GameAccountId gameAccountId, GameMatchId gameMatchId)
        {
            if (game == Game.LeagueOfLegends)
            {
                var match = await _leagueOfLegendsMatchService.GetMatchAsync(gameMatchId.ToString());

                return new LeagueOfLegendsMatchStatsAdapter(gameAccountId, match);
            }

            throw new NotSupportedException();
        }
    }
}
