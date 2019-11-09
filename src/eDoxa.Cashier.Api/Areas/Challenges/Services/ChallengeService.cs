// Filename: ChallengeService.cs
// Date Created: 2019-11-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Challenges.Services.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

namespace eDoxa.Cashier.Api.Areas.Challenges.Services
{
    public sealed class ChallengeService : IChallengeService
    {
        private readonly IPayoutFactory _payoutFactory;
        private readonly IChallengeRepository _challengeRepository;

        public ChallengeService(IPayoutFactory payoutFactory, IChallengeRepository challengeRepository)
        {
            _payoutFactory = payoutFactory;
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

        public async Task<ValidationResult> CreateChallengeAsync(ChallengeId challengeId, PayoutEntries payoutEntries, EntryFee entryFee)
        {
            var strategy = _payoutFactory.CreateInstance();

            var payout = strategy.GetPayout(payoutEntries, entryFee);

            if (payout == null)
            {
                return new ValidationFailure("", "Invalid payout structure. Payout entries doesn't match the chart.").ToResult();
            }

            var challenge = new Challenge(challengeId, entryFee, payout);

            _challengeRepository.Create(challenge);

            await _challengeRepository.CommitAsync();

            return new ValidationResult();
        }
    }
}
