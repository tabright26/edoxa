// Filename: IChallengeService.cs
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
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Challenges.Domain.Services
{
    public interface IChallengeService
    {
        Task<IChallenge> FindChallengeAsync(ChallengeId challengeId);

        Task<bool> ChallengeExistsAsync(ChallengeId challengeId);

        Task<DomainValidationResult<IChallenge>> CreateChallengeAsync(
            ChallengeName name,
            Game game,
            BestOf bestOf,
            Entries entries,
            ChallengeDuration duration,
            IDateTimeProvider createAt,
            Scoring scoring,
            CancellationToken cancellationToken = default
        );

        Task<DomainValidationResult<Participant>> RegisterChallengeParticipantAsync(
            IChallenge challenge,
            UserId userId,
            ParticipantId participantId,
            PlayerId playerId,
            IDateTimeProvider registeredAt,
            CancellationToken cancellationToken = default
        );

        Task<DomainValidationResult<IChallenge>> CloseChallengeAsync(IChallenge challenge, IDateTimeProvider provider, CancellationToken cancellationToken = default);

        Task<DomainValidationResult<IChallenge>> SynchronizeChallengeAsync(
            IChallenge challenge,
            IDateTimeProvider synchronizedAt,
            CancellationToken cancellationToken = default
        );

        Task<DomainValidationResult<IChallenge>> DeleteChallengeAsync(IChallenge challenge, CancellationToken cancellationToken = default);

        Task<DomainValidationResult<Participant>> SnapshotChallengeParticipantAsync(
            IChallenge challenge,
            PlayerId gamePlayerId,
            IDateTimeProvider synchronizedAt,
            Func<IScoring, IImmutableSet<Match>> snapshotMatches,
            CancellationToken cancellationToken = default
        );
    }
}
