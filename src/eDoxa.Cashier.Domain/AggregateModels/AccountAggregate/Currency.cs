// Filename: Currency.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Globalization;

using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public class Currency : ValueObject, ICurrency
    {
        protected Currency(decimal amount, CurrencyType type) : this()
        {
            Type = type;
            Amount = amount;
        }

        private Currency()
        {
        }

        public CurrencyType Type { get; private set; }

        public decimal Amount { get; private set; }

        public static Currency operator -(Currency currency)
        {
            return new Currency
            {
                Amount = currency.Amount,
                Type = currency.Type
            };
        }

        public static implicit operator Price(Currency currency)
        {
            return new Price(currency);
        }

        public static implicit operator decimal(Currency currency)
        {
            return currency.Amount;
        }

        [CanBeNull]
        public static Currency Convert(decimal amount, CurrencyType type)
        {
            if (type == CurrencyType.Money)
            {
                return new Money(amount);
            }

            if (type == CurrencyType.Token)
            {
                return new Token(amount);
            }

            throw new ArgumentException(nameof(type));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Type;
            yield return Amount;
        }

        public override string ToString()
        {
            if (Type == CurrencyType.Money)
            {
                return $"${Amount}";
            }

            return Amount.ToString(CultureInfo.InvariantCulture);
        }
    }
}
