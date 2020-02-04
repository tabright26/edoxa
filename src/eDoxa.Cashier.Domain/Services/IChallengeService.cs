// Filename: IChallengeService.cs
// Date Created: 2019-12-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.Services
{
    public interface IChallengeService
    {
        Task<IChallenge?> FindChallengeOrNullAsync(ChallengeId challengeId);

        Task<DomainValidationResult<IChallenge>> CreateChallengeAsync(
            ChallengeId challengeId,
            ChallengePayoutEntries payoutEntries,
            EntryFee entryFee,
            CancellationToken cancellationToken = default
        );

        Task<IChallenge> FindChallengeAsync(ChallengeId challengeId);

        Task<bool> ChallengeExistsAsync(ChallengeId challengeId);

        Task CloseChallengeAsync(
            IChallenge challenge,
            Dictionary<UserId, decimal?> scoreboard,
            CancellationToken cancellationToken = default
        );
    }
}
