// Filename: IChallengeMatchesAdapter.cs
// Date Created: 2019-11-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

using eDoxa.Games.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Games.Domain.Adapters
{
    public interface IChallengeMatchesAdapter
    {
        Game Game { get; }

        Task<IReadOnlyCollection<ChallengeMatch>> GetMatchesAsync(PlayerId playerId, DateTime? startedAt, DateTime? endedAt, IImmutableSet<string> matchIds);
    }
}
