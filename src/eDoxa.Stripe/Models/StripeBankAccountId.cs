// Filename: StripeBankAccountId.cs
// Date Created: 2019-06-01
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
    public sealed class StripeBankAccountId : StripeId<StripeBankAccountId>
    {
        private const string Prefix = "ba";

        public StripeBankAccountId(string bankAccountId) : base(bankAccountId, Prefix)
        {
        }
    }
}
