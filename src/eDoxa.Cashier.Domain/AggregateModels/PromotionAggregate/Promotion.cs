// Filename: Promotion.cs
// Date Created: 2020-01-20
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate
{
    public sealed class Promotion : Entity<PromotionId>
    {
        private HashSet<PromotionRecipient> _recipients = new HashSet<PromotionRecipient>();

        public Promotion(
            string promotionalCode,
            ICurrency currency,
            TimeSpan duration,
            IDateTimeProvider expiredAt
        )
        {
            PromotionalCode = promotionalCode.ToUpperInvariant();
            Amount = currency.Amount;
            Currency = currency.Type;
            Duration = duration;
            ExpiredAt = expiredAt.DateTime;
            CanceledAt = null;
        }

        public string PromotionalCode { get; }

        public decimal Amount { get; }

        public Currency Currency { get; }

        public TimeSpan Duration { get; }

        public DateTime ExpiredAt { get; }

        public DateTime? CanceledAt { get; private set; }

        public IReadOnlyCollection<PromotionRecipient> Recipients => _recipients;

        public void Redeem(PromotionRecipient promotionRecipient)
        {
            if (!this.CanRedeem())
            {
                throw new InvalidOperationException();
            }

            _recipients.Add(promotionRecipient);

            //this.AddDomainEvent();
        }

        public bool CanRedeem()
        {
            return true;
        }

        public void Cancel(IDateTimeProvider canceledAt)
        {
            if (!this.CanCancel())
            {
                throw new InvalidOperationException();
            }

            CanceledAt = canceledAt.DateTime;
        }

        public bool CanCancel()
        {
            return true;
        }
    }
}
