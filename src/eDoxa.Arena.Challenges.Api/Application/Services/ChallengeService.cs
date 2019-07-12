// Filename: ChallengeService.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Arena.Challenges.Api.Application.Services
{
    public sealed class ChallengeService : IChallengeService
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly IGameReferencesFactory _gameReferencesFactory;
        private readonly IMatchFactory _matchFactory;
        private readonly IIdentityService _identityService;

        public ChallengeService(
            IChallengeRepository challengeRepository,
            IGameReferencesFactory gameReferencesFactory,
            IMatchFactory matchFactory,
            IIdentityService identityService
        )
        {
            _challengeRepository = challengeRepository;
            _gameReferencesFactory = gameReferencesFactory;
            _matchFactory = matchFactory;
            _identityService = identityService;
        }

        public async Task FakeChallengesAsync(
            int count,
            int seed,
            ChallengeGame game = null,
            ChallengeState state = null,
            CancellationToken cancellationToken = default
        )
        {
            var challengeFaker = new ChallengeFaker(game, state);

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

        public async Task RegisterParticipantAsync(
            ChallengeId challengeId,
            UserId userId,
            IDateTimeProvider registeredAt,
            CancellationToken cancellationToken = default
        )
        {
            var challenge = await _challengeRepository.FindChallengeAsync(challengeId) ?? throw new InvalidOperationException();

            // TODO: Need validation via the resource filter.
            var gameAccountId = await _identityService.GetGameAccountIdAsync(userId, challenge.Game) ?? throw new InvalidOperationException();

            challenge.Register(new Participant(userId, gameAccountId, registeredAt));

            if (challenge.IsInscriptionCompleted())
            {
                challenge.Start(registeredAt);
            }

            await _challengeRepository.CommitAsync(cancellationToken);
        }

        public async Task SynchronizeAsync(
            ChallengeGame game,
            TimeSpan interval,
            IDateTimeProvider synchronizedAt,
            CancellationToken cancellationToken = default
        )
        {
            var challenges = await _challengeRepository.FetchChallengesAsync(game, ChallengeState.InProgress);

            foreach (var challenge in challenges.Where(challenge => challenge.SynchronizedAt + interval <= synchronizedAt.DateTime)
                .OrderByDescending(challenge => challenge.SynchronizedAt))
            {
                this.Synchronize(challenge, synchronizedAt);

                await _challengeRepository.CommitAsync(cancellationToken);
            }
        }

        public async Task CloseAsync(IDateTimeProvider closedAt, CancellationToken cancellationToken = default)
        {
            var challenges = await _challengeRepository.FetchChallengesAsync(null, ChallengeState.Ended);

            foreach (var challenge in challenges.OrderByDescending(challenge => challenge.Timeline.EndedAt))
            {
                this.Synchronize(challenge, closedAt);

                challenge.Close(closedAt);

                await _challengeRepository.CommitAsync(cancellationToken);
            }
        }

        private void Synchronize(IChallenge challenge, IDateTimeProvider synchronizedAt)
        {
            var gameReferencesAdapter = _gameReferencesFactory.CreateInstance(challenge.Game);

            var matchAdapter = _matchFactory.CreateInstance(challenge.Game);

            challenge.Synchronize(
                (gameAccountId, startedAt, closedAt) => gameReferencesAdapter.GetGameReferencesAsync(gameAccountId, startedAt, closedAt).Result,
                (gameAccountId, gameReference, scoring) => matchAdapter.GetMatchAsync(gameAccountId, gameReference, scoring, synchronizedAt).Result,
                synchronizedAt
            );
        }
    }
}
