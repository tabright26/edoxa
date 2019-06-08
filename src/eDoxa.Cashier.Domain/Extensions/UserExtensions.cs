// Filename: UserExtensions.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Stripe.Models;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Domain.Extensions
{
    public static class UserExtensions
    {
        public static StripeConnectAccountId GetConnectAccountId(this User user)
        {
            return new StripeConnectAccountId(user.ConnectAccountId);
        }

        public static StripeCustomerId GetCustomerId(this User user)
        {
            return new StripeCustomerId(user.CustomerId);
        }

        [CanBeNull]
        public static StripeBankAccountId GetBankAccountId(this User user)
        {
            return user.BankAccountId != null ? new StripeBankAccountId(user.BankAccountId) : null;
        }
    }
}
