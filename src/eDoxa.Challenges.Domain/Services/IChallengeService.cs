// Filename: IChallengeService.cs
// Date Created: 2019-10-31
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
        Task<IChallenge?> FindChallengeAsync(ChallengeId challengeId);

        Task<IDomainValidationResult> CreateChallengeAsync(
            ChallengeId id,
            ChallengeName name,
            Game game,
            BestOf bestOf,
            Entries entries,
            ChallengeDuration duration,
            IDateTimeProvider createAt,
            CancellationToken cancellationToken = default
        );

        Task<IDomainValidationResult> RegisterChallengeParticipantAsync(
            IChallenge challenge,
            ParticipantId participantId,
            UserId userId,
            PlayerId playerId,
            IDateTimeProvider registeredAt,
            CancellationToken cancellationToken = default
        );

        Task SynchronizeChallengesAsync(Game game, IDateTimeProvider synchronizedAt, CancellationToken cancellationToken = default);

        Task<IDomainValidationResult> SynchronizeChallengeAsync(IChallenge challenge, IDateTimeProvider synchronizedAt, CancellationToken cancellationToken = default);

        Task DeleteChallengeAsync(IChallenge challenge, CancellationToken cancellationToken = default);
    }
}
