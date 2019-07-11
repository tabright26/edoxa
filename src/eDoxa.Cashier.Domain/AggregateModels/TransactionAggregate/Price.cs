// Filename: Price.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

namespace eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate
{
    public sealed class Price
    {
        private const decimal TokenToMoneyFactor = 1000M;

        private readonly Money _money;

        public Price(ICurrency currency)
        {
            Money money = null;

            if (currency.Type == Currency.Money)
            {
                money = new Money(Math.Abs(currency.Amount));
            }

            if (currency.Type == Currency.Token)
            {
                money = new Money(Math.Abs(currency.Amount) / TokenToMoneyFactor);
            }

            _money = money ?? throw new ArgumentException(nameof(currency));
        }

        public long ToCents()
        {
            return _money.ToCents();
        }
    }
}
