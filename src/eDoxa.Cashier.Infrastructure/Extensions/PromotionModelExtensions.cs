// Filename: PromotionModelExtensions.cs
// Date Created: 2020-01-22
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Infrastructure.Extensions
{
    public static class PromotionModelExtensions
    {
        public static Promotion ToEntity(this PromotionModel promotionModel)
        {
            var promotion = new Promotion(
                promotionModel.PromotionalCode,
                Currency.FromValue(promotionModel.Currency).From(promotionModel.Amount),
                TimeSpan.FromTicks(promotionModel.Duration),
                new DateTimeProvider(promotionModel.ExpiredAt));

            promotion.SetEntityId(promotionModel.Id);

            foreach (var recipient in promotionModel.Recipients)
            {
                promotion.Redeem(recipient.ToEntity());
            }

            if (promotionModel.CanceledAt.HasValue)
            {
                promotion.Cancel(new DateTimeProvider(promotionModel.CanceledAt.Value));
            }

            promotion.ClearDomainEvents();

            return promotion;
        }
    }
}
