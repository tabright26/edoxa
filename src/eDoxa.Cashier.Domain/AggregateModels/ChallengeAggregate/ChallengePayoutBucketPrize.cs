// Filename: ChallengePayoutBucketPrize.cs
// Date Created: 2020-01-30
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengePayoutBucketPrize : Currency
    {
        public static readonly ChallengePayoutBucketPrize None = new ChallengePayoutBucketPrize();
        public static readonly ChallengePayoutBucketPrize Consolation = new ChallengePayoutBucketPrize(Token.MinValue);

        public ChallengePayoutBucketPrize(decimal amount, CurrencyType type) : base(amount, type)
        {
            if (amount < 0)
            {
                throw new ArgumentException(nameof(amount));
            }
        }

        private ChallengePayoutBucketPrize() : base(0, CurrencyType.Token)
        {
        }

        public ChallengePayoutBucketPrize(Currency currency) : this(currency.Amount, currency.Type)
        {
        }

        public ChallengePayoutBucketPrize Apply(ChallengePayoutChart chart)
        {
            return new ChallengePayoutBucketPrize(chart.Weigthing * Amount, Type);
        }
    }
}
