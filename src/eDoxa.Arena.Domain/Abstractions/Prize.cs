// Filename: Prize.cs
// Date Created: 2019-05-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Globalization;

using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Enumerations;

namespace eDoxa.Arena.Domain.Abstractions
{
    public abstract class Prize : ValueObject
    {
        public static readonly Prize None = new UndefinedPrize();

        private readonly decimal _amount;
        private readonly Currency _currency;

        protected Prize(decimal amount, Currency currency)
        {
            if (amount < 0)
            {
                throw new ArgumentException(nameof(amount));
            }

            _amount = amount;
            _currency = currency;
        }

        public decimal Amount => _amount;

        public Currency Currency => _currency;

        public static implicit operator decimal(Prize prize)
        {
            return prize.Amount;
        }

        public override string ToString()
        {
            return Amount.ToString(CultureInfo.InvariantCulture);
        }

        public Prize ApplyFactor(PrizeFactor factor)
        {
            if (Currency == Currency.Money)
            {
                return new MoneyPrize(Amount * factor);
            }

            if (Currency == Currency.Token)
            {
                return new TokenPrize(Amount * factor);
            }

            throw new ArgumentException(nameof(factor));
        }

        private sealed class UndefinedPrize : Prize
        {
            internal UndefinedPrize() : base(0, Currency.Undefined)
            {
            }
        }
    }
}
