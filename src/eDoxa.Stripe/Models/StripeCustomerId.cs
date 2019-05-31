// Filename: StripeCustomerId.cs
// Date Created: 2019-05-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Stripe.Abstractions;

namespace eDoxa.Stripe.Models
{
    [TypeConverter(typeof(StripeIdConverter))]
    public sealed class StripeCustomerId : StripeId<StripeCustomerId>
    {
        private const string Prefix = "cus";

        public StripeCustomerId(string customerId) : base(customerId, Prefix)
        {
        }
    }
}
