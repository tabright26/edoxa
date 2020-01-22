// Filename: PromotionRedeemedDomainEvent.cs
// Date Created: 2020-01-21
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.DomainEvents
{
    public sealed class PromotionRedeemedDomainEvent : IDomainEvent
    {
        public PromotionRedeemedDomainEvent(PromotionRecipient recipient)
        {
            Recipient = recipient;
        }

        public PromotionRecipient Recipient { get; }
    }
}
