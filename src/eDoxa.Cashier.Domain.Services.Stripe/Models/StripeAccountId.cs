// Filename: StripeAccountId.cs
// Date Created: 2019-05-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;

namespace eDoxa.Cashier.Domain.Services.Stripe.Models
{
    [TypeConverter(typeof(StripeIdConverter))]
    public sealed class StripeAccountId : StripeId<StripeAccountId>
    {
        private const string Prefix = "acct";

        public StripeAccountId(string accountId) : base(accountId, Prefix)
        {
        }

        public static bool IsValid(string accountId)
        {
            return StripeId.IsValid(accountId, Prefix);
        }
    }
}