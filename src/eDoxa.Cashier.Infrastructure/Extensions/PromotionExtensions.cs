// Filename: PromotionExtensions.cs
// Date Created: 2020-01-22
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate;
using eDoxa.Cashier.Infrastructure.Models;

namespace eDoxa.Cashier.Infrastructure.Extensions
{
    public static class PromotionExtensions
    {
        public static PromotionModel ToModel(this Promotion promotion)
        {
            return new PromotionModel
            {
                Id = promotion.Id,
                PromotionalCode = promotion.PromotionalCode,
                Amount = promotion.Amount,
                Duration = promotion.Duration.Ticks,
                Currency = promotion.CurrencyType.Value,
                CanceledAt = promotion.CanceledAt,
                ExpiredAt = promotion.ExpiredAt,
                Recipients = promotion.Recipients.Select(recipient => recipient.ToModel()).ToList(),
                DomainEvents = promotion.DomainEvents.ToList()
            };
        }
    }
}
