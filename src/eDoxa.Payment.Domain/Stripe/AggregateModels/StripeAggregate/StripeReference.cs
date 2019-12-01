// Filename: StripeReference.cs
// Date Created: 2019-10-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Payment.Domain.Stripe.AggregateModels.StripeAggregate
{
    public sealed class StripeReference
    {
        public StripeReference(UserId userId, string customerId, string accountId) : this()
        {
            UserId = userId;
            CustomerId = customerId;
            AccountId = accountId;
        }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        private StripeReference()
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        {
            // Required by EF Core.
        }

        public UserId UserId { get; private set; }

        public string CustomerId { get; private set; }

        public string AccountId { get; private set; }
    }
}
