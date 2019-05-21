// Filename: IParticipantRepository.cs
// Date Created: 2019-05-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Arena.Challenges.Domain.Repositories
{
    public interface IParticipantRepository : IRepository<Participant>
    {
        Task<IEnumerable<Participant>> FindChallengeParticipantsAsNoTrackingAsync(ChallengeId challengeId);

        Task<Participant> FindParticipantAsNoTrackingAsync(ParticipantId participantId);
    }
}
