// Filename: ChallengeQueryExtensions.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Areas.Challenges.Responses;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Challenges.Api.Infrastructure.Queries.Extensions
{
    public static class ChallengeQueryExtensions
    {
        public static async Task<IReadOnlyCollection<ChallengeModel>> FetchUserChallengeModelHistoryAsync(
            this IChallengeQuery challengeQuery,
            UserId userId,
            Game? game = null,
            ChallengeState? state = null
        )
        {
            var challenges = await challengeQuery.FetchUserChallengeHistoryAsync(userId, game, state);

            return challengeQuery.Mapper.Map<IReadOnlyCollection<ChallengeModel>>(challenges);
        }

        public static async Task<IReadOnlyCollection<ChallengeModel>> FetchUserChallengeModelHistoryAsync(
            this IChallengeQuery challengeQuery,
            Game? game = null,
            ChallengeState? state = null
        )
        {
            var challenges = await challengeQuery.FetchUserChallengeHistoryAsync(game, state);

            return challengeQuery.Mapper.Map<IReadOnlyCollection<ChallengeModel>>(challenges);
        }

        public static async Task<IReadOnlyCollection<ChallengeModel>> FetchChallengeModelsAsync(
            this IChallengeQuery challengeQuery,
            Game? game = null,
            ChallengeState? state = null
        )
        {
            var challenges = await challengeQuery.FetchChallengesAsync(game, state);

            return challengeQuery.Mapper.Map<IReadOnlyCollection<ChallengeModel>>(challenges);
        }

        public static async Task<ChallengeModel?> FindChallengeModelAsync(this IChallengeQuery challengeQuery, ChallengeId challengeId)
        {
            var challenge = await challengeQuery.FindChallengeAsync(challengeId);

            return challengeQuery.Mapper.Map<ChallengeModel>(challenge);
        }

        public static async Task<IReadOnlyCollection<ChallengeResponse>> FetchUserChallengeHistoryResponsesAsync(
            this IChallengeQuery challengeQuery,
            UserId userId,
            Game? game = null,
            ChallengeState? state = null
        )
        {
            var challenges = await challengeQuery.FetchUserChallengeHistoryAsync(userId, game, state);

            return challengeQuery.Mapper.Map<IReadOnlyCollection<ChallengeResponse>>(challenges);
        }

        public static async Task<IReadOnlyCollection<ChallengeResponse>> FetchUserChallengeHistoryResponsesAsync(
            this IChallengeQuery challengeQuery,
            Game? game = null,
            ChallengeState? state = null
        )
        {
            var challenges = await challengeQuery.FetchUserChallengeHistoryAsync(game, state);

            return challengeQuery.Mapper.Map<IReadOnlyCollection<ChallengeResponse>>(challenges);
        }

        public static async Task<IReadOnlyCollection<ChallengeResponse>> FetchChallengeResponsesAsync(
            this IChallengeQuery challengeQuery,
            Game? game = null,
            ChallengeState? state = null
        )
        {
            var challenges = await challengeQuery.FetchChallengesAsync(game, state);

            return challengeQuery.Mapper.Map<IReadOnlyCollection<ChallengeResponse>>(challenges);
        }

        public static async Task<ChallengeResponse?> FindChallengeResponseAsync(this IChallengeQuery challengeQuery, ChallengeId challengeId)
        {
            var challenge = await challengeQuery.FindChallengeAsync(challengeId);

            return challengeQuery.Mapper.Map<ChallengeResponse>(challenge);
        }
    }
}
