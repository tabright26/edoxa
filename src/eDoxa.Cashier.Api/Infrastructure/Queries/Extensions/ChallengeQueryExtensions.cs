// Filename: ChallengeQueryExtensions.cs
// Date Created: 2019-07-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Cashier.Responses;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Cashier.Api.Infrastructure.Queries.Extensions
{
    public static class ChallengeQueryExtensions
    {
        public static async Task<IReadOnlyCollection<ChallengeModel>> FetchChallengeModelsAsync(this IChallengeQuery challengeQuery)
        {
            var challenges = await challengeQuery.FetchChallengesAsync();

            return challengeQuery.Mapper.Map<IReadOnlyCollection<ChallengeModel>>(challenges);
        }

        public static async Task<IReadOnlyCollection<ChallengeResponse>> FetchChallengeResponsesAsync(this IChallengeQuery challengeQuery)
        {
            var challenges = await challengeQuery.FetchChallengesAsync();

            return challengeQuery.Mapper.Map<IReadOnlyCollection<ChallengeResponse>>(challenges);
        }

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
