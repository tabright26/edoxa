// Filename: BankAccountId.cs
// Date Created: 2019-05-10
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
    public sealed class BankAccountId : StripeId<BankAccountId>
    {
        private const string Prefix = "ba";

        public BankAccountId(string bankAccountId) : base(bankAccountId, Prefix)
        {
        }

        public static bool IsValid(string bankAccountId)
        {
            return StripeId.IsValid(bankAccountId, Prefix);
        }
    }
}