// Filename: Buckets.cs
// Date Created: 2019-04-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.ObjectModel;
using System.Linq;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class Buckets : Collection<Bucket>, IBuckets
    {
        // TODO: To defend.
        public Buckets(IBucketSizes bucketSizes, IPrizes prizes)
        {
            for (var index = 0; index < bucketSizes.Count; index++)
            {
                this.Add(new Bucket(bucketSizes[index], prizes[index]));
            }
        }

        // TODO: To defend.
        public Buckets()
        {

        }

        public IPrizes Prizes => new Prizes(this.Select(bucket => bucket.Prize).OrderByDescending(prize => prize));

        public IBucketSizes BucketSizes => new BucketSizes(this.Select(bucket => bucket.Size).OrderBy(size => size));

        public PayoutLeftover Leftover(PrizePool prizePool)
        {
            return new PayoutLeftover(prizePool - this.Sum(bucket => bucket.Prize * bucket.Size));
        }
    }
}