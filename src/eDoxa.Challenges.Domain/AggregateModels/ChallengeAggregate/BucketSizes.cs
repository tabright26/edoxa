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
        public BucketSizes(PayoutEntries payoutEntries)
        {
            var bucketCount = new BucketCount(payoutEntries);

            if (payoutEntries < 3)
            {
                for (var index = 0; index < payoutEntries; index++)
                {
                    this.Add(BucketSize.DefaultValue);
                }

                return;
            }

            // First 3 buckets of size 1
            for (var index = 0; index < 3; index++)
            {
                this.Add(BucketSize.DefaultValue);
            }

            if (payoutEntries - this.PayoutEntries() == 1)
            {
                this.Add(BucketSize.DefaultValue); // Size of bucket 4 = 1

                return;
            }

            if (payoutEntries == bucketCount)
            {
                for (var index = 0; index < payoutEntries - 3; index++)
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

            var removeBucketCount = this.PayoutEntries() - payoutEntries;

            // We need to decrease some sizes
            if (removeBucketCount > 0)
            {
                this.RemoveRange(this, removeBucketCount);
            }

            if (Count < bucketCount)
            {
                AddRange(this, bucketCount);
            }

            removeBucketCount = this.PayoutEntries() - payoutEntries;

            // We need to decrease some sizes
            if (removeBucketCount > 0)
            {
                this.RemoveRange(this, removeBucketCount);
            }
        }

        public BucketSizes(IEnumerable<BucketSize> bucketSizes) : base(bucketSizes)
        {
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

        private void RemoveRange(BucketSizes bucketSizes, int bucketCount)
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

                    bucketCount -= diff;

                    index -= 1;
                }
            }

            if (bucketCount != 0)
            {
                this.RemoveRange(bucketSizes, bucketCount);
            }
        }

        public int PayoutEntries()
        {
            return this.Sum(bucketSize => bucketSize);
        }

        private static double Beta(PayoutEntries payoutEntries, int bucketCount)
        {
            return MathUtils.Bisection(b =>
            {
                double count = 0;

                for (var index = 1; index < bucketCount - 2; index++)
                {
                    count += Math.Pow(b, index);
                }

                return count - payoutEntries + 3;
            }, -1, 2);
        }
    }
}