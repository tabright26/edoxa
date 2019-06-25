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

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Arena.Challenges.Infrastructure.Repositories
{
    public sealed partial class ChallengeRepository
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

        private async Task<IReadOnlyCollection<ChallengeModel>> FindChallengeModelsAsync(int? game = null, int? state = null)
        {
            var challenges = from challenge in _context.Challenges.Include(NavigationPropertyPath)
                             where (game == null || challenge.Game == game) && (state == null || challenge.State == state)
                             select challenge;

            return await challenges.ToListAsync();
        }

        [ItemCanBeNull]
        private async Task<ChallengeModel> FindChallengeModelAsync(Guid challengeId)
        {
            var challenges = from challenge in _context.Challenges.Include(NavigationPropertyPath)
                             where challenge.Id == challengeId
                             select challenge;

            return await challenges.SingleOrDefaultAsync();
        }

        private async Task<bool> AnyChallengeModelAsync(Guid challengeId)
        {
            var challenges = from challenge in _context.Challenges
                             where challenge.Id == challengeId
                             select challenge;

            return await challenges.AnyAsync();
        }
    }

    public sealed partial class ChallengeRepository : IChallengeRepository
    {
        public void Create(IEnumerable<IChallenge> challenges)
        {
            challenges.ForEach(this.Create);
        }

        public void Create(IChallenge challenge)
        {
            var challengeModel = _mapper.Map<ChallengeModel>(challenge);

            _context.Challenges.Add(challengeModel);

            _materializedObjects[challenge] = challengeModel;
        }

        public async Task<IReadOnlyCollection<IChallenge>> FindChallengesAsync(ChallengeGame game = null, ChallengeState state = null)
        {
            var challenges = await this.FindChallengeModelsAsync(game?.Value, state?.Value);

            return challenges.Select(
                    challengeModel =>
                    {
                        var challenge = _mapper.Map<IChallenge>(challengeModel);

                        _materializedObjects[challenge] = challengeModel;

                        _materializedIds[challenge.Id] = challenge;

                        return challenge;
                    }
                )
                .ToList();
        }

        public async Task<IReadOnlyCollection<IChallenge>> FindChallengesAsync(ISpecification<IChallenge> specification)
        {
            var challenges = await this.FindChallengeModelsAsync();

            return challenges.Select(challenge => _mapper.Map<IChallenge>(challenge)).Where(specification.IsSatisfiedBy).ToList();
        }

        [ItemCanBeNull]
        public async Task<IChallenge> FindChallengeAsync(ChallengeId challengeId)
        {
            if (_materializedIds.TryGetValue(challengeId, out var challenge))
            {
                return challenge;
            }

            var challengeModel = await this.FindChallengeModelAsync(challengeId);

            if (challengeModel == null)
            {
                return null;
            }

            challenge = _mapper.Map<IChallenge>(challengeModel);

            _materializedObjects[challenge] = challengeModel;

            _materializedIds[challengeId] = challenge;

            return challenge;
        }

        public async Task<bool> AnyChallengeAsync(ChallengeId challengeId)
        {
            return await this.AnyChallengeModelAsync(challengeId);
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
    }
}
