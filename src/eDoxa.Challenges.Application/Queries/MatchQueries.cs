// Filename: MatchQueries.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Challenges.DTO;
using eDoxa.Challenges.DTO.Queries;
using eDoxa.Challenges.Infrastructure;
using eDoxa.Functional.Maybe;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Challenges.Application.Queries
{
    public sealed partial class MatchQueries
    {
        private const string NavigationPropertyPath = nameof(Match.Stats);

        private readonly ChallengesDbContext _context;
        private readonly IMapper _mapper;

        public MatchQueries(ChallengesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<IEnumerable<Match>> FindParticipantMatchesAsNoTrackingAsync(ParticipantId participantId)
        {
            return await _context.Matches
                .AsNoTracking()
                .Include(NavigationPropertyPath)
                .Where(match => match.Participant.Id == participantId)
                .OrderBy(match => match.Timestamp)
                .ToListAsync();
        }

        private async Task<Match> FindMatchAsNoTrackingAsync(MatchId matchId)
        {
            return await _context.Matches
                .AsNoTracking()
                .Include(NavigationPropertyPath)
                .Where(match => match.Id == matchId)
                .SingleOrDefaultAsync();
        }
    }

    public sealed partial class MatchQueries : IMatchQueries
    {
        public async Task<Option<MatchListDTO>> FindParticipantMatchesAsync(ParticipantId participantId)
        {
            var matches = await this.FindParticipantMatchesAsNoTrackingAsync(participantId);

            var list = _mapper.Map<MatchListDTO>(matches);

            return list.Any() ? new Option<MatchListDTO>(list) : new Option<MatchListDTO>();
        }

        public async Task<Option<MatchDTO>> FindMatchAsync(MatchId matchId)
        {
            var match = await this.FindMatchAsNoTrackingAsync(matchId);

            var mapper = _mapper.Map<MatchDTO>(match);

            return mapper != null ? new Option<MatchDTO>(mapper) : new Option<MatchDTO>();
        }
    }
}