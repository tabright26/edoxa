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
    public sealed class WithdrawalMoneyBundles : ReadOnlyDictionary<WithdrawalMoneyBundleType, MoneyBundle>
    {
        private static readonly Dictionary<WithdrawalMoneyBundleType, MoneyBundle> Bundles =
            new Dictionary<WithdrawalMoneyBundleType, MoneyBundle>
            {
                [WithdrawalMoneyBundleType.Fifty] = new MoneyBundle(Money.Fifty),
                [WithdrawalMoneyBundleType.OneHundred] = new MoneyBundle(Money.OneHundred),
                [WithdrawalMoneyBundleType.TwoHundred] = new MoneyBundle(Money.TwoHundred)
            };

        public WithdrawalMoneyBundles() : base(Bundles)
        {
        }
    }
}