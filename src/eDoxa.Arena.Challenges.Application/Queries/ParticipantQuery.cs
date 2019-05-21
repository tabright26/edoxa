// Filename: ParticipantQuery.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.DTO;
using eDoxa.Arena.Challenges.DTO.Queries;
using eDoxa.Functional;

namespace eDoxa.Arena.Challenges.Application.Queries
{
    internal sealed partial class ParticipantQuery
    {
        private readonly IMapper _mapper;
        private readonly IParticipantRepository _repository;

        public ParticipantQuery(IParticipantRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }

    internal sealed partial class ParticipantQuery : IParticipantQuery
    {
        public async Task<Option<ParticipantListDTO>> FindChallengeParticipantsAsync(ChallengeId challengeId)
        {
            var participants = await _repository.FindChallengeParticipantsAsNoTrackingAsync(challengeId);

            var list = _mapper.Map<ParticipantListDTO>(participants);

            return list.Any() ? new Option<ParticipantListDTO>(list) : new Option<ParticipantListDTO>();
        }

        public async Task<Option<ParticipantDTO>> FindParticipantAsync(ParticipantId participantId)
        {
            var participant = await _repository.FindParticipantAsNoTrackingAsync(participantId);

            var mapper = _mapper.Map<ParticipantDTO>(participant);

            return mapper != null ? new Option<ParticipantDTO>(mapper) : new Option<ParticipantDTO>();
        }
    }
}
