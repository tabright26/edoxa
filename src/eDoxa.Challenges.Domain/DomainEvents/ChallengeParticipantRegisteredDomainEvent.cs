// Filename: ChallengeParticipantRegisteredDomainEvent.cs
// Date Created: 2019-11-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Challenges.Domain.DomainEvents
{
    public sealed class ChallengeParticipantRegisteredDomainEvent : IDomainEvent
    {
        public ChallengeParticipantRegisteredDomainEvent(ChallengeId challengeId, ParticipantId participantId, UserId userId)
        {
            ChallengeId = challengeId;
            ParticipantId = participantId;
            UserId = userId;
        }

        public ChallengeId ChallengeId { get; }

        public ParticipantId ParticipantId { get; }

        public UserId UserId { get; }
    }
}
