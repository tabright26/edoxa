// Filename: PromotionRecipientExtensions.cs
// Date Created: 2020-01-22
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate;
using eDoxa.Cashier.Infrastructure.Models;

namespace eDoxa.Cashier.Infrastructure.Extensions
{
    public static class PromotionRecipientExtensions
    {
        public static PromotionRecipientModel ToModel(this PromotionRecipient recipient)
        {
            return new PromotionRecipientModel
            {
                UserId = recipient.User.Id,
                RedeemedAt = recipient.RedeemedAt
            };
        }
    }
}
