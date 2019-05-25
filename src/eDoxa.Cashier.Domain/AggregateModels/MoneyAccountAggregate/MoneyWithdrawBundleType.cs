// Filename: MoneyWithdrawBundleType.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class MoneyWithdrawBundleType : Enumeration<MoneyWithdrawBundleType>
    {
        public static readonly MoneyWithdrawBundleType Fifty = new MoneyWithdrawBundleType(1 << 0, nameof(Fifty));
        public static readonly MoneyWithdrawBundleType OneHundred = new MoneyWithdrawBundleType(1 << 1, nameof(OneHundred));
        public static readonly MoneyWithdrawBundleType TwoHundred = new MoneyWithdrawBundleType(1 << 2, nameof(TwoHundred));

        private MoneyWithdrawBundleType(int value, string name) : base(value, name)
        {
        }
    }
}
