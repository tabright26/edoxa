// Filename: ChallengePayoutBucket.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public class ChallengePayoutBucket : ValueObject
    {
        public ChallengePayoutBucket(ChallengePayoutBucketPrize prize, ChallengePayoutBucketSize size)
        {
            Size = size;
            Prize = prize;
        }

        public ChallengePayoutBucketPrize Prize { get; }

        public ChallengePayoutBucketSize Size { get; }

        public override string ToString()
        {
            return $"{Prize}:{Size}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Prize;
            yield return Size;
        }

        public IChallengePayoutBuckets AsIndividualBuckets()
        {
            var buckets = new List<ChallengePayoutBucket>();

            for (var index = 0; index < Size; index++)
            {
                buckets.Add(new ChallengePayoutBucket(Prize, ChallengePayoutBucketSize.Individual));
            }

            return new ChallengePayoutBuckets(buckets);
        }
    }
}
