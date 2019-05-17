// Filename: WithdrawalBundleType.cs
// Date Created: 2019-05-10
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
    public sealed class MoneyWithdrawalBundleType : Enumeration<MoneyWithdrawalBundleType>
    {
        public static readonly MoneyWithdrawalBundleType Fifty = new MoneyWithdrawalBundleType(1 << 0, nameof(Fifty));
        public static readonly MoneyWithdrawalBundleType OneHundred = new MoneyWithdrawalBundleType(1 << 1, nameof(OneHundred));
        public static readonly MoneyWithdrawalBundleType TwoHundred = new MoneyWithdrawalBundleType(1 << 2, nameof(TwoHundred));

        private MoneyWithdrawalBundleType(int value, string name) : base(value, name)
        {
        }
    }
}