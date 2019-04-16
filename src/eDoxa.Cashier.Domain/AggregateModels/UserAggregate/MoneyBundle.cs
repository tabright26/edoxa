﻿// Filename: MoneyBundle.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public sealed class MoneyBundle : CurrencyBundle<Money>
    {
        public MoneyBundle(Money amount) : base(amount, amount)
        {
        }

        public override Transaction CreateTransaction(CustomerId customerId)
        {
            return new Transaction(customerId, $"eDoxa Funds ({Amount})", Price);
        }
    }
}