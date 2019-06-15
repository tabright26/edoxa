// Filename: ParticipantRepository.cs
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
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Seedwork.Domain;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Arena.Challenges.Infrastructure.Repositories
{
    public sealed partial class ParticipantRepository
    {
        private static readonly string NavigationPropertyPath = $"{nameof(Participant.Matches)}";

        private readonly ChallengesDbContext _context;

        public ParticipantRepository(ChallengesDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
    }

    public sealed partial class ParticipantRepository : IParticipantRepository
    {
        public async Task<IEnumerable<Participant>> FindChallengeParticipantsAsNoTrackingAsync(ChallengeId challengeId)
        {
            return await _context.Participants.AsNoTracking()
                .Include(NavigationPropertyPath)
                .Include(participant => participant.Challenge)
                .Where(participant => participant.Challenge.Id == challengeId)
                .OrderBy(participant => participant.Timestamp)
                .ToListAsync();
        }

        public async Task<Participant> FindParticipantAsNoTrackingAsync(ParticipantId participantId)
        {
            return await _context.Participants.AsNoTracking()
                .Include(NavigationPropertyPath)
                .Include(participant => participant.Challenge)
                .Where(participant => participant.Id == participantId)
                .SingleOrDefaultAsync();
        }
    }
}
