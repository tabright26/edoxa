// Filename: ChallengeQuery.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Misc;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Api.Infrastructure.Queries
{
    public sealed partial class ChallengeQuery
    {
        public ChallengeQuery(CashierDbContext context, IMapper mapper)
        {
            Challenges = context.Challenges.AsNoTracking();
            Mapper = mapper;
        }

        private IQueryable<ChallengeModel> Challenges { get; }

        public IMapper Mapper { get; }

        private async Task<IReadOnlyCollection<ChallengeModel>> FetchChallengeModelsAsync()
        {
            var challenges = from challenge in Challenges.AsExpandable()
                             select challenge;

            return await challenges.ToListAsync();
        }

        private async Task<ChallengeModel?> FindChallengeModelAsync(Guid challengeId)
        {
            var challenges = from challenge in Challenges.AsExpandable()
                             where challenge.Id == challengeId
                             select challenge;

            return await challenges.SingleOrDefaultAsync();
        }
    }

    public sealed partial class ChallengeQuery : IChallengeQuery
    {
        public async Task<IReadOnlyCollection<IChallenge>> FetchChallengesAsync()
        {
            var challengeModels = await this.FetchChallengeModelsAsync();

            return Mapper.Map<IReadOnlyCollection<IChallenge>>(challengeModels);
        }

        public async Task<IChallenge> FindChallengeAsync(ChallengeId challengeId)
        {
            var challengeModel = await this.FindChallengeModelAsync(challengeId);

            return Mapper.Map<IChallenge>(challengeModel);
        }
    }
}
