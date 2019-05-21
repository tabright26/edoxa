// Filename: ChallengeRepository.cs
// Date Created: 2019-05-20
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
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Enumerations;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Arena.Challenges.Infrastructure.Repositories
{
    internal sealed partial class ChallengeRepository
    {
        private static readonly string NavigationPropertyPath = $"{nameof(Challenge.Participants)}.{nameof(Participant.Matches)}.{nameof(Match.Stats)}";

        private readonly ChallengesDbContext _context;

        public ChallengeRepository(ChallengesDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Create(Challenge challenge)
        {
            _context.Challenges.Add(challenge);
        }

        public void Create(IEnumerable<Challenge> challenges)
        {
            _context.Challenges.AddRange(challenges);
        }
    }

    internal sealed partial class ChallengeRepository : IChallengeRepository
    {
        [ItemCanBeNull]
        public async Task<Challenge> FindChallengeAsync(ChallengeId challengeId)
        {
            return await _context.Challenges.Include(NavigationPropertyPath).Where(challenge => challenge.Id == challengeId).SingleOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<Challenge>> FindChallengesAsync(Game game)
        {
            var challenges = await _context.Challenges.Include(NavigationPropertyPath).ToListAsync();

            return challenges.Where(challenge => challenge.Game.HasFlag(game)).ToList();
        }

        public async Task<IReadOnlyCollection<Challenge>> FindUserChallengeHistoryAsNoTrackingAsync(UserId userId, Game game)
        {
            var challenges = await _context.Challenges.AsNoTracking()
                                           .Include(NavigationPropertyPath)
                                           .Where(challenge => challenge.Participants.Any(participant => participant.UserId == userId))
                                           .ToListAsync();

            return challenges.Where(challenge => challenge.Game.HasFlag(game)).ToList();
        }

        public async Task<IReadOnlyCollection<Challenge>> FindChallengesAsNoTrackingAsync(Game game)
        {
            var challenges = await _context.Challenges.AsNoTracking().Include(NavigationPropertyPath).ToListAsync();

            return challenges.Where(challenge => challenge.Game.HasFlag(game)).ToList();
        }

        public async Task<Challenge> FindChallengeAsNoTrackingAsync(ChallengeId challengeId)
        {
            return await _context.Challenges.AsNoTracking()
                                 .Include(NavigationPropertyPath)
                                 .Where(challenge => challenge.Id == challengeId)
                                 .SingleOrDefaultAsync();
        }
    }
}
