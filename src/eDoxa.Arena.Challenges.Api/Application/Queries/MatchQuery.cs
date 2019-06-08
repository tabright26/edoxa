// Filename: MatchQuery.cs
// Date Created: 2019-06-07
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

using eDoxa.Arena.Challenges.Api.Application.Abstractions;
using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.Abstractions.Repositories;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Application.Queries
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
        public async Task<IReadOnlyCollection<MatchViewModel>> FindParticipantMatchesAsync(ParticipantId participantId)
        {
            var matches = await _repository.FindParticipantMatchesAsNoTrackingAsync(participantId);

            return _mapper.Map<IReadOnlyCollection<MatchViewModel>>(matches);
        }

        [ItemCanBeNull]
        public async Task<MatchViewModel> FindMatchAsync(MatchId matchId)
        {
            var match = await _repository.FindMatchAsNoTrackingAsync(matchId);

            return _mapper.Map<MatchViewModel>(match);
        }
    }
}
