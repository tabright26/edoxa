// Filename: PromotionRecipientModelExtensions.cs
// Date Created: 2020-01-22
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Infrastructure.Extensions
{
    public static class PromotionRecipientModelExtensions
    {
        public static PromotionRecipient ToEntity(this PromotionRecipientModel promotionRecipientModel)
        {
            var user = new User(promotionRecipientModel.UserId.ConvertTo<UserId>());

            return new PromotionRecipient(user, new DateTimeProvider(promotionRecipientModel.RedeemedAt));
        }
    }
}
