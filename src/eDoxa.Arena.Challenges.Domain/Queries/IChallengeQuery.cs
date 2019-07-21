// Filename: IChallengeQuery.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.Queries
{
    public interface IChallengeQuery
    {
        IMapper Mapper { get; }

        Task<IReadOnlyCollection<IChallenge>> FetchUserChallengeHistoryAsync(UserId userId, ChallengeGame game = null, ChallengeState state = null);

        Task<IReadOnlyCollection<IChallenge>> FetchUserChallengeHistoryAsync(ChallengeGame game = null, ChallengeState state = null);

        Task<IReadOnlyCollection<IChallenge>> FetchChallengesAsync(ChallengeGame game = null, ChallengeState state = null);

        [ItemCanBeNull]
        Task<IChallenge> FindChallengeAsync(ChallengeId challengeId);
    }
}
