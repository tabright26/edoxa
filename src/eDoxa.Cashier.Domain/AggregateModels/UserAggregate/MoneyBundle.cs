﻿// Filename: MoneyBundle.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public sealed class MoneyBundle : Bundle<Money>
    {
        public MoneyBundle(Money amount) : base(amount, amount)
        {
        }
    }
}