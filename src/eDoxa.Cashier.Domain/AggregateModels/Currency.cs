// Filename: Currency.cs
// Date Created: 2020-01-30
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Globalization;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels
{
    public abstract partial class Currency : ValueObject
    {
        protected const int ConvertionRatio = 100;

        protected Currency(decimal amount, CurrencyType type)
        {
            Amount = amount;
            Type = type;
        }

        public decimal Amount { get; private set; }

        public CurrencyType Type { get; private set; }

        public static implicit operator decimal(Currency currency)
        {
            return currency.Amount;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return Type;
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

    public abstract partial class Currency : IComparable, IComparable<Currency?>
    {
        public int CompareTo(object? obj)
        {
            return this.CompareTo(obj as Currency);
        }

        public int CompareTo(Currency? other)
        {
            return Amount.CompareTo(other?.Amount);
        }
    }
}
