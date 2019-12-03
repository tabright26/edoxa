// Filename: IChallengeService.cs
// Date Created: 2019-12-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.Services
{
    public interface IChallengeService
    {
        Task<IChallenge?> FindChallengeAsync(ChallengeId challengeId);

        Task DeleteChallengeAsync(IChallenge challenge, CancellationToken cancellationToken = default);

        Task<DomainValidationResult> CreateChallengeAsync(
            ChallengeId challengeId,
            PayoutEntries payoutEntries,
            EntryFee entryFee,
            CancellationToken cancellationToken = default
        );
    }
}
