// Filename: EntryFee.cs
// Date Created: 2020-01-29
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Cashier.Domain.AggregateModels
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

        private TokenEntryFee(decimal entryFee) : base(entryFee, CurrencyType.Token)
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

        private MoneyEntryFee(decimal entryFee) : base(entryFee, CurrencyType.Money)
        {
        }
    }

    public class EntryFee : Currency
    {
        public EntryFee(decimal amount, CurrencyType type) : base(amount, type)
        {
        }

        public bool Free => Amount == 0;

        public ChallengePayoutBucketPrize GetPayoutBucketPrizeOrDefault()
        {
            if (Free)
            {
                if (Type == CurrencyType.Money)
                {
                    return new ChallengePayoutBucketPrize(Money.MinValue, Type);
                }

                if (Type == CurrencyType.Token)
                {
                    return new ChallengePayoutBucketPrize(Token.MinValue, Type);
                }
            }

            return new ChallengePayoutBucketPrize(Amount, Type);
        }
    }
}
