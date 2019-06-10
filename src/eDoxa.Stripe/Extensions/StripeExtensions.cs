// Filename: AccountExtensions.cs
// Date Created: 2019-06-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Stripe.Models;

using Stripe;

namespace eDoxa.Stripe.UnitTests.Extensions
{
    public static class StripeExtensions
    {
        public static StripeConnectAccountId ToStripeId(this Account account)
        {
            return new StripeConnectAccountId(account.Id);
        }

        public static StripeCustomerId ToStripeId(this Customer customer)
        {
            return new StripeCustomerId(customer.Id);
        }

        public static StripeBankAccountId ToStripeId(this BankAccount bankAccount)
        {
            return new StripeBankAccountId(bankAccount.Id);
        }

        public static StripeCardId ToStripeId(this Card card)
        {
            return new StripeCardId(card.Id);
        }
    }
}
