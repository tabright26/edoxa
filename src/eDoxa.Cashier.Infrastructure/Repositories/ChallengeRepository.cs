// Filename: ChallengeRepository.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Misc;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Infrastructure.Repositories
{
    public sealed partial class ChallengeRepository
    {
        private readonly IDictionary<Guid, IChallenge> _materializedIds = new Dictionary<Guid, IChallenge>();
        private readonly IDictionary<IChallenge, ChallengeModel> _materializedObjects = new Dictionary<IChallenge, ChallengeModel>();
        private readonly IMapper _mapper;
        private readonly CashierDbContext _context;

        public ChallengeRepository(CashierDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<ChallengeModel?> FindChallengeModelAsync(Guid challengeId)
        {
            var challenges = from challenge in _context.Challenges.AsExpandable()
                             where challenge.Id == challengeId
                             select challenge;

            return await challenges.SingleOrDefaultAsync();
        }
    }

    public sealed partial class ChallengeRepository : IChallengeRepository
    {
        public void Create(IEnumerable<IChallenge> challenges)
        {
            foreach (var challenge in challenges)
            {
                this.Create(challenge);
            }
        }

        public void Create(IChallenge challenge)
        {
            var challengeModel = _mapper.Map<ChallengeModel>(challenge);

            _context.Challenges.Add(challengeModel);

            _materializedObjects[challenge] = challengeModel;
        }

        public void Delete(IChallenge challenge)
        {
            var challengeModel = _materializedObjects[challenge];

            _materializedObjects.Remove(challenge);

            _materializedIds.Remove(challenge.Id);

            _context.Challenges.Remove(challengeModel);
        }

        public async Task<IChallenge?> FindChallengeAsync(ChallengeId challengeId)
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

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);

            foreach (var (challenge, challengeModel) in _materializedObjects)
            {
                _materializedIds[challengeModel.Id] = challenge;
            }
        }
    }
}
