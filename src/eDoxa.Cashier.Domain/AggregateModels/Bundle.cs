// Filename: Bundle.cs
// Date Created: 2019-04-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Cashier.Domain.AggregateModels
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

        public Money Price { get; }
    }
}