// Filename: IChallengeRepository.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Challenges.Domain.Repositories
{
    public interface IChallengeRepository
    {
        void Create(IEnumerable<IChallenge> challenges);

        void Create(IChallenge challenge);

        Task<IReadOnlyCollection<IChallenge>> FetchChallengesAsync(Game? game = null, ChallengeState? state = null);

        Task<IChallenge?> FindChallengeAsync(ChallengeId challengeId);

        Task<bool> AnyChallengeAsync(ChallengeId challengeId);

        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
