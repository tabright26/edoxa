// Filename: Bundle.cs
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
    public abstract class Bundle<TCurrency>
    where TCurrency : ICurrency
    {
        protected Bundle(Money price, TCurrency amount)
        {
            Price = price;
            Amount = amount;
        }

        public TCurrency Amount { get; }

        protected Money Price { get; }

        public abstract Transaction CreateTransaction(User user);
    }
}