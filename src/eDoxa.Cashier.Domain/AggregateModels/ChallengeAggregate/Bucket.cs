// Filename: Bucket.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate
{
    public class Bucket : ValueObject
    {
        public Bucket(Prize prize, BucketSize size)
        {
            Size = size;
            Prize = prize;
        }

        public Prize Prize { get; }

        public BucketSize Size { get; }

        public override string ToString()
        {
            return $"{Prize}:{Size}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Prize;
            yield return Size;
        }

        public IBuckets AsIndividualBuckets()
        {
            var buckets = new List<IndividualBucket>();

            for (var index = 0; index < Size; index++)
            {
                buckets.Add(new IndividualBucket(Prize));
            }

            return new Buckets(buckets);
        }
    }
}
