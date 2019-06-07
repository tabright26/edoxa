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

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Services.LeagueOfLegends.Adapters;
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.Api.Abstractions;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Arena.Challenges.Services.Factories
{
    public sealed class MatchStatsFactory : IMatchStatsFactory
    {
        private readonly ILeagueOfLegendsMatchService _leagueOfLegendsMatchService;

        public MatchStatsFactory(ILeagueOfLegendsMatchService leagueOfLegendsMatchService)
        {
            _leagueOfLegendsMatchService = leagueOfLegendsMatchService;
        }

        public async Task<IMatchStatsAdapter> CreateAdapter(Game game, ExternalAccount externalAccount, MatchReference matchReference)
        {
            if (game == Game.LeagueOfLegends)
            {
                var match = await _leagueOfLegendsMatchService.GetMatchAsync(matchReference.ToString());

                return new LeagueOfLegendsMatchStatsAdapter(externalAccount, match);
            }

            throw new NotSupportedException();
        }
    }
}
