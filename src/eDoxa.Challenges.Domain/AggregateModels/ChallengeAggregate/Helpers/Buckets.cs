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
        public static List<int> InitBuckSize(int winners, int numBuck)
        {
            var bucketSizes = new List<int>();

            // Must be at least 4 winners
            if (winners < 4)
            {
                return bucketSizes;
            }

            // Must be more or an equal number of winners than the number of buckets.
            if (winners < numBuck)
            {
                return bucketSizes;
            }

            // The first four buckets have size 1.
            if (winners > 4 && numBuck <= 4)
            {
                return bucketSizes;
            }

            // First 4 buckets of size 1
            for (var i = 0; i < 4; i++)
            {
                bucketSizes.Add(1);
            }

            if (winners - bucketSizes.Sum() == 1)
            {
                bucketSizes.Add(1); // Size of bucket 5 = 1

                return bucketSizes;
            }

            if (winners == numBuck)
            {
                for (var i = 0; i < winners - 4; i++)
                {
                    bucketSizes.Add(1);
                }

                return bucketSizes;
            }

            var beta = Optimization.Bisection(b =>
            {
                double count = 0;

                for (var index = 1; index < numBuck - 3; index++)
                {
                    count += Math.Pow(b, index);
                }

                return count - winners + 4;
            }, -1, 2);

            var sumBuck = 5;

            var idx = 1;

            while (sumBuck <= winners)
            {
                var thisBuckSize = Math.Ceiling(Math.Pow(beta, idx));

                var buckSize = Convert.ToInt32(thisBuckSize);

                bucketSizes.Add(buckSize);

                sumBuck += buckSize;

                idx += 1;
            }

            var toRemove = bucketSizes.Sum() - winners;

            // We need to decrease some sizes
            if (toRemove > 0)
            {
                bucketSizes = RemoveSize(bucketSizes, toRemove);
            }

            if (bucketSizes.Count < numBuck)
            {
                bucketSizes = ExtendBuckets(bucketSizes, numBuck);
            }

            toRemove = bucketSizes.Sum() - winners;

            // We need to decrease some sizes
            if (toRemove > 0)
            {
                bucketSizes = RemoveSize(bucketSizes, toRemove);
            }

            return bucketSizes;
        }

        private static List<int> RemoveSize(List<int> bucketSizes, int toRemove)
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
                RemoveSize(bucketSizes, toRemove);
            }

            return bucketSizes;
        }

        //Note: Some errors can occurs, like init_buck_size(15, 12)
        //Typically errors will occurs when num_wins is near num_bucks.

        private static List<int> ExtendBuckets(List<int> bucketSizes, int numBucks)
        {
            var numBuckToAdd = numBucks - bucketSizes.Count;

            // First try to extend with ones. 
            for (var i = 0; i < numBuckToAdd; i++)
            {
                bucketSizes.Add(1);
            }

            return bucketSizes;
        }
    }
}