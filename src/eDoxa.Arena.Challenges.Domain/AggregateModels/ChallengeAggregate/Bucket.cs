// Filename: Bucket.cs
// Date Created: 2019-06-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class Bucket : ValueObject
    {
        public Bucket(Prize prize, BucketSize size) : this()
        {
            Size = size;
            Prize = prize;
        }

        private Bucket()
        {
            // Required by EF Core.
        }

        public Prize Prize { get; private set; }

        public BucketSize Size { get; private set; }

        public override string ToString()
        {
            return $"{Prize}:{Size}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Prize;
            yield return Size;
        }

        public IBuckets ToIndividualBuckets()
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
