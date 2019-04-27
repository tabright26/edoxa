// Filename: MatchQueries.cs
// Date Created: 2019-04-21
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

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.DTO;
using eDoxa.Challenges.DTO.Queries;
using eDoxa.Challenges.Infrastructure;
using eDoxa.Functional.Maybe;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Challenges.Application.Queries
{
    public sealed partial class MatchQueries
    {
        private static readonly string ExpandStats = nameof(Match.Stats);

        private readonly ChallengesDbContext _context;
        private readonly IMapper _mapper;

        public MatchQueries(ChallengesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<IEnumerable<Match>> FindParticipantMatchesAsNoTrackingAsync(ParticipantId participantId)
        {
            return await _context.Matches.AsNoTracking()
                .Include(ExpandStats)
                .Where(match => match.Participant.Id == participantId)
                .OrderBy(match => match.Timestamp)
                .ToListAsync();
        }

        private async Task<Match> FindMatchAsNoTrackingAsync(MatchId matchId)
        {
            return await _context.Matches.AsNoTracking().Include(ExpandStats).Where(match => match.Id == matchId).SingleOrDefaultAsync();
        }
    }

    public sealed partial class MatchQueries : IMatchQueries
    {
        public async Task<Maybe<MatchListDTO>> FindParticipantMatchesAsync(ParticipantId participantId)
        {
            var matches = await this.FindParticipantMatchesAsNoTrackingAsync(participantId);

            var mapper = _mapper.Map<MatchListDTO>(matches);

            return mapper.Any() ? new Maybe<MatchListDTO>(mapper) : new Maybe<MatchListDTO>();
        }

        public async Task<Maybe<MatchDTO>> FindMatchAsync(MatchId matchId)
        {
            var match = await this.FindMatchAsNoTrackingAsync(matchId);

            var mapper = _mapper.Map<MatchDTO>(match);

            return mapper != null ? new Maybe<MatchDTO>(mapper) : new Maybe<MatchDTO>();
        }
    }
}