// Filename: ChallengeQueryExtensions.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Challenges.Responses;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Infrastructure.Models;

namespace eDoxa.Cashier.Api.Infrastructure.Queries.Extensions
{
    public static class ChallengeQueryExtensions
    {
        public static async Task<ChallengeModel?> FindChallengeModelAsync(this IChallengeQuery challengeQuery, ChallengeId challengeId)
        {
            var challenge = await challengeQuery.FindChallengeAsync(challengeId);

            return challengeQuery.Mapper.Map<ChallengeModel>(challenge);
        }

        public static async Task<ChallengeResponse?> FindChallengeResponseAsync(this IChallengeQuery challengeQuery, ChallengeId challengeId)
        {
            var challenge = await challengeQuery.FindChallengeAsync(challengeId);

            return challengeQuery.Mapper.Map<ChallengeResponse>(challenge);
        }
    }
}
