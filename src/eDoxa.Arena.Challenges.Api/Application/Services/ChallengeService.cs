// Filename: ChallengeService.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Application.Fakers;
using eDoxa.Arena.Challenges.Domain;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Specifications;

namespace eDoxa.Arena.Challenges.Api.Application.Services
{
    public sealed class ChallengeService : IChallengeService
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly IGameReferencesFactory _gameReferencesFactory;
        private readonly IMatchStatsFactory _matchStatsFactory;

        public ChallengeService(IChallengeRepository challengeRepository, IGameReferencesFactory gameReferencesFactory, IMatchStatsFactory matchStatsFactory)
        {
            _challengeRepository = challengeRepository;
            _gameReferencesFactory = gameReferencesFactory;
            _matchStatsFactory = matchStatsFactory;
        }

        public async Task RegisterParticipantAsync(
            ChallengeId challengeId,
            UserId userId,
            Func<ChallengeGame, GameAccountId> funcUserGameReference,
            CancellationToken cancellationToken = default
        )
        {
            var challenge = await _challengeRepository.FindChallengeAsync(challengeId);

            var userGameReference = funcUserGameReference(challenge.Game);

            challenge.Register(new Participant(userId, userGameReference, new UtcNowDateTimeProvider()));

            await _challengeRepository.CommitAsync(cancellationToken);
        }

        public async Task CloseAsync(IDateTimeProvider closedAt, CancellationToken cancellationToken = default)
        {
            var challenges = await _challengeRepository.FetchChallengesAsync(null, ChallengeState.Ended);

            challenges.ForEach(challenge => challenge.Close(closedAt));

            await _challengeRepository.CommitAsync(cancellationToken);
        }

        public async Task FakeChallengesAsync(
            int count,
            int seed,
            ChallengeGame game = null,
            ChallengeState state = null,
            Currency entryFeeCurrency = null,
            CancellationToken cancellationToken = default
        )
        {
            var challengeFaker = new ChallengeFaker(game, state, entryFeeCurrency);

            challengeFaker.UseSeed(seed);

            var challenges = challengeFaker.Generate(count);

            foreach (var challenge in challenges)
            {
                if (await _challengeRepository.AnyChallengeAsync(challenge.Id))
                {
                    throw new InvalidOperationException("This seed was already used.");
                }
            }

            _challengeRepository.Create(challenges);

            await _challengeRepository.CommitAsync(cancellationToken);
        }

        public async Task SynchronizeAsync(
            IDateTimeProvider synchronizedAt,
            ChallengeGame game,
            CancellationToken cancellationToken = default
        )
        {
            var specification = SpecificationFactory.Instance.Create<IChallenge>();
                //.And(new LastSynchronizationMoreThanSpecification(synchronizationInterval));

            var challenges = await _challengeRepository.FetchChallengesAsync(game, ChallengeState.InProgress);

            var gameReferencesAdapter = _gameReferencesFactory.CreateInstance(game);

            var matchStatsAdapter = _matchStatsFactory.CreateInstance(game);

            foreach (var challenge in challenges.Where(specification.IsSatisfiedBy).OrderByDescending(challenge => challenge.SynchronizedAt))
            {
                challenge.Synchronize(
                    (gameAccountId, startedAt, closedAt) => gameReferencesAdapter.GetGameReferencesAsync(gameAccountId, startedAt, closedAt).Result,
                    (gameAccountId, gameReference) => matchStatsAdapter.GetMatchStatsAsync(gameAccountId, gameReference).Result,
                    synchronizedAt
                );

                await _challengeRepository.CommitAsync(cancellationToken);
            }
        }
    }
}
