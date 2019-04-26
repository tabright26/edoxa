// Filename: MoneyBundles.cs
// Date Created: 2019-04-26
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
    public sealed class MoneyBundles : ReadOnlyDictionary<MoneyBundleType, MoneyBundle>
    {
        private static readonly Dictionary<MoneyBundleType, MoneyBundle> Bundles =
            new Dictionary<MoneyBundleType, MoneyBundle>
            {
                [MoneyBundleType.Ten] = new MoneyBundle(Money.Ten),
                [MoneyBundleType.Twenty] = new MoneyBundle(Money.Twenty),
                [MoneyBundleType.Fifty] = new MoneyBundle(Money.Fifty),
                [MoneyBundleType.OneHundred] = new MoneyBundle(Money.OneHundred),
                [MoneyBundleType.FiveHundred] = new MoneyBundle(Money.FiveHundred)
            };

        public MoneyBundles() : base(Bundles)
        {
        }
    }
}