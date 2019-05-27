// Filename: MoneyDepositBundleType.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.TypeConverters;

namespace eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate
{
    [TypeConverter(typeof(EnumerationTypeConverter<MoneyDepositBundleType>))]
    public sealed class MoneyDepositBundleType : Enumeration
    {
        public static readonly MoneyDepositBundleType Ten = new MoneyDepositBundleType(1 << 0, nameof(Ten));
        public static readonly MoneyDepositBundleType Twenty = new MoneyDepositBundleType(1 << 1, nameof(Twenty));
        public static readonly MoneyDepositBundleType Fifty = new MoneyDepositBundleType(1 << 2, nameof(Fifty));
        public static readonly MoneyDepositBundleType OneHundred = new MoneyDepositBundleType(1 << 3, nameof(OneHundred));
        public static readonly MoneyDepositBundleType FiveHundred = new MoneyDepositBundleType(1 << 4, nameof(FiveHundred));

        private MoneyDepositBundleType(int value, string name) : base(value, name)
        {
        }
    }
}
