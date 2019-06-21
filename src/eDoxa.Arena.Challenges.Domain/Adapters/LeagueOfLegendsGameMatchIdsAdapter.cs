// Filename: LeagueOfLegendsMatchReferencesAdapter.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Domain.Abstractions.Adapters;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Services.LeagueOfLegends.Dtos;

namespace eDoxa.Arena.Challenges.Domain.Adapters
{
    public sealed class LeagueOfLegendsGameMatchIdsAdapter : IGameMatchIdsAdapter
    {
        private readonly LeagueOfLegendsMatchReferenceDto[] _matchReferences;

        public LeagueOfLegendsGameMatchIdsAdapter(LeagueOfLegendsMatchReferenceDto[] matchReferences)
        {
            _matchReferences = matchReferences;
        }

        public IEnumerable<GameMatchId> GameMatchIds => _matchReferences.Select(matchReference => new GameMatchId(matchReference.GameId));
    }
}
