// Filename: ChallengeRepository.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.Abstractions.Repositories;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Extensions;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Specifications.Abstractions;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Arena.Challenges.Infrastructure.Repositories
{
    public sealed class ChallengeRepository : IChallengeRepository
    {
        private const string NavigationPropertyPath = "Participants.Matches";

        private readonly ChallengesDbContext _context;
        private readonly IMapper _mapper;

        private IDictionary<Guid, IChallenge> _materializedIds = new Dictionary<Guid, IChallenge>();
        private IDictionary<IChallenge, ChallengeModel> _materializedObjects = new Dictionary<IChallenge, ChallengeModel>();

        public ChallengeRepository(ChallengesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    
        public void Create(IChallenge challenge)
        {
            var challengeModel = _mapper.Map<ChallengeModel>(challenge);

            _context.Challenges.Add(challengeModel);

            _materializedObjects[challenge] = challengeModel;
        }

        public void Create(IEnumerable<IChallenge> challenges)
        {
            challenges.ForEach(this.Create);
        }

        public async Task<IChallenge> FindChallengeAsync(ChallengeId challengeId)
        {
            if (_materializedIds.TryGetValue(challengeId.ToGuid(), out var challenge))
            {
                return challenge;
            }

            var challengeModel = await _context.Challenges.Include(NavigationPropertyPath).SingleOrDefaultAsync(x => x.Id == challengeId.ToGuid());

            if (challengeModel == null)
            {
                return null;
            }

            challenge = _mapper.Map<IChallenge>(challengeModel);

            _materializedObjects[challenge] = challengeModel;

            _materializedIds[challengeId] = challenge;

            return challenge;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            foreach (var (challenge, challengeModel) in _materializedObjects)
            {
                _mapper.CopyChanges(challenge, challengeModel);
            }

            await _context.SaveChangesAsync(cancellationToken);

            foreach (var (challenge, challengeModel) in _materializedObjects)
            {
                _materializedIds[challengeModel.Id] = challenge;
            }
        }

        public async Task<IReadOnlyCollection<IChallenge>> FindChallengesAsync(ChallengeGame game = null, ChallengeState state = null)
        {
            var challengeModels = await _context.Challenges.Include(NavigationPropertyPath).ToListAsync();

            var challenges = challengeModels.Select(challenge => _mapper.Map<IChallenge>(challenge));

            challenges = challenges.Where(challenge => challenge.Game.HasFilter(game) && challenge.Timeline.State.HasFilter(state));

            return challenges.ToList();
        }

        public async Task<IReadOnlyCollection<IChallenge>> FindChallengesAsync(ISpecification<IChallenge> specification)
        {
            var challengeModels = await _context.Challenges.Include(NavigationPropertyPath).ToListAsync();

            var challenges = challengeModels.Select(challenge => _mapper.Map<IChallenge>(challenge));

            return challenges.Where(specification.IsSatisfiedBy).ToList();
        }
    }
}
