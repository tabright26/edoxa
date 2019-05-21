// Filename: MatchRepository.cs
// Date Created: 2019-05-21
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

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Seedwork.Domain;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Arena.Challenges.Infrastructure.Repositories
{
    internal sealed partial class MatchRepository
    {
        private const string NavigationPropertyPath = nameof(Match.Stats);

        private readonly ChallengesDbContext _context;

        public MatchRepository(ChallengesDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
    }

    internal sealed partial class MatchRepository : IMatchRepository
    {
        public async Task<IEnumerable<Match>> FindParticipantMatchesAsNoTrackingAsync(ParticipantId participantId)
        {
            return await _context.Matches.AsNoTracking()
                                 .Include(NavigationPropertyPath)
                                 .Where(match => match.Participant.Id == participantId)
                                 .OrderBy(match => match.Timestamp)
                                 .ToListAsync();
        }

        public async Task<Match> FindMatchAsNoTrackingAsync(MatchId matchId)
        {
            return await _context.Matches.AsNoTracking().Include(NavigationPropertyPath).Where(match => match.Id == matchId).SingleOrDefaultAsync();
        }
    }
}
