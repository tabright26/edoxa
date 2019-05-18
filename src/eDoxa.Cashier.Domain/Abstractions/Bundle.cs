// Filename: Bundle.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;

namespace eDoxa.Cashier.Domain.Abstractions
{
    public abstract class Bundle<TCurrency> : IBundle
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