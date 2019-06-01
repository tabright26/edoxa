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
using eDoxa.Seedwork.Domain.Common;
using eDoxa.Seedwork.Domain.Common.Enumerations;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Arena.Challenges.Infrastructure.Repositories
{
    public sealed partial class ChallengeRepository
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
            _context.Add(challenge);
        }

        public void Create(IEnumerable<Challenge> challenges)
        {
            _context.Challenges.AddRange(challenges);
        }
    }

    public sealed partial class ChallengeRepository : IChallengeRepository
    {
        public async Task<Challenge> FindChallengeAsync(ChallengeId challengeId)
        {
            return await _context.Challenges.Include(NavigationPropertyPath).Where(challenge => challenge.Id == challengeId).SingleOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<Challenge>> FindChallengesAsync([CanBeNull] Game game = null, [CanBeNull] ChallengeState state = null)
        {
            var challenges = await _context.Challenges.Include(NavigationPropertyPath).ToListAsync();

            return challenges.Where(challenge => challenge.Game.HasFilter(game) && challenge.State.HasFilter(state)).ToList();
        }

        public async Task<IReadOnlyCollection<Challenge>> FindUserChallengeHistoryAsNoTrackingAsync(UserId userId, [CanBeNull] Game game = null, [CanBeNull] ChallengeState state = null)
        {
            var challenges = await _context.Challenges.AsNoTracking()
                                           .Include(NavigationPropertyPath)
                                           .Where(challenge => challenge.Participants.Any(participant => participant.UserId == userId))
                                           .ToListAsync();

            return challenges.Where(challenge => challenge.Game.HasFilter(game) && challenge.State.HasFilter(state)).ToList();
        }

        public async Task<IReadOnlyCollection<Challenge>> FindChallengesAsNoTrackingAsync([CanBeNull] Game game = null, [CanBeNull] ChallengeState state = null)
        {
            var challenges = await _context.Challenges.AsNoTracking().Include(NavigationPropertyPath).ToListAsync();

            return challenges.Where(challenge => challenge.Game.HasFilter(game) && challenge.State.HasFilter(state)).ToList();
        }

        [ItemCanBeNull]
        public async Task<Challenge> FindChallengeAsNoTrackingAsync(ChallengeId challengeId)
        {
            return await _context.Challenges.AsNoTracking()
                                 .Include(NavigationPropertyPath)
                                 .Where(challenge => challenge.Id == challengeId)
                                 .SingleOrDefaultAsync();
        }
    }
}
