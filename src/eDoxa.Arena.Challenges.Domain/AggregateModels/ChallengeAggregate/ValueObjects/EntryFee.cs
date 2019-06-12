// Filename: EntryFee.cs
// Date Created: 2019-06-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Globalization;

using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.ValueObjects
{
    public sealed class TokenEntryFee : EntryFee
    {
        public static readonly TokenEntryFee TwoAndHalf = new TokenEntryFee(2500M);
        public static readonly TokenEntryFee Five = new TokenEntryFee(5000M);
        public static readonly TokenEntryFee Ten = new TokenEntryFee(10000M);
        public static readonly TokenEntryFee Twenty = new TokenEntryFee(20000M);
        public static readonly TokenEntryFee TwentyFive = new TokenEntryFee(25000M);
        public static readonly TokenEntryFee Fifty = new TokenEntryFee(50000M);
        public static readonly TokenEntryFee SeventyFive = new TokenEntryFee(75000M);
        public static readonly TokenEntryFee OneHundred = new TokenEntryFee(100000M);

        private TokenEntryFee(decimal entryFee) : base(CurrencyType.Token, entryFee)
        {
        }
    }

    public sealed class MoneyEntryFee : EntryFee
    {
        public static readonly MoneyEntryFee TwoAndHalf = new MoneyEntryFee(2.5M);
        public static readonly MoneyEntryFee Five = new MoneyEntryFee(5M);
        public static readonly MoneyEntryFee Ten = new MoneyEntryFee(10M);
        public static readonly MoneyEntryFee Twenty = new MoneyEntryFee(20M);
        public static readonly MoneyEntryFee TwentyFive = new MoneyEntryFee(25M);
        public static readonly MoneyEntryFee Fifty = new MoneyEntryFee(50M);
        public static readonly MoneyEntryFee SeventyFive = new MoneyEntryFee(75M);
        public static readonly MoneyEntryFee OneHundred = new MoneyEntryFee(100M);

        private MoneyEntryFee(decimal entryFee) : base(CurrencyType.Money, entryFee)
        {
        }
    }

    public class EntryFee : ValueObject, ICurrency
    {
        public EntryFee(CurrencyType type, decimal amount)
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
