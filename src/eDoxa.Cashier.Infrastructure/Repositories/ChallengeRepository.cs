// Filename: ChallengeRepository.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Infrastructure.Extensions;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.Infrastructure;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Infrastructure.Repositories
{
    public sealed partial class ChallengeRepository : Repository<IChallenge, ChallengePayoutModel>
    {
        public ChallengeRepository(CashierDbContext context)
        {
            UnitOfWork = context;
            ChallengePayouts = context.Set<ChallengePayoutModel>();
        }

        private IUnitOfWork UnitOfWork { get; }

        private DbSet<ChallengePayoutModel> ChallengePayouts { get; }

        private async Task<ChallengePayoutModel?> FindChallengeModelAsync(Guid challengeId)
        {
            var challenges = from challenge in ChallengePayouts.AsExpandable()
                             where challenge.ChallengeId == challengeId
                             select challenge;

            return await challenges.SingleOrDefaultAsync();
        }

        private async Task<bool> ChallengeModelExistsAsync(Guid challengeId)
        {
            return await ChallengePayouts.AsExpandable().AnyAsync(challenge => challenge.ChallengeId == challengeId);
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
            var challengeModel = challenge.ToModel();

            ChallengePayouts.Add(challengeModel);

            Mappings[challenge] = challengeModel;
        }

        public async Task<IChallenge> FindChallengeAsync(ChallengeId challengeId)
        {
            return await this.FindChallengeOrNullAsync(challengeId) ?? throw new InvalidOperationException("Challenge payout does not exists.");
        }

        public async Task<bool> ChallengeExistsAsync(ChallengeId challengeId)
        {
            return await this.ChallengeModelExistsAsync(challengeId);
        }

        public async Task<IChallenge?> FindChallengeOrNullAsync(ChallengeId challengeId)
        {
            var challenge = Mappings.Keys.SingleOrDefault(x => x.Id == challengeId);

            if (challenge != null)
            {
                return challenge;
            }

            var challengeModel = await this.FindChallengeModelAsync(challengeId);

            if (challengeModel == null)
            {
                return null;
            }

            challenge = challengeModel.ToEntity();

            Mappings[challenge] = challengeModel;

            return challenge;
        }

        public override async Task CommitAsync(bool publishDomainEvents = true, CancellationToken cancellationToken = default)
        {
            foreach (var (challenge, challengeModel) in Mappings)
            {
                CopyChanges(challenge, challengeModel);
            }

            await UnitOfWork.CommitAsync(publishDomainEvents, cancellationToken);
        }

        public void Delete(IChallenge challenge)
        {
            var challengeModel = Mappings[challenge];

            Mappings.Remove(challenge);

            ChallengePayouts.Remove(challengeModel);
        }

        private static void CopyChanges(IChallenge promotion, ChallengePayoutModel promotionModel)
        {
            promotionModel.DomainEvents = promotion.DomainEvents.ToList();

            promotion.ClearDomainEvents();
        }
    }
}
