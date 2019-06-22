// Filename: ChallengeService.cs
// Date Created: 2019-06-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.Abstractions.Factories;
using eDoxa.Arena.Challenges.Domain.Abstractions.Repositories;
using eDoxa.Arena.Challenges.Domain.Abstractions.Services;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.ValueObjects;

namespace eDoxa.Arena.Challenges.Api.Application.Services
{
    public sealed class ChallengeService : IChallengeService
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly IGameMatchIdsFactory _gameMatchIdsFactory;
        private readonly IMatchStatsFactory _matchStatsFactory;

        public ChallengeService(
            IChallengeRepository challengeRepository,
            IGameMatchIdsFactory gameMatchIdsFactory,
            IMatchStatsFactory matchStatsFactory
        )
        {
            _challengeRepository = challengeRepository;
            _gameMatchIdsFactory = gameMatchIdsFactory;
            _matchStatsFactory = matchStatsFactory;
        }

        public async Task<Participant> RegisterParticipantAsync(
            ChallengeId challengeId,
            UserId userId,
            Func<ChallengeGame, GameAccountId> funcUserGameReference,
            CancellationToken cancellationToken = default
        )
        {
            var challenge = await _challengeRepository.FindChallengeAsync(challengeId);

            var userGameReference = funcUserGameReference(challenge.Game);

            var participant = challenge.Register(new Participant(userId, userGameReference, new UtcNowDateTimeProvider()));

            await _challengeRepository.CommitAsync(cancellationToken);

            return participant;
        }

        public async Task CloseAsync(CancellationToken cancellationToken = default)
        {
            //var specification = SpecificationFactory.Instance.Create<Challenge>().And(new ChallengeOfStateSpecification(ChallengeState.Ended));

            //var challenges = await _challengeRepository.FindChallengesAsync(specification);

            //challenges.ForEach(
            //    challenge => challenge.TryClose(
            //        async () =>
            //        {
            //            await challenge.SynchronizeAsync(_matchReferencesFactory, _matchStatsFactory);

            //            challenge.DistributeParticipantPrizes();
            //        }
            //    )
            //);

            //await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            await Task.CompletedTask;
        }

        public async Task<IEnumerable<IChallenge>> FakeChallengesAsync(
            int count,
            int seed,
            ChallengeGame game = null,
            ChallengeState state = null,
            CurrencyType entryFeeCurrency = null,
            CancellationToken cancellationToken = default
        )
        {
            var challengeFaker = new ChallengeFaker(game, state, entryFeeCurrency);

            challengeFaker.UseSeed(seed);

            var challenges = challengeFaker.GenerateEntities(count).ToList();

            _challengeRepository.Create(challenges);

            await _challengeRepository.CommitAsync(cancellationToken);

            return challenges;
        }

        public async Task SynchronizeAsync(ChallengeGame game, CancellationToken cancellationToken = default)
        {
            //var specification = SpecificationFactory.Instance.Create<Challenge>()
            //    .And(new ChallengeForGameSpecification(game))
            //    .And(new ChallengeLastSyncMoreThanSpecification(TimeSpan.FromHours(1)))
            //    .And(new ChallengeOfStateSpecification(ChallengeState.Inscription).Not());

            //foreach (var challenge in await _challengeRepository.FindChallengesAsync(specification))
            //{
            //    await challenge.SynchronizeAsync(_matchReferencesFactory, _matchStatsFactory);
            //}

            //await _challengeRepository.UnitOfWork.CommitAndDispatchDomainEventsAsync(cancellationToken);

            await Task.CompletedTask;
        }
    }
}
