// Filename: IChallengeRepository.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.Repositories
{
    public interface IChallengeRepository
    {
        void Create(IEnumerable<IChallenge> challenges);

        void Create(IChallenge challenge);

        void Delete(IChallenge challenge);

        Task<IChallenge?> FindChallengeOrNullAsync(ChallengeId challengeId);

        Task CommitAsync(CancellationToken cancellationToken = default);

        Task<IChallenge> FindChallengeAsync(ChallengeId challengeId);

        Task<bool> ChallengeExistsAsync(ChallengeId challengeId);
    }
}
