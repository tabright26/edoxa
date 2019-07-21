// Filename: IParticipantQuery.cs
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

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.Queries
{
    public interface IParticipantQuery
    {
        IMapper Mapper { get; }

        Task<IReadOnlyCollection<Participant>> FetchChallengeParticipantsAsync(ChallengeId challengeId);

        [ItemCanBeNull]
        Task<Participant> FindParticipantAsync(ParticipantId participantId);
    }
}
