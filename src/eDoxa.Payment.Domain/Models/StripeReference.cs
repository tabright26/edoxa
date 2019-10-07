// Filename: StripeReference.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Payment.Domain.Models
{
    public sealed class StripeReference
    {
        public StripeReference(UserId userId, string customerId, string connectAccountId) : this()
        {
            UserId = userId;
            CustomerId = customerId;
            ConnectAccountId = connectAccountId;
        }

        private StripeReference()
        {
            // Required by EF Core.
        }

        public UserId UserId { get; private set; }

        public string CustomerId { get; private set; }

        public string ConnectAccountId { get; private set; }
    }
}
