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
        public static Promotion ToEntity(this PromotionModel model)
        {
            var promotion = new Promotion(
                model.PromotionalCode,
                CurrencyType.FromValue(model.Currency).ToCurrency(model.Amount),
                TimeSpan.FromTicks(model.Duration),
                new DateTimeProvider(model.ExpiredAt));

            promotion.SetEntityId(model.Id);

            foreach (var recipient in model.Recipients)
            {
                promotion.Redeem(recipient.ToEntity());
            }

            if (model.CanceledAt.HasValue)
            {
                promotion.Cancel(new DateTimeProvider(model.CanceledAt.Value));
            }

            promotion.ClearDomainEvents();

            return promotion;
        }
    }
}
