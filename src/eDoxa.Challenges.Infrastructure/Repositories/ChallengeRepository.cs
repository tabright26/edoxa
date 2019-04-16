// Filename: ChallengeRepository.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Common.Enums;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Challenges.Infrastructure.Repositories
{
    public sealed partial class ChallengeRepository
    {
        private static readonly string ExpandParticipants = nameof(Challenge.Participants);
        private static readonly string ExpandParticipantMatches = $"{ExpandParticipants}.{nameof(Participant.Matches)}";
        private static readonly string ExpandParticipantMatchStats = $"{ExpandParticipantMatches}.{nameof(Match.Stats)}";

        private readonly ChallengesDbContext _context;

        public ChallengeRepository(ChallengesDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public void Create(Challenge challenge)
        {
            _context.Challenges.Add(challenge);
        }
    }

    public sealed partial class ChallengeRepository : IChallengeRepository
    {
        public void Create(IEnumerable<Challenge> challenges)
        {
            _context.Challenges.AddRange(challenges);
        }

        public async Task<IReadOnlyCollection<Challenge>> FindChallengesAsync(Game game, ChallengeType type, ChallengeState state)
        {
            return await _context.Challenges.Include(ExpandParticipantMatchStats)
                                 .Where(
                                     challenge => (challenge.Game & game) != Game.None &&
                                                  (challenge.Settings.Type & type) != ChallengeType.None &&
                                                  (challenge.Timeline.State & state) != ChallengeState.None
                                 )
                                 .OrderBy(challenge => challenge.Timeline.StartedAt)
                                 .ToListAsync();
        }

        public async Task<Challenge> FindChallengeAsync(ChallengeId challengeId)
        {
            return await _context.Challenges.Include(ExpandParticipantMatchStats).Where(challenge => challenge.Id == challengeId).SingleOrDefaultAsync();
        }
    }
}