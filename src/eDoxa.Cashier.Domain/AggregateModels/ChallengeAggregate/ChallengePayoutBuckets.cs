// Filename: ChallengePayoutBuckets.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengePayoutBuckets : List<ChallengePayoutBucket>, IChallengePayoutBuckets
    {
        public ChallengePayoutBuckets(IEnumerable<ChallengePayoutBucket> buckets) : base(buckets)
        {
        }
    }
}
