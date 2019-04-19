// Filename: Buckets.cs
// Date Created: 2019-04-19
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

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Helpers
{
    public static class Buckets
    {
        public static List<int> BucketList(int payoutEntries, int bucketCount)
        {
            var buckets = new List<int>();

            // Must be at least 4 winners
            if (payoutEntries < 4)
            {
                return buckets;
            }

            // Must be more or an equal number of winners than the number of buckets.
            if (payoutEntries < bucketCount)
            {
                return buckets;
            }

            // The first four buckets have size 1.
            if (payoutEntries > 4 && bucketCount <= 4)
            {
                return buckets;
            }

            // First 4 buckets of size 1
            for (var index = 0; index < 4; index++)
            {
                buckets.Add(1);
            }

            if (payoutEntries - buckets.Sum() == 1)
            {
                buckets.Add(1); // Size of bucket 5 = 1

                return buckets;
            }

            if (payoutEntries == bucketCount)
            {
                for (var index = 0; index < payoutEntries - 4; index++)
                {
                    buckets.Add(1);
                }

                return buckets;
            }

            var beta = Beta(payoutEntries, bucketCount);

            var bucketSum = 5;

            var i = 1;

            while (bucketSum <= payoutEntries)
            {
                var thisBuckSize = Math.Ceiling(Math.Pow(beta, i));

                var buckSize = Convert.ToInt32(thisBuckSize);

                buckets.Add(buckSize);

                bucketSum += buckSize;

                i += 1;
            }

            var removeBucketSize = buckets.Sum() - payoutEntries;

            // We need to decrease some sizes
            if (removeBucketSize > 0)
            {
                buckets = RemoveRange(buckets, removeBucketSize);
            }

            if (buckets.Count < bucketCount)
            {
                buckets = AddRange(buckets, bucketCount);
            }

            removeBucketSize = buckets.Sum() - payoutEntries;

            // We need to decrease some sizes
            if (removeBucketSize > 0)
            {
                buckets = RemoveRange(buckets, removeBucketSize);
            }

            return buckets;
        }

        private static double Beta(int payoutEntries, int bucketCount)
        {
            return Optimization.Bisection(b =>
            {
                double count = 0;

                for (var index = 1; index < bucketCount - 3; index++)
                {
                    count += Math.Pow(b, index);
                }

                return count - payoutEntries + 4;
            }, -1, 2);
        }

        private static List<int> RemoveRange(List<int> bucketSizes, int toRemove)
        {
            if (bucketSizes.Count == 0)
            {
                return bucketSizes;
            }

            var index = bucketSizes.Count - 1;

            while (toRemove > 0)
            {
                var diff = bucketSizes[index] - bucketSizes[index - 1];

                if (diff >= toRemove)
                {
                    bucketSizes[index] -= toRemove;

                    toRemove = 0;
                }
                else
                {
                    bucketSizes[index] -= diff;

                    toRemove -= diff;

                    index -= 1;
                }
            }

            if (toRemove != 0)
            {
                RemoveRange(bucketSizes, toRemove);
            }

            return bucketSizes;
        }

        //Note: Some errors can occurs, like init_buck_size(15, 12)
        //Typically errors will occurs when num_wins is near num_bucks.
        private static List<int> AddRange(List<int> bucketSizes, int numBucks)
        {
            var numBuckToAdd = numBucks - bucketSizes.Count;

            // First try to extend with ones. 
            for (var index = 0; index < numBuckToAdd; index++)
            {
                bucketSizes.Add(1);
            }

            return bucketSizes;
        }
    }
}