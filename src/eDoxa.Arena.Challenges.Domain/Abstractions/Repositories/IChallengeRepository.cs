// Filename: IChallengeRepository.cs
// Date Created: 2019-06-07
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

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Specifications.Abstractions;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.Abstractions.Repositories
{
    public interface IChallengeRepository
    {
        void Create(IEnumerable<IChallenge> challenges);

        void Create(IChallenge challenge);

        Task<IReadOnlyCollection<IChallenge>> FindChallengesAsync(ChallengeGame game = null, ChallengeState state = null);

        Task<IReadOnlyCollection<IChallenge>> FindChallengesAsync(ISpecification<IChallenge> specification);

        [ItemCanBeNull]
        Task<IChallenge> FindChallengeAsync(ChallengeId challengeId);

        Task<bool> AnyChallengeAsync(ChallengeId challengeId);

        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
