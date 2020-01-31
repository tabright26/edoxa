// Filename: ChallengePayoutPrizePool.cs
// Date Created: 2020-01-31
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengePayoutPrizePool : Currency
    {
        public ChallengePayoutPrizePool(IChallengePayoutBuckets buckets) : base(
            buckets.SelectMany(bucket => bucket.AsIndividualBuckets()).Sum(bucket => bucket.Prize.Amount),
            buckets.First().Prize.Type)
        {
        }
    }
}
