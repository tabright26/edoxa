// Filename: Prize.cs
// Date Created: 2019-05-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Common.Abstactions;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Arena.Domain.ValueObjects
{
    public class Prize : ValueObject, ICurrency
    {
        private const decimal Factor = 1000M;

        public static readonly Prize None = new Prize(0, CurrencyType.Token);

        public Prize(decimal amount, CurrencyType type)
        {
            if (amount < 0)
            {
                throw new ArgumentException(nameof(amount));
            }

            Amount = amount;
            Type = type;
        }

        public CurrencyType Type { get; private set; }

        public decimal Amount { get; private set; }

        public static implicit operator decimal(Prize currency)
        {
            return currency.Amount;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Type;
            yield return Amount;
        }

        public Prize ApplyFactor(PrizeFactor factor)
        {
            return new Prize(Amount * factor, Type);
        }

        public Prize ConvertTo(CurrencyType type)
        {
            return Type != type ? Type == CurrencyType.Money ? new Prize(Amount * Factor, type) : new Prize(Amount / Factor, type) : this;
        }
    }
}
