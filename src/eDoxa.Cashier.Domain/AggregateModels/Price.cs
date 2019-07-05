// Filename: Price.cs
// Date Created: 2019-07-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;

namespace eDoxa.Cashier.Domain.AggregateModels
{
    public sealed class Price
    {
        private const decimal TokenToMoneyFactor = 1000M;

        private readonly Money _money;

        public Price(ICurrency currency)
        {
            Money money = null;

            if (currency.Type == CurrencyType.Money)
            {
                money = new Money(currency.Amount);
            }

            if (currency.Type == CurrencyType.Token)
            {
                money = new Money(currency.Amount / TokenToMoneyFactor);
            }

            _money = money ?? throw new ArgumentException(nameof(currency));
        }

        public long ToCents()
        {
            return _money.ToCents();
        }
    }
}
