// Filename: ChallengeService.cs
// Date Created: 2019-06-07
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

using eDoxa.Arena.Challenges.Domain.Abstractions.Factories;
using eDoxa.Arena.Challenges.Domain.Abstractions.Repositories;
using eDoxa.Arena.Challenges.Domain.Abstractions.Services;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.Specifications;
using eDoxa.Seedwork.Domain.Common;
using eDoxa.Seedwork.Domain.Common.Enumerations;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Specifications;

namespace eDoxa.Arena.Challenges.Domain.Services
{
    public sealed class ChallengeService : IChallengeService
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly IPayoutFactory _payoutFactory;
        private readonly IScoringFactory _scoringFactory;
        private readonly IMatchReferencesFactory _matchReferencesFactory;
        private readonly IMatchStatsFactory _matchStatsFactory;

        public ChallengeService(
            IChallengeRepository challengeRepository,
            IPayoutFactory payoutFactory,
            IScoringFactory scoringFactory,
            IMatchReferencesFactory matchReferencesFactory,
            IMatchStatsFactory matchStatsFactory
        )
        {
            _challengeRepository = challengeRepository;
            _payoutFactory = payoutFactory;
            _scoringFactory = scoringFactory;
            _matchReferencesFactory = matchReferencesFactory;
            _matchStatsFactory = matchStatsFactory;
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

        public async Task CloseAsync(CancellationToken cancellationToken = default)
        {
            var specification = SpecificationFactory.Instance.Create<Challenge>().And(new ChallengeOfStateSpecification(ChallengeState.Ended));

            var challenges = await _challengeRepository.FindChallengesAsync(specification);

            challenges.ForEach(
                challenge => challenge.TryClose(
                    async () =>
                    {
                        await challenge.SynchronizeAsync(_matchReferencesFactory, _matchStatsFactory);

                        challenge.DistributeParticipantPrizes();
                    }
                )
            );

            await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
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

            builder.StoreScoring(_scoringFactory);

            builder.StorePayout(_payoutFactory);

            if (testMode != null)
            {
                builder.EnableTestMode(testMode);
            }

            var challenge = builder.Build() as Challenge;

            _challengeRepository.Create(challenge);

            await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return challenge;
        }

        public async Task SynchronizeAsync(Game game, CancellationToken cancellationToken = default)
        {
            var specification = SpecificationFactory.Instance.Create<Challenge>()
                .And(new ChallengeForGameSpecification(game))
                .And(new ChallengeLastSyncMoreThanSpecification(TimeSpan.FromHours(1)))
                .And(new ChallengeOfStateSpecification(ChallengeState.Inscription).Not());

            foreach (var challenge in await _challengeRepository.FindChallengesAsync(specification))
            {
                await challenge.SynchronizeAsync(_matchReferencesFactory, _matchStatsFactory);
            }

            await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);
        }
    }
}
