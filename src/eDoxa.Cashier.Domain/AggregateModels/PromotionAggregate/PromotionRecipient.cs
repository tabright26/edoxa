// Filename: Recipient.cs
// Date Created: 2020-01-20
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate
{
    public sealed class PromotionRecipient : ValueObject
    {
        public PromotionRecipient(User user, IDateTimeProvider redeemedAt)
        {
            User = user;
            RedeemedAt = redeemedAt.DateTime;
        }

        public User User { get; }

        public DateTime RedeemedAt { get; }

        protected override IEnumerable<object?> GetAtomicValues()
        {
            yield return User;
            yield return RedeemedAt;
        }

        public override string ToString()
        {
            return User.Id;
        }
    }
}
