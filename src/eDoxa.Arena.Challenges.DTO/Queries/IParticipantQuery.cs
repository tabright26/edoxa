// Filename: IParticipantQuery.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Functional;

namespace eDoxa.Arena.Challenges.DTO.Queries
{
    public interface IParticipantQuery
    {
        Task<Option<ParticipantListDTO>> FindChallengeParticipantsAsync(ChallengeId challengeId);

        Task<Option<ParticipantDTO>> FindParticipantAsync(ParticipantId participantId);
    }
}
