// Filename: MoneyWithdrawBundles.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate
{
    public sealed class MoneyWithdrawBundles : ReadOnlyDictionary<MoneyWithdrawBundleType, MoneyBundle>
    {
        private static readonly Dictionary<MoneyWithdrawBundleType, MoneyBundle> Bundles = new Dictionary<MoneyWithdrawBundleType, MoneyBundle>
        {
            [MoneyWithdrawBundleType.Fifty] = new MoneyBundle(Money.Fifty),
            [MoneyWithdrawBundleType.OneHundred] = new MoneyBundle(Money.OneHundred),
            [MoneyWithdrawBundleType.TwoHundred] = new MoneyBundle(Money.TwoHundred)
        };

        public MoneyWithdrawBundles() : base(Bundles)
        {
        }

        public bool IsValid(MoneyWithdrawBundleType bundleType)
        {
            return this.ContainsKey(bundleType);
        }
    }
}
