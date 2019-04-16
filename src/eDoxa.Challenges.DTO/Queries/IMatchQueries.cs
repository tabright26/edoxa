﻿// Filename: IMatchQueries.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels;
using JetBrains.Annotations;

namespace eDoxa.Challenges.DTO.Queries
{
    public interface IMatchQueries
    {
        Task<MatchListDTO> FindParticipantMatchesAsync(ParticipantId participantId);

        [ItemCanBeNull]
        Task<MatchDTO> FindMatchAsync(MatchId matchId);
    }
}