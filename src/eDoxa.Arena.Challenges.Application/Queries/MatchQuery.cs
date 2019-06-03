// Filename: MatchQuery.cs
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

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.DTO;
using eDoxa.Arena.Challenges.DTO.Queries;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Application.Queries
{
    public sealed partial class MatchQuery
    {
        private readonly IMapper _mapper;
        private readonly IMatchRepository _repository;

        public MatchQuery(IMatchRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }

    public sealed partial class MatchQuery : IMatchQuery
    {
        public async Task<IReadOnlyCollection<MatchDTO>> FindParticipantMatchesAsync(ParticipantId participantId)
        {
            var matches = await _repository.FindParticipantMatchesAsNoTrackingAsync(participantId);

            return _mapper.Map<IReadOnlyCollection<MatchDTO>>(matches);
        }

        [ItemCanBeNull]
        public async Task<MatchDTO> FindMatchAsync(MatchId matchId)
        {
            var match = await _repository.FindMatchAsNoTrackingAsync(matchId);

            return _mapper.Map<MatchDTO>(match);
        }
    }
}
