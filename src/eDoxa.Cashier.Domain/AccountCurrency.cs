// Filename: AccountCurrency.cs
// Date Created: 2019-05-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class AccountCurrency : Enumeration<AccountCurrency>
    {
        public static readonly AccountCurrency Money = new AccountCurrency(1 << 0, nameof(Money));
        public static readonly AccountCurrency Token = new AccountCurrency(1 << 1, nameof(Token));

        private AccountCurrency(int value, string name) : base(value, name)
        {
        }
    }
}
