// Filename: MoneyBundles.cs
// Date Created: 2019-05-06
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
    public sealed class MoneyDepositBundles : ReadOnlyDictionary<MoneyDepositBundleType, MoneyBundle>
    {
        private static readonly Dictionary<MoneyDepositBundleType, MoneyBundle> Bundles =
            new Dictionary<MoneyDepositBundleType, MoneyBundle>
            {
                [MoneyDepositBundleType.Ten] = new MoneyBundle(Money.Ten),
                [MoneyDepositBundleType.Twenty] = new MoneyBundle(Money.Twenty),
                [MoneyDepositBundleType.Fifty] = new MoneyBundle(Money.Fifty),
                [MoneyDepositBundleType.OneHundred] = new MoneyBundle(Money.OneHundred),
                [MoneyDepositBundleType.FiveHundred] = new MoneyBundle(Money.FiveHundred)
            };

        public MoneyDepositBundles() : base(Bundles)
        {
        }

        public bool IsValid(MoneyDepositBundleType bundleType)
        {
            return this.ContainsKey(bundleType);
        }
    }
}