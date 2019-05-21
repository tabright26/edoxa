﻿// Filename: ParticipantRepository.cs
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
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Seedwork.Domain;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Arena.Challenges.Infrastructure.Repositories
{
    internal sealed partial class ParticipantRepository
    {
        private static readonly string NavigationPropertyPath = $"{nameof(Participant.Matches)}.{nameof(Match.Stats)}";

        private readonly ChallengesDbContext _context;

        public ParticipantRepository(ChallengesDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
    }

    internal sealed partial class ParticipantRepository : IParticipantRepository
    {
        public async Task<IEnumerable<Participant>> FindChallengeParticipantsAsNoTrackingAsync(ChallengeId challengeId)
        {
            return await _context.Participants.AsNoTracking()
                                 .Include(NavigationPropertyPath)
                                 .Where(participant => participant.Challenge.Id == challengeId)
                                 .OrderBy(participant => participant.Timestamp)
                                 .ToListAsync();
        }

        public async Task<Participant> FindParticipantAsNoTrackingAsync(ParticipantId participantId)
        {
            return await _context.Participants.AsNoTracking()
                                 .Include(NavigationPropertyPath)
                                 .Where(participant => participant.Id == participantId)
                                 .SingleOrDefaultAsync();
        }
    }
}
