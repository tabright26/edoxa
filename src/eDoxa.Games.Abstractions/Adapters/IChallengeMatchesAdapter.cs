// Filename: IChallengeMatchesAdapter.cs
// Date Created: 2019-11-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Seedwork.Application.Dtos;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Games.Abstractions.Adapters
{
    public interface IChallengeMatchesAdapter
    {
        Game Game { get; }

        Task<IReadOnlyCollection<MatchDto>> GetMatchesAsync(PlayerId playerId, DateTime? startedAt, DateTime? endedAt);
    }
}
