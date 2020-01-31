// Filename: ChallengeParticipantPayoutDomainEvent.cs
// Date Created: 2020-01-30
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.DomainEvents
{
    public sealed class ChallengeParticipantPayoutDomainEvent : IDomainEvent
    {
        public ChallengeParticipantPayoutDomainEvent(UserId userId, ICurrency currency)
        {
            UserId = userId;
            Currency = currency;
        }

        public UserId UserId { get; }

        public ICurrency Currency { get; }
    }
}
