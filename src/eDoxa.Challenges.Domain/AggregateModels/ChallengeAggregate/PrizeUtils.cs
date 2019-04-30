// Filename: PrizeBuilder.cs
// Date Created: 2019-04-29
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
    // TODO: To defend.
    public static class PrizeUtils
    {
        public static Buckets PerfectAverages(Prizes unperfectPrizes, PayoutEntries payoutEntries)
        {
            var bucketSizes = new BucketSizes(payoutEntries);

            // Need to check if sum(bucket_sizes) == len(unperfect_prize)
            if (bucketSizes.Sum(bucketSize => bucketSize) != unperfectPrizes.Count)
            {
                throw new ArgumentException("Bucket sizes is incompatible with the number of prizes");
            }

            var perfectPrizeAverages = new Prizes(); // Will contains the first attempt of good prizes

            // Take the first unperfect_prize and generate nice numbers list
            var perfectPrizes = PerfectPrizes(unperfectPrizes);

            var index = 0;

            var leftoverAverage = PayoutLeftover.DefaultValue;

            bucketSizes.ForEach(bucketSize =>
            {
                // rounding the first tentative prize to nearest nice number
                var unperfectPrizeAverage = UnperfectAverage(unperfectPrizes, index, leftoverAverage, bucketSize);

                var perfectPrizeAverage = PerfectAverage(unperfectPrizeAverage, perfectPrizes);

                perfectPrizeAverages.Add(perfectPrizeAverage);

                leftoverAverage = LeftoverAverage(unperfectPrizeAverage, perfectPrizeAverage, bucketSize);

                index += bucketSize;
            });

            return new Buckets(bucketSizes, perfectPrizeAverages);
        }

        private static decimal UnperfectAverage(Prizes unperfectPrizes, int index, PayoutLeftover leftover, BucketSize bucketSize)
        {
            var prizes = unperfectPrizes.GetRange(index, bucketSize);

            return (prizes.Sum(prize => prize) + leftover) / bucketSize;
        }

        public static Prize PerfectAverage(decimal number, List<Prize> perfectPrizes)
        {
            // TODO: Refactor to be defensive by design (replace exception with a non-null default).
            if (perfectPrizes.Count <= 0)
            {
                throw new ArgumentException("The nice number list should be at least 1.");
            }

            // TODO: Refactor to be defensive by design (replace exception with a non-null default).
            if (number < perfectPrizes.First())
            {
                throw new ArgumentException("The number should be greater than first nice number.");
            }

            if (number >= perfectPrizes.Last())
            {
                return perfectPrizes.Last();
            }

            var minIndex = 0;

            var maxIndex = perfectPrizes.Count - 1;

            var index = MathUtils.FloorDiv(maxIndex + minIndex, 2);

            var currentPerfectPrize = perfectPrizes[index];

            var nextPerfectPrize = perfectPrizes[index + 1];

            while (currentPerfectPrize > number || number >= nextPerfectPrize)
            {
                if (currentPerfectPrize < number)
                {
                    minIndex = index;
                }
                else if (currentPerfectPrize > number)
                {
                    maxIndex = index;
                }

                index = MathUtils.FloorDiv(maxIndex + minIndex, 2);

                currentPerfectPrize = perfectPrizes[index];

                nextPerfectPrize = perfectPrizes[index + 1];
            }

            return new Prize(currentPerfectPrize);
        }

        private static PayoutLeftover LeftoverAverage(decimal unperfectPrizeAverage, Prize perfectPrizeAverage, BucketSize bucketSize)
        {
            return new PayoutLeftover((unperfectPrizeAverage - perfectPrizeAverage) * bucketSize);
        }

        // TODO: This need to be optimized for large number like 5000000 (very slow).
        public static List<Prize> PerfectPrizes(IPrizes prizes)
        {
            var perfectPrizes = new List<Prize>();

            for (var perfectPrize = 1; perfectPrize < Convert.ToInt64(prizes.First()) + 1; perfectPrize++)
            {
                if (IsPerfectPrize(perfectPrize))
                {
                    perfectPrizes.Add(new Prize(perfectPrize));
                }
            }

            return perfectPrizes;
        }

        public static bool IsPerfectPrize(int prize)
        {
            var perfectPrize = (decimal) prize;

            while (perfectPrize > 1000)
            {
                perfectPrize /= 10;
            }

            if (perfectPrize >= 250)
            {
                return perfectPrize % 50 == 0;
            }

            if (perfectPrize >= 100)
            {
                return perfectPrize % 25 == 0;
            }

            if (perfectPrize >= 10)
            {
                return perfectPrize % 5 == 0;
            }

            if (perfectPrize > 0)
            {
                return perfectPrize % 1 == 0;
            }

            return false;
        }
    }
}