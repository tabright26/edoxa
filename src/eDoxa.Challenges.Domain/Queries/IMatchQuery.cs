﻿// Filename: IMatchQuery.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Challenges.Domain.Queries
{
    public interface IMatchQuery
    {
        Task<IReadOnlyCollection<IMatch>> FetchParticipantMatchesAsync(ParticipantId participantId);

        Task<IMatch?> FindMatchAsync(MatchId matchId);
    }
}
