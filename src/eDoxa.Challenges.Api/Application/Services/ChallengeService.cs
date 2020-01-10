// Filename: ChallengeService.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.Domain.Services;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Challenges.Api.Application.Services
{
    public sealed class ChallengeService : IChallengeService
    {
        private readonly IChallengeRepository _challengeRepository;

        public ChallengeService(IChallengeRepository challengeRepository)
        {
            _challengeRepository = challengeRepository;
        }

        public async Task<IChallenge> FindChallengeAsync(ChallengeId challengeId)
        {
            return await _challengeRepository.FindChallengeAsync(challengeId);
        }

        public async Task<bool> ChallengeExistsAsync(ChallengeId challengeId)
        {
            return await _challengeRepository.ChallengeExistsAsync(challengeId);
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
                result.AddFailedPreconditionError("The challenge was sold out.");
            }

            if (challenge.ParticipantExists(userId))
            {
                result.AddFailedPreconditionError("The user already is registered.");
            }

            if (result.IsValid)
            {
                var participant = new Participant(
                    participantId,
                    userId,
                    playerId,
                    registeredAt);

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

        public async Task<IDomainValidationResult> SynchronizeChallengeAsync(
            IChallenge challenge,
            IDateTimeProvider synchronizedAt,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult();

            if (!challenge.CanSynchronize())
            {
                result.AddFailedPreconditionError("Challenge wasn't synchronized due to is current state.");
            }

            if (result.IsValid)
            {
                challenge.Synchronize(synchronizedAt);

                await _challengeRepository.CommitAsync(true, cancellationToken);

                result.AddEntityToMetadata(challenge);
            }

            return result;
        }

        public async Task<IDomainValidationResult> CloseChallengeAsync(
            IChallenge challenge,
            IDateTimeProvider provider,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult();

            if (!challenge.CanClose())
            {
                result.AddFailedPreconditionError("Challenge can't be closed.");
            }

            if (result.IsValid)
            {
                challenge.Close(provider);

                await _challengeRepository.CommitAsync(true, cancellationToken);

                result.AddEntityToMetadata(challenge);
            }

            return result;
        }

        public async Task<IDomainValidationResult> DeleteChallengeAsync(IChallenge challenge, CancellationToken cancellationToken = default)
        {
            var result = new DomainValidationResult();

            if (!challenge.CanDelete())
            {
                result.AddFailedPreconditionError("Challenge can't be deleted.");
            }

            if (result.IsValid)
            {
                _challengeRepository.Delete(challenge);

                await _challengeRepository.CommitAsync(true, cancellationToken);

                result.AddEntityToMetadata(challenge);
            }

            return result;
        }

        public async Task<IDomainValidationResult> SnapshotChallengeParticipantAsync(
            IChallenge challenge,
            PlayerId gamePlayerId,
            IDateTimeProvider synchronizedAt,
            Func<IScoring, IImmutableSet<Match>> snapshotMatches,
            CancellationToken cancellationToken = default
        )
        {
            var result = new DomainValidationResult();

            if (!challenge.ParticipantExists(gamePlayerId))
            {
                result.AddFailedPreconditionError("Participant doesn't exists.");
            }

            if (result.IsValid)
            {
                var participant = challenge.FindParticipant(gamePlayerId);

                var matches = snapshotMatches(challenge.Scoring);

                participant.Snapshot(matches, synchronizedAt);

                await _challengeRepository.CommitAsync(true, cancellationToken);

                result.AddEntityToMetadata(participant);
            }

            return result;
        }
    }
}
