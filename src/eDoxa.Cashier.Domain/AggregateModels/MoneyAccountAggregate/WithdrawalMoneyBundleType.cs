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
    public sealed class WithdrawalMoneyBundleType : Enumeration<WithdrawalMoneyBundleType>
    {
        public static readonly WithdrawalMoneyBundleType Fifty = new WithdrawalMoneyBundleType(1 << 0, nameof(Fifty));
        public static readonly WithdrawalMoneyBundleType OneHundred = new WithdrawalMoneyBundleType(1 << 1, nameof(OneHundred));
        public static readonly WithdrawalMoneyBundleType TwoHundred = new WithdrawalMoneyBundleType(1 << 2, nameof(TwoHundred));

        private WithdrawalMoneyBundleType(int value, string name) : base(value, name)
        {
        }
    }
}