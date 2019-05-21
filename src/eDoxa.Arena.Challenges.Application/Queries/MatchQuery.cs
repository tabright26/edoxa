// Filename: MatchQuery.cs
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
    internal sealed partial class MatchQuery
    {
        private readonly IMapper _mapper;
        private readonly IMatchRepository _repository;

        public MatchQuery(IMatchRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }

    internal sealed partial class MatchQuery : IMatchQuery
    {
        public async Task<Option<MatchListDTO>> FindParticipantMatchesAsync(ParticipantId participantId)
        {
            var matches = await _repository.FindParticipantMatchesAsNoTrackingAsync(participantId);

            var list = _mapper.Map<MatchListDTO>(matches);

            return list.Any() ? new Option<MatchListDTO>(list) : new Option<MatchListDTO>();
        }

        public async Task<Option<MatchDTO>> FindMatchAsync(MatchId matchId)
        {
            var match = await _repository.FindMatchAsNoTrackingAsync(matchId);

            var mapper = _mapper.Map<MatchDTO>(match);

            return mapper != null ? new Option<MatchDTO>(mapper) : new Option<MatchDTO>();
        }
    }
}
