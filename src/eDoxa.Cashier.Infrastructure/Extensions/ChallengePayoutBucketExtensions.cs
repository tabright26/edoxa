// Filename: ChallengePayoutBucketExtensions.cs
// Date Created: 2020-01-22
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Infrastructure.Models;

namespace eDoxa.Cashier.Infrastructure.Extensions
{
    public static class ChallengePayoutBucketExtensions
    {
        public static ChallengePayoutBucketModel ToModel(this ChallengePayoutBucket challengePayoutBucket)
        {
            return new ChallengePayoutBucketModel
            {
                Size = challengePayoutBucket.Size,
                PrizeCurrency = challengePayoutBucket.Prize.Currency.Value,
                PrizeAmount = challengePayoutBucket.Prize.Amount
            };
        }
    }
}
