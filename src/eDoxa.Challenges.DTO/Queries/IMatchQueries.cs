// Filename: IMatchQueries.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Functional.Maybe;

namespace eDoxa.Challenges.DTO.Queries
{
    public interface IMatchQueries
    {
        Task<Option<MatchListDTO>> FindParticipantMatchesAsync(ParticipantId participantId);

        Task<Option<MatchDTO>> FindMatchAsync(MatchId matchId);
    }
}