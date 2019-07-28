// Filename: ChallengeQueryExtensions.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Arena.Challenges.Infrastructure.Models;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Queries.Extensions
{
    public static class ChallengeQueryExtensions
    {
        public static async Task<IReadOnlyCollection<ChallengeModel>> FetchUserChallengeModelHistoryAsync(
            this IChallengeQuery challengeQuery,
            UserId userId,
            ChallengeGame game = null,
            ChallengeState state = null
        )
        {
            var challenges = await challengeQuery.FetchUserChallengeHistoryAsync(userId, game, state);

            return challengeQuery.Mapper.Map<IReadOnlyCollection<ChallengeModel>>(challenges);
        }

        public static async Task<IReadOnlyCollection<ChallengeModel>> FetchUserChallengeModelHistoryAsync(
            this IChallengeQuery challengeQuery,
            ChallengeGame game = null,
            ChallengeState state = null
        )
        {
            var challenges = await challengeQuery.FetchUserChallengeHistoryAsync(game, state);

            return challengeQuery.Mapper.Map<IReadOnlyCollection<ChallengeModel>>(challenges);
        }

        public static async Task<IReadOnlyCollection<ChallengeModel>> FetchChallengeModelsAsync(
            this IChallengeQuery challengeQuery,
            ChallengeGame game = null,
            ChallengeState state = null
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

        public static async Task<IReadOnlyCollection<ChallengeViewModel>> FetchUserChallengeViewModelHistoryAsync(
            this IChallengeQuery challengeQuery,
            UserId userId,
            ChallengeGame game = null,
            ChallengeState state = null
        )
        {
            var challenges = await challengeQuery.FetchUserChallengeHistoryAsync(userId, game, state);

            return challengeQuery.Mapper.Map<IReadOnlyCollection<ChallengeViewModel>>(challenges);
        }

        public static async Task<IReadOnlyCollection<ChallengeViewModel>> FetchUserChallengeViewModelHistoryAsync(
            this IChallengeQuery challengeQuery,
            ChallengeGame game = null,
            ChallengeState state = null
        )
        {
            var challenges = await challengeQuery.FetchUserChallengeHistoryAsync(game, state);

            return challengeQuery.Mapper.Map<IReadOnlyCollection<ChallengeViewModel>>(challenges);
        }

        public static async Task<IReadOnlyCollection<ChallengeViewModel>> FetchChallengeViewModelsAsync(
            this IChallengeQuery challengeQuery,
            ChallengeGame game = null,
            ChallengeState state = null
        )
        {
            var challenges = await challengeQuery.FetchChallengesAsync(game, state);

            return challengeQuery.Mapper.Map<IReadOnlyCollection<ChallengeViewModel>>(challenges);
        }

        public static async Task<ChallengeViewModel?> FindChallengeViewModelAsync(this IChallengeQuery challengeQuery, ChallengeId challengeId)
        {
            var challenge = await challengeQuery.FindChallengeAsync(challengeId);

            return challengeQuery.Mapper.Map<ChallengeViewModel>(challenge);
        }
    }
}
