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

using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public sealed partial class Prize : ValueObject
    {
        public static readonly Prize None = new Prize(0, Currency.Token);

        public Prize(decimal amount, Currency currency)
        {
            if (amount < 0)
            {
                throw new ArgumentException(nameof(amount));
            }

            Amount = amount;
            Currency = currency;
        }

        public decimal Amount { get; }

        public Currency Currency { get; }

        public static implicit operator decimal(Prize currency)
        {
            return currency.Amount;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Amount;
            yield return Currency;
        }

        public override string ToString()
        {
            if (Currency == Currency.Money)
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

            return new Prize(Amount * factor, Currency);
        }
    }

    public sealed partial class Prize : IComparable
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return Amount.CompareTo(((Prize) obj)?.Amount);
        }
    }
}
