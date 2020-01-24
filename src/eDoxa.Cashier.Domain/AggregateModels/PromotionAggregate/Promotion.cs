﻿// Filename: Promotion.cs
// Date Created: 2020-01-20
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Cashier.Domain.DomainEvents;
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

        public void Redeem(PromotionRecipient recipient)
        {
            if (!this.CanRedeem(recipient))
            {
                throw new InvalidOperationException();
            }

            _recipients.Add(recipient);

            this.AddDomainEvent(new PromotionRedeemedDomainEvent(recipient.User.Id, Id, Currency, Amount));
        }

        private bool CanRedeem(PromotionRecipient recipient)
        {
            return this.IsActive() && !this.IsRedeemBy(recipient);
        }

        public void Cancel(IDateTimeProvider canceledAt)
        {
            if (!this.CanCancel())
            {
                throw new InvalidOperationException();
            }

            CanceledAt = canceledAt.DateTime;
        }

        private bool CanCancel()
        {
            return !this.IsExpired() && !this.IsCanceled();
        }

        public bool IsActive()
        {
            var utcNow = DateTime.UtcNow;

            return ExpiredAt - Duration <= utcNow && utcNow < ExpiredAt && !this.IsExpired() && !this.IsCanceled();
        }

        public bool IsExpired()
        {
            return IsExpired(ExpiredAt);
        }

        public static bool IsExpired(DateTime expiredAt)
        {
            return expiredAt < DateTime.UtcNow;
        }

        public bool IsRedeemBy(PromotionRecipient recipient)
        {
            return Recipients.Any(x => x.User.Id == recipient.User.Id);
        }

        public static bool IsCanceled(DateTime? canceledAt)
        {
            return canceledAt.HasValue;
        }

        public bool IsCanceled()
        {
            return IsCanceled(CanceledAt);
        }
    }
}