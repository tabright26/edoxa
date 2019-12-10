// Filename: IChallengeMatchesAdapter.cs
// Date Created: 2019-11-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Dtos;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Games.Domain.Adapters
{
    public interface IChallengeMatchesAdapter
    {
        Game Game { get; }

        Task<IReadOnlyCollection<ChallengeMatch>> GetMatchesAsync(PlayerId playerId, DateTime? startedAt, DateTime? endedAt);
    }
}
