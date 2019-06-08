﻿// Filename: ParticipantQuery.cs
// Date Created: 2019-05-20
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

using eDoxa.Arena.Challenges.Application.Abstractions.Queries;
using eDoxa.Arena.Challenges.Application.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Application.Queries
{
    public sealed partial class ParticipantQuery
    {
        private readonly IMapper _mapper;
        private readonly IParticipantRepository _repository;

        public ParticipantQuery(IParticipantRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }

    public sealed partial class ParticipantQuery : IParticipantQuery
    {
        public async Task<IReadOnlyCollection<ParticipantViewModel>> FindChallengeParticipantsAsync(ChallengeId challengeId)
        {
            var participants = await _repository.FindChallengeParticipantsAsNoTrackingAsync(challengeId);

            return _mapper.Map<IReadOnlyCollection<ParticipantViewModel>>(participants);
        }

        [ItemCanBeNull]
        public async Task<ParticipantViewModel> FindParticipantAsync(ParticipantId participantId)
        {
            var participant = await _repository.FindParticipantAsNoTrackingAsync(participantId);

            return _mapper.Map<ParticipantViewModel>(participant);
        }
    }
}
