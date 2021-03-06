﻿// Filename: ChallengeService.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Api.Application.Services
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

        public async Task<IChallenge?> FindChallengeOrNullAsync(ChallengeId challengeId)
        {
            return await _challengeRepository.FindChallengeOrNullAsync(challengeId);
        }

        public async Task<IChallenge> FindChallengeAsync(ChallengeId challengeId)
        {
            return await _challengeRepository.FindChallengeAsync(challengeId);
        }

        public async Task<bool> ChallengeExistsAsync(ChallengeId challengeId)
        {
            return await _challengeRepository.ChallengeExistsAsync(challengeId);
        }

        public async Task<DomainValidationResult<IChallenge>> CreateChallengeAsync(
            ChallengeId challengeId,
            ChallengePayoutEntries payoutEntries,
            EntryFee entryFee,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult<IChallenge>();

            var strategy = _challengePayoutFactory.CreateInstance();

            if (payoutEntries == 0)
            {
                return result.AddFailedPreconditionError("Challenge payout entries cannot be zero.");
            }

            var payout = strategy.GetChallengePayout(payoutEntries, entryFee);

            if (payout == null)
            {
                return result.AddFailedPreconditionError("Invalid payout structure. Payout entries doesn't match the chart.");
            }

            if (result.IsValid)
            {
                var challenge = new Challenge(challengeId, payout!);

                _challengeRepository.Create(challenge);

                await _challengeRepository.CommitAsync(true, cancellationToken);

                return challenge;
            }

            return result;
        }

        public async Task CloseChallengeAsync(IChallenge challenge, Dictionary<UserId, decimal?> scoreboard, CancellationToken cancellationToken = default)
        {
            challenge.Close(new ChallengeScoreboard(challenge.Payout, scoreboard));

            await _challengeRepository.CommitAsync(true, cancellationToken);
        }
    }
}
