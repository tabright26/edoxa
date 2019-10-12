// Filename: StripeReference.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Payment.Domain.Stripe.Models
{
    public sealed class StripeReference
    {
        public StripeReference(UserId userId, string customerId, string accountId) : this()
        {
            UserId = userId;
            CustomerId = customerId;
            AccountId = accountId;
        }

        private StripeReference()
        {
            // Required by EF Core.
        }

        public UserId UserId { get; private set; }

        public string CustomerId { get; private set; }

        public string AccountId { get; private set; }
    }
}
