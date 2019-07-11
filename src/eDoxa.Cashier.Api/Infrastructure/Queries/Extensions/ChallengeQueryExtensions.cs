// Filename: ChallengeQueryExtensions.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Infrastructure.Models;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Api.Infrastructure.Queries.Extensions
{
    public static class ChallengeQueryExtensions
    {
        [ItemCanBeNull]
        public static async Task<ChallengeModel> FindChallengeModelAsync(this IChallengeQuery challengeQuery, ChallengeId challengeId)
        {
            var challenge = await challengeQuery.FindChallengeAsync(challengeId);

            return challengeQuery.Mapper.Map<ChallengeModel>(challenge);
        }

        [ItemCanBeNull]
        public static async Task<ChallengeViewModel> FindChallengeViewModelAsync(this IChallengeQuery challengeQuery, ChallengeId challengeId)
        {
            var challenge = await challengeQuery.FindChallengeAsync(challengeId);

            return challengeQuery.Mapper.Map<ChallengeViewModel>(challenge);
        }
    }
}
