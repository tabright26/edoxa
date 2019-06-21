// Filename: EntryFee.cs
// Date Created: 2019-06-20
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
using eDoxa.Seedwork.Domain.Attributes;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class TokenEntryFee : EntryFee
    {
        [AllowValue(true)] public static readonly TokenEntryFee TwoThousandFiveHundred = new TokenEntryFee(2500M);
        [AllowValue(true)] public static readonly TokenEntryFee FiveThousand = new TokenEntryFee(5000M);
        [AllowValue(true)] public static readonly TokenEntryFee TenThousand = new TokenEntryFee(10000M);
        [AllowValue(true)] public static readonly TokenEntryFee TwentyThousand = new TokenEntryFee(20000M);
        [AllowValue(true)] public static readonly TokenEntryFee TwentyFiveThousand = new TokenEntryFee(25000M);
        [AllowValue(true)] public static readonly TokenEntryFee FiftyThousand = new TokenEntryFee(50000M);
        [AllowValue(false)] public static readonly TokenEntryFee SeventyFiveThousand = new TokenEntryFee(75000M);
        [AllowValue(false)] public static readonly TokenEntryFee OneHundredThousand = new TokenEntryFee(100000M);

        private TokenEntryFee(decimal entryFee) : base(CurrencyType.Token, entryFee)
        {
        }
    }

    public sealed class MoneyEntryFee : EntryFee
    {
        [AllowValue(true)] public static readonly MoneyEntryFee TwoAndHalf = new MoneyEntryFee(2.5M);
        [AllowValue(true)] public static readonly MoneyEntryFee Five = new MoneyEntryFee(5M);
        [AllowValue(true)] public static readonly MoneyEntryFee Ten = new MoneyEntryFee(10M);
        [AllowValue(true)] public static readonly MoneyEntryFee Twenty = new MoneyEntryFee(20M);
        [AllowValue(true)] public static readonly MoneyEntryFee TwentyFive = new MoneyEntryFee(25M);
        [AllowValue(true)] public static readonly MoneyEntryFee Fifty = new MoneyEntryFee(50M);
        [AllowValue(false)] public static readonly MoneyEntryFee SeventyFive = new MoneyEntryFee(75M);
        [AllowValue(false)] public static readonly MoneyEntryFee OneHundred = new MoneyEntryFee(100M);

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
