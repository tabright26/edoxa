// Filename: PromotionRedeemedDomainEvent.cs
// Date Created: 2020-01-21
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.DomainEvents
{
    public sealed class PromotionRedeemedDomainEvent : IDomainEvent
    {
        public PromotionRedeemedDomainEvent(
            UserId userId,
            PromotionId promotionId,
            Currency currency,
            decimal amount
        )
        {
            UserId = userId;
            PromotionId = promotionId;
            Currency = currency;
            Amount = amount;
        }

        public UserId UserId { get; }

        public PromotionId PromotionId { get; }

        public Currency Currency { get; }

        public decimal Amount { get; }
    }
}
