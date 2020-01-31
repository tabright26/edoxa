// Filename: ChallengeClosedDomainEvent.cs
// Date Created: 2020-01-30
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.DomainEvents
{
    public sealed class ChallengeClosedDomainEvent : IDomainEvent
    {
        public ChallengeClosedDomainEvent(ChallengeId challengeId, ChallengeParticipantPayouts payouts)
        {
            ChallengeId = challengeId;
            Payouts = payouts;
        }

        public ChallengeId ChallengeId { get; }

        public ChallengeParticipantPayouts Payouts { get; }
    }
}
