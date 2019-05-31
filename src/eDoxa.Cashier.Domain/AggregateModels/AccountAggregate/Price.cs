// Filename: Price.cs
// Date Created: 2019-05-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public sealed class Price
    {
        private const decimal TokenToMoneyFactor = 1000M;

        private readonly Money _money;

        public Price(decimal amount, CurrencyType type)
        {
            Money money = null;

            if (type == CurrencyType.Money)
            {
                money = new Money(amount);
            }

            if (type == CurrencyType.Token)
            {
                money = new Money(amount / TokenToMoneyFactor);
            }

            _money = money ?? throw new ArgumentException(nameof(type));
        }

        public long ToCents()
        {
            return _money.ToCents();
        }
    }
}
