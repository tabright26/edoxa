// Filename: EntryFee.cs
// Date Created: 2019-05-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Globalization;

using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Common.Abstactions;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Arena.Domain.ValueObjects
{
    public class EntryFee : ValueObject, ICurrency
    {
        protected EntryFee(CurrencyType type, decimal amount)
        {
            Type = type;
            Amount = amount;
        }

        public CurrencyType Type { get; private set; }

        public decimal Amount { get; private set; }

        public static implicit operator decimal(EntryFee currency)
        {
            return currency.Amount;
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

        public Prize GetLowestPrize()
        {
            return new Prize(Amount, Type);
        }
    }
}
