// Filename: CurrencyType.cs
// Date Created: 2019-11-25
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.ComponentModel;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels
{
    // Francis: c'est quoi le nom de ce patron la ??? sa a tu rapport avec value et name ?
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class CurrencyType : Enumeration<CurrencyType>
    {
        public static readonly CurrencyType Money = new CurrencyType(1, nameof(Money));
        public static readonly CurrencyType Token = new CurrencyType(1 << 1, nameof(Token));

        public CurrencyType()
        {
        }

        private CurrencyType(int value, string name) : base(value, name)
        {
        }

        public Currency ToCurrency(decimal amount)
        {
            if (this == Money)
            {
                return new Money(amount);
            }

            if (this == Token)
            {
                return new Token(amount);
            }

            throw new InvalidOperationException("Invalid currency type.");
        }
    }
}
