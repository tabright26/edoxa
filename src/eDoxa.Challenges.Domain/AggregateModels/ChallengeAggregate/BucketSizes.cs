// Filename: BucketSizes.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Domain.Common;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class BucketSizes : List<BucketSize>, IBucketSizes
    {
        public BucketSizes(PayoutEntries payoutEntries, BucketCount bucketCount)
        {
            // Must be at least 4 winners
            if (payoutEntries < 4)
            {
                return;
            }

            // Must be more or an equal number of winners than the number of buckets.
            if (payoutEntries < bucketCount)
            {
                return;
            }

            // The first four buckets have size 1.
            if (payoutEntries > 4 && bucketCount <= 4)
            {
                return;
            }

            // First 4 buckets of size 1
            for (var index = 0; index < 4; index++)
            {
                this.Add(BucketSize.DefaultValue);
            }

            if (payoutEntries - this.Sum(x => x) == 1)
            {
                this.Add(BucketSize.DefaultValue); // Size of bucket 5 = 1

                return;
            }

            if (payoutEntries == bucketCount)
            {
                for (var index = 0; index < payoutEntries - 4; index++)
                {
                    this.Add(BucketSize.DefaultValue);
                }

                return;
            }

            var beta = Beta(payoutEntries, bucketCount);

            var bucketSum = 5;

            var i = 1;

            while (bucketSum <= payoutEntries)
            {
                var thisBuckSize = Math.Ceiling(Math.Pow(beta, i));

                var buckSize = Convert.ToInt32(thisBuckSize);

                this.Add(new BucketSize(buckSize));

                bucketSum += buckSize;

                i += 1;
            }

            var removeBucketCount = new BucketCount(this.Sum(x => x) - payoutEntries);

            // We need to decrease some sizes
            if (removeBucketCount > 0)
            {
                this.RemoveRange(this, removeBucketCount);
            }

            if (Count < bucketCount)
            {
                AddRange(this, bucketCount);
            }

            removeBucketCount = new BucketCount(this.Sum(x => x) - payoutEntries);

            // We need to decrease some sizes
            if (removeBucketCount > 0)
            {
                this.RemoveRange(this, removeBucketCount);
            }
        }

        public BucketSizes(IEnumerable<BucketSize> bucketSizes) : base(bucketSizes)
        {
        }

        private static double Beta(PayoutEntries payoutEntries, BucketCount bucketCount)
        {
            return MathUtils.Bisection(b =>
            {
                double count = 0;

                for (var index = 1; index < bucketCount - 3; index++)
                {
                    count += Math.Pow(b, index);
                }

                return count - payoutEntries + 4;
            }, -1, 2);
        }

        private void RemoveRange(BucketSizes bucketSizes, BucketCount bucketCount)
        {
            if (bucketSizes.Count == 0)
            {
                return;
            }

            var index = bucketSizes.Count - 1;

            while (bucketCount > 0)
            {
                var diff = bucketSizes[index] - bucketSizes[index - 1];

                if (diff >= bucketCount)
                {
                    bucketSizes[index] = new BucketSize(bucketSizes[index] - bucketCount);

                    bucketCount = BucketCount.EmptyValue;
                }
                else
                {
                    bucketSizes[index] = new BucketSize(bucketSizes[index] - diff);

                    bucketCount = new BucketCount(bucketCount - diff);

                    index -= 1;
                }
            }

            if (bucketCount != 0)
            {
                this.RemoveRange(bucketSizes, bucketCount);
            }
        }

        //Note: Some errors can occurs, like init_buck_size(15, 12)
        //Typically errors will occurs when num_wins is near num_bucks.
        private static void AddRange(BucketSizes bucketSizes, BucketCount bucketCount)
        {
            var numBuckToAdd = bucketCount - bucketSizes.Count;

            // First try to extend with ones. 
            for (var index = 0; index < numBuckToAdd; index++)
            {
                bucketSizes.Add(BucketSize.DefaultValue);
            }
        }
    }
}