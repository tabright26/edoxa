// Filename: WithdrawalMoneyBundles.cs
// Date Created: 2019-05-10
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
    public sealed class MoneyWithdrawalBundles : ReadOnlyDictionary<MoneyWithdrawalBundleType, MoneyBundle>
    {
        private static readonly Dictionary<MoneyWithdrawalBundleType, MoneyBundle> Bundles =
            new Dictionary<MoneyWithdrawalBundleType, MoneyBundle>
            {
                [MoneyWithdrawalBundleType.Fifty] = new MoneyBundle(Money.Fifty),
                [MoneyWithdrawalBundleType.OneHundred] = new MoneyBundle(Money.OneHundred),
                [MoneyWithdrawalBundleType.TwoHundred] = new MoneyBundle(Money.TwoHundred)
            };

        public MoneyWithdrawalBundles() : base(Bundles)
        {
        }

        public bool IsValid(MoneyWithdrawalBundleType bundleType)
        {
            return this.ContainsKey(bundleType);
        }
    }
}