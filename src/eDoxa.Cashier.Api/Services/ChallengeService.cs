// Filename: ChallengeService.cs
// Date Created: 2019-12-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Api.Services
{
    public sealed class ChallengeService : IChallengeService
    {
        private readonly IChallengePayoutFactory _challengePayoutFactory;
        private readonly IChallengeRepository _challengeRepository;

        public ChallengeService(IChallengePayoutFactory challengePayoutFactory, IChallengeRepository challengeRepository)
        {
            _challengePayoutFactory = challengePayoutFactory;
            _challengeRepository = challengeRepository;
        }

        public async Task<IChallenge?> FindChallengeAsync(ChallengeId challengeId)
        {
            return await _challengeRepository.FindChallengeAsync(challengeId);
        }

        public async Task DeleteChallengeAsync(IChallenge challenge, CancellationToken cancellationToken = default)
        {
            _challengeRepository.Delete(challenge);

            await _challengeRepository.CommitAsync(cancellationToken);
        }

        public async Task<DomainValidationResult> CreateChallengeAsync(
            ChallengeId challengeId,
            PayoutEntries payoutEntries,
            EntryFee entryFee,
            CancellationToken cancellationToken = default
        )
        {
            var strategy = _challengePayoutFactory.CreateInstance();

            var payout = strategy.GetPayout(payoutEntries, entryFee);

            if (payout == null)
            {
                return DomainValidationResult.Failure("Invalid payout structure. Payout entries doesn't match the chart.");
            }

            var challenge = new Challenge(challengeId, entryFee, payout);

            _challengeRepository.Create(challenge);

            await _challengeRepository.CommitAsync(cancellationToken);

            return new DomainValidationResult();
        }
    }
}
