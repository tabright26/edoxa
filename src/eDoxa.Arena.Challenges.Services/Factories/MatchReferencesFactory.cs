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

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Services.LeagueOfLegends.Adapters;
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.Api.Abstractions;
using eDoxa.Seedwork.Domain.Common.Enumerations;

// ReSharper disable PossibleInvalidOperationException

namespace eDoxa.Arena.Challenges.Services.Factories
{
    public sealed class MatchReferencesFactory : IMatchReferencesFactory
    {
        private readonly ILeagueOfLegendsMatchService _leagueOfLegendsMatchService;

        public MatchReferencesFactory(ILeagueOfLegendsMatchService leagueOfLegendsMatchService)
        {
            _leagueOfLegendsMatchService = leagueOfLegendsMatchService;
        }

        public async Task<IMatchReferencesAdapter> CreateAdapterAsync(Game game, ExternalAccount externalAccount, ChallengeTimeline timeline)
        {
            if (timeline.State == ChallengeState.Inscription)
            {
                throw new InvalidOperationException();
            }

            if (game == Game.LeagueOfLegends)
            {
                var matchReferences = await _leagueOfLegendsMatchService.GetMatchReferencesAsync(
                    externalAccount.ToString(),
                    timeline.StartedAt.Value,
                    timeline.EndedAt.Value
                );

                return new LeagueOfLegendsMatchReferencesAdapter(matchReferences);
            }

            throw new NotSupportedException();
        }
    }
}
