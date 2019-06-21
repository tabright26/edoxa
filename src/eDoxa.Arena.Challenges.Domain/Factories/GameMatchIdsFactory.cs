﻿// Filename: MatchReferencesFactory.cs
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

// ReSharper disable PossibleInvalidOperationException

namespace eDoxa.Arena.Challenges.Domain.Factories
{
    public sealed class GameMatchIdsFactory : IGameMatchIdsFactory
    {
        private readonly ILeagueOfLegendsMatchService _leagueOfLegendsMatchService;

        public GameMatchIdsFactory(ILeagueOfLegendsMatchService leagueOfLegendsMatchService)
        {
            _leagueOfLegendsMatchService = leagueOfLegendsMatchService;
        }

        public async Task<IGameMatchIdsAdapter> CreateAdapterAsync(ChallengeGame game, GameAccountId gameAccountId, ChallengeTimeline timeline)
        {
            if (timeline.State == ChallengeState.Inscription)
            {
                throw new InvalidOperationException();
            }

            if (game == ChallengeGame.LeagueOfLegends)
            {
                var matchReferences = await _leagueOfLegendsMatchService.GetMatchReferencesAsync(
                    gameAccountId.ToString(),
                    timeline.StartedAt.Value,
                    timeline.EndedAt.Value
                );

                return new LeagueOfLegendsGameMatchIdsAdapter(matchReferences);
            }

            throw new NotSupportedException();
        }
    }
}
