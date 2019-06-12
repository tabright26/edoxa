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
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.Abstractions.Factories;
using eDoxa.Arena.Challenges.Domain.Abstractions.Repositories;
using eDoxa.Arena.Challenges.Domain.Abstractions.Services;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Arena.Challenges.Domain.Specifications;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Specifications;

namespace eDoxa.Arena.Challenges.Domain.Services
{
    public sealed class ChallengeService : IChallengeService
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly IMatchReferencesFactory _matchReferencesFactory;
        private readonly IMatchStatsFactory _matchStatsFactory;

        public ChallengeService(
            IChallengeRepository challengeRepository,
            IMatchReferencesFactory matchReferencesFactory,
            IMatchStatsFactory matchStatsFactory
        )
        {
            _challengeRepository = challengeRepository;
            _matchReferencesFactory = matchReferencesFactory;
            _matchStatsFactory = matchStatsFactory;
        }

        public async Task<Participant> RegisterParticipantAsync(
            ChallengeId challengeId,
            UserId userId,
            Func<Game, UserGameReference> funcUserGameReference,
            CancellationToken cancellationToken = default
        )
        {
            var challenge = await _challengeRepository.FindChallengeAsync(challengeId);

            var userGameReference = funcUserGameReference(challenge.Game);

            var participant = challenge.RegisterParticipant(userId, userGameReference);

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

        public async Task<IEnumerable<Challenge>> FakeChallengesAsync(
            int count,
            int? seed = null,
            Game game = null,
            ChallengeState state = null,
            CurrencyType entryFeeCurrency = null,
            CancellationToken cancellationToken = default
        )
        {
            var challengeFaker = new ChallengeFaker();

            if (seed.HasValue)
            {
                challengeFaker.UseSeed(seed.Value);
            }
            
            var challenges = challengeFaker.FakeChallenges(count, game, state, entryFeeCurrency);

            _challengeRepository.Create(challenges);

            await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            return challenges;
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
