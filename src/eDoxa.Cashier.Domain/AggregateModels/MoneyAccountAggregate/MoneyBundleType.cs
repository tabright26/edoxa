// Filename: MoneyBundleType.cs
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

namespace eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate
{
    [TypeConverter(typeof(EnumerationConverter))]
    public sealed class MoneyBundleType : Enumeration<MoneyBundleType>
    {
        public static readonly MoneyBundleType Ten = new MoneyBundleType(1 << 0, nameof(Ten));
        public static readonly MoneyBundleType Twenty = new MoneyBundleType(1 << 1, nameof(Twenty));
        public static readonly MoneyBundleType Fifty = new MoneyBundleType(1 << 2, nameof(Fifty));
        public static readonly MoneyBundleType OneHundred = new MoneyBundleType(1 << 3, nameof(OneHundred));
        public static readonly MoneyBundleType FiveHundred = new MoneyBundleType(1 << 4, nameof(FiveHundred));

        private MoneyBundleType(int value, string name) : base(value, name)
        {
        }
    }
}