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
            CurrencyType currencyType,
            decimal amount
        )
        {
            UserId = userId;
            PromotionId = promotionId;
            CurrencyType = currencyType;
            Amount = amount;
        }

        public UserId UserId { get; }

        public PromotionId PromotionId { get; }

        public CurrencyType CurrencyType { get; }

        public decimal Amount { get; }
    }
}
