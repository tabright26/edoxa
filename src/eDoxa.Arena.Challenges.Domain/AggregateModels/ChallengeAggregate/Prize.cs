// Filename: Prize.cs
// Date Created: 2019-06-20
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
using eDoxa.Seedwork.Domain.Attributes;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class Prize : ValueObject, ICurrency, IComparable
    {
        private const decimal Factor = 1000M;

        [AllowValue(false)] public static readonly Prize None = new Prize(0, CurrencyType.Token);

        public Prize(decimal amount, CurrencyType type)
        {
            if (amount < 0)
            {
                throw new ArgumentException(nameof(amount));
            }

            Amount = amount;
            Type = type;
        }

        public int CompareTo([CanBeNull] object obj)
        {
            return Amount.CompareTo(((Prize) obj)?.Amount);
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

        public override string ToString()
        {
            if (Type == CurrencyType.Money)
            {
                return $"${Amount}";
            }

            return Amount.ToString(CultureInfo.InvariantCulture);
        }

        public Prize ApplyFactor(decimal factor)
        {
            if (factor < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(factor));
            }

            return new Prize(Amount * factor, Type);
        }

        public Prize ConvertTo(CurrencyType type)
        {
            return Type != type ? Type == CurrencyType.Money ? new Prize(Amount * Factor, type) : new Prize(Amount / Factor, type) : this;
        }
    }
}
