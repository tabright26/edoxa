// Filename: EntryFee.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Globalization;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class TokenEntryFee : EntryFee
    {
        public static readonly TokenEntryFee TwoThousandFiveHundred = new TokenEntryFee(2500M);
        public static readonly TokenEntryFee FiveThousand = new TokenEntryFee(5000M);
        public static readonly TokenEntryFee TenThousand = new TokenEntryFee(10000M);
        public static readonly TokenEntryFee TwentyThousand = new TokenEntryFee(20000M);
        public static readonly TokenEntryFee TwentyFiveThousand = new TokenEntryFee(25000M);
        public static readonly TokenEntryFee FiftyThousand = new TokenEntryFee(50000M);
        public static readonly TokenEntryFee SeventyFiveThousand = new TokenEntryFee(75000M);
        public static readonly TokenEntryFee OneHundredThousand = new TokenEntryFee(100000M);

        private TokenEntryFee(decimal entryFee) : base(entryFee, Currency.Token)
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

        private MoneyEntryFee(decimal entryFee) : base(entryFee, Currency.Money)
        {
        }
    }

    public class EntryFee : ValueObject
    {
        public EntryFee(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public decimal Amount { get; private set; }

        public Currency Currency { get; private set; }

        public static implicit operator decimal(EntryFee entryFee)
        {
            return entryFee.Amount;
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

        public Prize GetLowestPrize()
        {
            if (Amount == 0)
            {
                if (Currency == Currency.Money)
                {
                    return new Prize(Money.MinValue, Currency); 
                }

                if (Currency == Currency.Token)
                {
                    return new Prize(Token.MinValue, Currency); 
                }
            }

            return new Prize(Amount, Currency);
        }
    }
}
