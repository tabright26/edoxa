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
using System.Collections.Generic;
using System.Globalization;

using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Arena.Domain.Abstractions
{
    public abstract class Prize : ValueObject
    {
        private readonly decimal _amount;
        private readonly CurrencyType _currencyType;

        protected Prize(decimal amount, CurrencyType currencyType)
        {
            if (amount < 0)
            {
                throw new ArgumentException(nameof(amount));
            }

            _amount = amount;
            _currencyType = currencyType;
        }

        public decimal Amount => _amount;

        public CurrencyType CurrencyType => _currencyType;

        public static implicit operator decimal(Prize prize)
        {
            return prize.Amount;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return CurrencyType;
        }

        public override string ToString()
        {
            return Amount.ToString(CultureInfo.InvariantCulture);
        }

        public Prize ApplyFactor(PrizeFactor factor)
        {
            if (CurrencyType == CurrencyType.Money)
            {
                return new MoneyPrize(Amount * factor);
            }

            if (CurrencyType == CurrencyType.Token)
            {
                return new TokenPrize(Amount * factor);
            }

            throw new ArgumentException(nameof(factor));
        }
    }
}
