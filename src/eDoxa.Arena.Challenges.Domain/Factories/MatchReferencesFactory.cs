// Filename: MatchReferencesFactory.cs
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

// ReSharper disable PossibleInvalidOperationException

namespace eDoxa.Arena.Challenges.Domain.Factories
{
    public sealed class MatchReferencesFactory : IMatchReferencesFactory
    {
        private readonly ILeagueOfLegendsMatchService _leagueOfLegendsMatchService;

        public MatchReferencesFactory(ILeagueOfLegendsMatchService leagueOfLegendsMatchService)
        {
            _leagueOfLegendsMatchService = leagueOfLegendsMatchService;
        }

        public async Task<IMatchReferencesAdapter> CreateAdapterAsync(Game game, UserGameReference userGameReference, ChallengeTimeline timeline)
        {
            if (timeline.State == ChallengeState.Inscription)
            {
                throw new InvalidOperationException();
            }

            if (game == Game.LeagueOfLegends)
            {
                var matchReferences = await _leagueOfLegendsMatchService.GetMatchReferencesAsync(
                    userGameReference.ToString(),
                    timeline.StartedAt.Value,
                    timeline.EndedAt.Value
                );

                return new LeagueOfLegendsMatchReferencesAdapter(matchReferences);
            }

            throw new NotSupportedException();
        }
    }
}
