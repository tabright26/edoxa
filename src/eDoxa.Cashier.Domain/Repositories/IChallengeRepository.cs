// Filename: IChallengeRepository.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Cashier.Domain.Repositories
{
    public interface IChallengeRepository
    {
        void Create(IEnumerable<IChallenge> challenges);

        void Create(IChallenge challenge);

        Task<IChallenge> FindChallengeAsync(ChallengeId challengeId);

        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
