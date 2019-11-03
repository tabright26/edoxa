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

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Challenges.Domain.Queries
{
    public interface IChallengeQuery
    {
        IMapper Mapper { get; }

        Task<IReadOnlyCollection<IChallenge>> FetchUserChallengeHistoryAsync(UserId userId, Game? game = null, ChallengeState? state = null);

        Task<IReadOnlyCollection<IChallenge>> FetchUserChallengeHistoryAsync(Game? game = null, ChallengeState? state = null);

        Task<IReadOnlyCollection<IChallenge>> FetchChallengesAsync(Game? game = null, ChallengeState? state = null);

        Task<IChallenge?> FindChallengeAsync(ChallengeId challengeId);
    }
}
