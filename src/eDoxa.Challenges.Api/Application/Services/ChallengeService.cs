// Filename: ChallengeService.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.Domain.Services;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.Extensions.Logging;

namespace eDoxa.Challenges.Api.Application.Services
{
    public sealed class ChallengeService : IChallengeService
    {
        private readonly IChallengeRepository _challengeRepository;
        private readonly ILogger _logger;

        public ChallengeService(IChallengeRepository challengeRepository, ILogger<ChallengeService> logger)
        {
            _challengeRepository = challengeRepository;
            _logger = logger;
        }

        public async Task<IChallenge?> FindChallengeAsync(ChallengeId challengeId)
        {
            return await _challengeRepository.FindChallengeAsync(challengeId);
        }

        public async Task<IDomainValidationResult> CreateChallengeAsync(
            ChallengeName name,
            Game game,
            BestOf bestOf,
            Entries entries,
            ChallengeDuration duration,
            IDateTimeProvider createAt,
            Scoring scoring,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult();

            if (result.IsValid)
            {
                var challenge = new Challenge(
                    new ChallengeId(),
                    name,
                    game,
                    bestOf,
                    entries,
                    new ChallengeTimeline(createAt, duration),
                    scoring);

                _challengeRepository.Create(challenge);

                await _challengeRepository.CommitAsync(true, cancellationToken);

                result.AddEntityToMetadata(challenge);
            }

            return result;
        }

        public async Task<IDomainValidationResult> RegisterChallengeParticipantAsync(
            IChallenge challenge,
            UserId userId,
            ParticipantId participantId,
            PlayerId playerId,
            IDateTimeProvider registeredAt,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult();

            if (challenge.SoldOut)
            {
                result.AddDomainValidationError("_error", "The challenge was sold out.");
            }

            if (challenge.ParticipantExists(userId))
            {
                result.AddDomainValidationError("_error", "The user already is registered.");
            }

            if (result.IsValid)
            {
                var participant = new Participant(participantId, userId, playerId, registeredAt);

                challenge.Register(participant);

                if (challenge.SoldOut)
                {
                    challenge.Start(registeredAt);
                }

                await _challengeRepository.CommitAsync(true, cancellationToken);

                result.AddEntityToMetadata(participant);
            }

            return result;
        }

        public async Task SynchronizeChallengesAsync(Game game, IDateTimeProvider synchronizedAt, CancellationToken cancellationToken = default)
        {
            var challengeInProgress = await _challengeRepository.FetchChallengesAsync(game, ChallengeState.InProgress);

            var challengeEnded = await _challengeRepository.FetchChallengesAsync(game, ChallengeState.Ended);

            foreach (var challenge in challengeInProgress.Union(challengeEnded))
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

        public async Task<IDomainValidationResult> SynchronizeChallengeAsync(
            IChallenge challenge,
            IDateTimeProvider synchronizedAt,
            CancellationToken cancellationToken = default
        )
        {
            //if (!challenge.CanSynchronize())
            //{
            //    return DomainValidationResult.Failure("_error", "Challenge wasn't synchronized due to is current state.");
            //}

            //challenge.Synchronize(synchronizedAt);

            //foreach (var participant in challenge.Participants)
            //{
            //    if (challenge.CanSynchronize(participant))
            //    {
            //        try
            //        {
            //            var matches = await _gamesHttpClient.GetChallengeMatchesAsync(
            //                challenge.Game,
            //                participant.PlayerId,
            //                participant.SynchronizedAt ?? challenge.Timeline.StartedAt,
            //                challenge.Timeline.EndedAt);

            //            participant.Snapshot(
            //                matches.Select(match => new Match(challenge.Scoring.Map(match.Stats), new GameUuid(match.GameUuid))).ToList(),
            //                synchronizedAt);

            //            await _challengeRepository.CommitAsync(true, cancellationToken);
            //        }
            //        catch (RpcException exception)
            //        {
            //            _logger.LogError(
            //                exception,
            //                $"Synchronize challenge ({challenge}) thrown an exception when fetching participant ({participant}) matches.");
            //        }
            //    }
            //}

            //await _challengeRepository.CommitAsync(true, cancellationToken);

            //if (challenge.CanClose())
            //{
            //    challenge.Close(synchronizedAt);

            //    await _challengeRepository.CommitAsync(true, cancellationToken);
            //}

            //return new DomainValidationResult();

            return await Task.FromResult(new DomainValidationResult());
        }

        public async Task DeleteChallengeAsync(IChallenge challenge, CancellationToken cancellationToken = default)
        {
            _challengeRepository.Delete(challenge);

            await _challengeRepository.CommitAsync(true, cancellationToken);
        }
    }
}
