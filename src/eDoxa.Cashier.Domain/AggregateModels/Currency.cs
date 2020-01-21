// Filename: Currency.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.ComponentModel;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels
{
    [TypeConverter(typeof(EnumerationTypeConverter))]
    public sealed class Currency : Enumeration<Currency>
    {
        public static readonly Currency Money = new Currency(1, nameof(Money));
        public static readonly Currency Token = new Currency(1 << 1, nameof(Token));

        public Currency()
        {
        }

        private Currency(int value, string name) : base(value, name)
        {
        }

        public ICurrency From(decimal amount)
        {
            if (this == Money)
            {
                return new Money(amount);
            }

            if (this == Token)
            {
                return new Token(amount);
            }

            throw new FormatException();
        }
    }
}
