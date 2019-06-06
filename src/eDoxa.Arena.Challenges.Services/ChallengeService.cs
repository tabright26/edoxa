// Filename: ChallengeService.cs
// Date Created: 2019-06-01
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

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Services.Abstractions;
using eDoxa.Arena.Challenges.Services.Factories;
using eDoxa.Arena.Domain.ValueObjects;
using eDoxa.Seedwork.Domain.Common;
using eDoxa.Seedwork.Domain.Common.Enumerations;
using eDoxa.Seedwork.Domain.Extensions;

namespace eDoxa.Arena.Challenges.Services
{
    public sealed class ChallengeService : IChallengeService
    {
        private readonly IChallengeRepository _challengeRepository;

        public ChallengeService(IChallengeRepository challengeRepository)
        {
            _challengeRepository = challengeRepository;
        }

        public async Task<Participant> RegisterParticipantAsync(
            ChallengeId challengeId,
            UserId userId,
            Func<Game, ExternalAccount> funcExternalAccount,
            CancellationToken cancellationToken = default
        )
        {
            var challenge = await _challengeRepository.FindChallengeAsync(challengeId);

            var externalAccount = funcExternalAccount(challenge.Game);

            var participant = challenge.RegisterParticipant(userId, externalAccount);

            await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return participant;
        }

        public async Task CompleteAsync(ChallengeId challengeId, CancellationToken cancellationToken)
        {
            var challenges = await _challengeRepository.FindChallengesAsync();

            challenges.ForEach(
                challenge =>
                {
                    var scoreboard = new Scoreboard(challenge.Participants);

                    challenge.DistributePrizes(scoreboard);
                }
            );

            await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
        }

        public Task SynchronizeAsync(ChallengeId challengeId, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public async Task<Challenge> CreateChallengeAsync(
            string name,
            Game game,
            int duration,
            int bestOf,
            int payoutEntries,
            EntryFee entryFee,
            TestMode testMode = null,
            CancellationToken cancellationToken = default
        )
        {
            var builder = new ChallengeBuilder(
                game,
                new ChallengeName(name),
                new ChallengeSetup(new BestOf(bestOf), new PayoutEntries(payoutEntries), entryFee, new Entries(Convert.ToInt32(payoutEntries * 2))),
                new ChallengeTimeline(new ChallengeDuration(TimeSpan.FromDays(duration)))
            );

            builder.StoreScoring(ScoringFactory.Instance);

            builder.StorePayout(PayoutFactory.Instance);

            if (testMode != null)
            {
                builder.EnableTestMode(testMode);
            }

            var challenge = builder.Build() as Challenge;

            _challengeRepository.Create(challenge);

            await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return challenge;
        }
    }
}
