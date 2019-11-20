﻿// Filename: IParticipantQuery.cs
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

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Challenges.Domain.Queries
{
    public interface IParticipantQuery
    {
        IMapper Mapper { get; }

        Task<IReadOnlyCollection<Participant>> FetchChallengeParticipantsAsync(ChallengeId challengeId);

        Task<Participant?> FindParticipantAsync(ParticipantId participantId);
    }
}