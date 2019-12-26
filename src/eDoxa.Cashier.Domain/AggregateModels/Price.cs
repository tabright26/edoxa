// Filename: Price.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels
{
    public sealed class Price : ValueObject
    {
        private const decimal TokenToMoneyFactor = 1000M;

        private readonly Money _money;

        public Price(ICurrency currency)
        {
            Money? money = null;

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

        public Money Money => _money;

        public long ToCents()
        {
            return _money.ToCents();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _money;
        }

        public override string ToString()
        {
            return _money.ToString();
        }
    }
}
