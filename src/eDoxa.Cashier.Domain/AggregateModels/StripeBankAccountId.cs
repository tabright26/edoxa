// Filename: StripeBankAccountId.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Cashier.Domain.Abstractions;

namespace eDoxa.Cashier.Domain.AggregateModels
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
