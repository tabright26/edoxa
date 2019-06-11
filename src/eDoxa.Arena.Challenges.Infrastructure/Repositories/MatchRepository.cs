﻿// Filename: MatchRepository.cs
// Date Created: 2019-06-01
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

using eDoxa.Arena.Challenges.Domain.Abstractions.Repositories;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Seedwork.Domain;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Arena.Challenges.Infrastructure.Repositories
{
    public sealed partial class MatchRepository
    {
        private const string NavigationPropertyPath = nameof(Match.Stats);

        private readonly ChallengesDbContext _context;

        public MatchRepository(ChallengesDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
    }

    public sealed partial class MatchRepository : IMatchRepository
    {
        public async Task<IEnumerable<Match>> FindParticipantMatchesAsNoTrackingAsync(ParticipantId participantId)
        {
            var participant = await _context.Participants.AsNoTracking()
                .Include("Matches.Stats")
                .Where(x => x.Id == participantId)
                .SingleAsync();

            return participant.Matches.OrderBy(match => match.Timestamp).ToList();
        }

        public async Task<Match> FindMatchAsNoTrackingAsync(MatchId matchId)
        {
            return await _context.Matches.AsNoTracking().Include(NavigationPropertyPath).Where(match => match.Id == matchId).SingleOrDefaultAsync();
        }
    }
}
