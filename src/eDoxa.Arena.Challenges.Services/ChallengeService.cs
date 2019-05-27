// Filename: ChallengeService.cs
// Date Created: 2019-05-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Services.Abstractions;
using eDoxa.Arena.Challenges.Services.Decorators;
using eDoxa.Arena.Challenges.Services.Factories;
using eDoxa.Arena.Challenges.Services.Validators;
using eDoxa.Arena.Domain;
using eDoxa.Functional;
using eDoxa.Functional.Extensions;
using eDoxa.Seedwork.Domain.Entities;
using eDoxa.Seedwork.Domain.Enumerations;
using eDoxa.Seedwork.Domain.Validations;

namespace eDoxa.Arena.Challenges.Services
{
    public sealed class ChallengeService : IChallengeService
    {
        private readonly IChallengeRepository _challengeRepository;

        public ChallengeService(IChallengeRepository challengeRepository)
        {
            _challengeRepository = challengeRepository;
        }

        public async Task<Either<ValidationError, Participant>> RegisterParticipantAsync(
            ChallengeId challengeId,
            UserId userId,
            Func<Game, ExternalAccount> funcExternalAccount,
            CancellationToken cancellationToken = default
        )
        {
            var challenge = await _challengeRepository.FindChallengeAsync(challengeId);

            if (challenge == null)
            {
                return new ValidationError("Challenge not found.");
            }

            var externalAccount = funcExternalAccount(challenge.Game);

            var validator = new RegisterParticipantValidator(userId, externalAccount);

            if (!validator.Validate(challenge, out var result))
            {
                return result.ValidationError;
            }

            var participant = challenge.RegisterParticipant(userId, externalAccount);

            await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return participant;
        }

        public async Task CompleteAsync(CancellationToken cancellationToken)
        {
            var challenges = await _challengeRepository.FindChallengesAsync(null);

            challenges.ForEach(challenge => challenge.Complete());

            await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
        }

        public Task SynchronizeAsync(Game game, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public async Task<Either<ValidationError, Challenge>> CreateChallengeAsync(
            string name,
            Game game,
            int duration,
            int bestOf,
            int payoutEntries,
            decimal entryFee,
            Currency currency,
            bool isFake = false,
            CancellationToken cancellationToken = default
        )
        {
            var challenge = new Challenge(
                game,
                new ChallengeName(name),
                new ChallengeSetup(new BestOf(bestOf), new PayoutEntries(payoutEntries), new EntryFee(entryFee, currency)),
                new ChallengeDuration(duration),
                ScoringFactory.Instance.CreateScoringStrategy(game).Scoring
            );

            if (isFake)
            {
                challenge = new FakeChallengeDecorator(challenge);
            }

            _challengeRepository.Create(challenge);

            await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return challenge;
        }
    }
}
