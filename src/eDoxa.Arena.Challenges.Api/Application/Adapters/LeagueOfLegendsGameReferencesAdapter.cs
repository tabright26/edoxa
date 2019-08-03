// Filename: LeagueOfLegendsGameReferencesAdapter.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.Adapters;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Games.LeagueOfLegends.Abstractions;

namespace eDoxa.Arena.Challenges.Api.Application.Adapters
{
    public sealed class LeagueOfLegendsGameReferencesAdapter : IGameReferencesAdapter
    {
        private readonly ILeagueOfLegendsProxy _leagueOfLegendsProxy;

        public LeagueOfLegendsGameReferencesAdapter(ILeagueOfLegendsProxy leagueOfLegendsProxy)
        {
            _leagueOfLegendsProxy = leagueOfLegendsProxy;
        }

        public ChallengeGame Game => ChallengeGame.LeagueOfLegends;

        public async Task<IEnumerable<GameReference>> GetGameReferencesAsync(GameAccountId gameAccountId, DateTime startedAt, DateTime endedAt)
        {
            var matchReferences = await _leagueOfLegendsProxy.GetMatchReferencesAsync(gameAccountId.ToString(), startedAt, endedAt);

            return matchReferences.Select(matchReference => new GameReference(matchReference.GameId)).ToList();
        }
    }
}
