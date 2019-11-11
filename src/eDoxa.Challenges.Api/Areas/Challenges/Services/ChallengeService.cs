// Filename: ChallengeService.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Areas.Challenges.Services.Abstractions;
using eDoxa.Challenges.Api.HttpClients;
using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

using Microsoft.Extensions.Logging;

using Refit;

namespace eDoxa.Challenges.Api.Areas.Challenges.Services
{
    public sealed class ChallengeService : IChallengeService
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly IGamesHttpClient _gamesHttpClient;
        private readonly ILogger _logger;

        public ChallengeService(IChallengeRepository challengeRepository, IGamesHttpClient gamesHttpClient, ILogger<ChallengeService> logger)
        {
            _challengeRepository = challengeRepository;
            _gamesHttpClient = gamesHttpClient;
            _logger = logger;
        }

        public async Task<IChallenge?> FindChallengeAsync(ChallengeId challengeId)
        {
            return await _challengeRepository.FindChallengeAsync(challengeId);
        }

        public async Task<ValidationResult> CreateChallengeAsync(
            ChallengeId id,
            ChallengeName name,
            Game game,
            BestOf bestOf,
            Entries entries,
            ChallengeDuration duration,
            IDateTimeProvider createAt,
            CancellationToken cancellationToken = default
        )
        {
            var scoring = await _gamesHttpClient.GetChallengeScoringAsync(game);

            var challenge = new Challenge(
                id,
                name,
                game,
                bestOf,
                entries,
                new ChallengeTimeline(createAt, duration),
                new Scoring(scoring));

            _challengeRepository.Create(challenge);

            await _challengeRepository.CommitAsync(cancellationToken);

            return new ValidationResult();
        }

        public async Task<ValidationResult> RegisterChallengeParticipantAsync(
            IChallenge challenge,
            ParticipantId participantId,
            UserId userId,
            PlayerId playerId,
            IDateTimeProvider registeredAt,
            CancellationToken cancellationToken = default
        )
        {
            if (challenge.SoldOut)
            {
                return new ValidationFailure("_error", "The challenge was sold out.").ToResult();
            }

            if (challenge.ParticipantExists(userId))
            {
                return new ValidationFailure("_error", "The user already is registered.").ToResult();
            }

            var participant = new Participant(participantId, userId, playerId, registeredAt);

            challenge.Register(participant);

            if (challenge.SoldOut)
            {
                challenge.Start(registeredAt);
            }

            await _challengeRepository.CommitAsync(cancellationToken);

            return new ValidationResult();
        }

        public async Task SynchronizeChallengesAsync(Game game, IDateTimeProvider synchronizedAt, CancellationToken cancellationToken = default)
        {
            foreach (var challenge in await _challengeRepository.FetchChallengesAsync(game, ChallengeState.InProgress))
            {
                var result = await this.SynchronizeChallengeAsync(challenge, synchronizedAt, cancellationToken);

                if (!result.IsValid)
                {
                    foreach (var error in result.Errors)
                    {
                        _logger.LogError(error.ErrorMessage);
                    }
                }
            }
        }

        public async Task<ValidationResult> SynchronizeChallengeAsync(
            IChallenge challenge,
            IDateTimeProvider synchronizedAt,
            CancellationToken cancellationToken = default
        )
        {
            if (!challenge.CanSynchronize())
            {
                return new ValidationFailure("_error", "Challenge wasn't synchronized due to is current state.").ToResult();
            }

            challenge.Synchronize(synchronizedAt);

            foreach (var participant in challenge.Participants)
            {
                try
                {
                    var matches = await _gamesHttpClient.GetChallengeMatchesAsync(
                        challenge.Game,
                        participant.PlayerId,
                        participant.SynchronizedAt ?? challenge.Timeline.StartedAt,
                        challenge.Timeline.EndedAt);

                    participant.Snapshot(
                        matches.Select(match => new Match(challenge.Scoring.Map(match.Stats), new GameUuid(match.GameUuid))).ToList(),
                        synchronizedAt);

                    await _challengeRepository.CommitAsync(cancellationToken);
                }
                catch (ApiException exception)
                {
                    _logger.LogError(
                        exception,
                        $"Synchronize challenge ({challenge}) thrown an exception when fetching participant ({participant}) matches.");
                }
            }

            await _challengeRepository.CommitAsync(cancellationToken);

            return new ValidationResult();
        }

        public async Task DeleteChallengeAsync(IChallenge challenge, CancellationToken cancellationToken = default)
        {
            _challengeRepository.Delete(challenge);

            await _challengeRepository.CommitAsync(cancellationToken);
        }
    }
}
