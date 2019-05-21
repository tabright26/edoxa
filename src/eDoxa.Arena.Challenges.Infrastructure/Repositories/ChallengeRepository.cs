// Filename: ChallengeRepository.cs
// Date Created: 2019-05-06
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

    public sealed partial class ChallengeRepository : IChallengeRepository
    {
        [ItemCanBeNull]
        public async Task<Challenge> FindChallengeAsync(ChallengeId challengeId)
        {
            return await _context.Challenges.Include(ExpandParticipantMatchStats).Where(challenge => challenge.Id == challengeId).SingleOrDefaultAsync();
        }

        public async Task<IReadOnlyCollection<Challenge>> FindChallengesAsync(Game game, ChallengeState state)
        {
            return await _context.Challenges.Include(ExpandParticipantMatchStats)
                .Where(challenge => challenge.Game.HasFlag(game))
                .ToListAsync();
        }
    }
}