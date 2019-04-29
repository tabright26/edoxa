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
        public static (IPrizes initialPrizes, PayoutLeftover leftover) InitPrizes(IPrizes unperfectPrizes, IBucketSizes bucketSizes)
        {
            // Need to check if sum(bucket_sizes) == len(unperfect_prize)
            if (bucketSizes.Sum(bucketSize => bucketSize) != unperfectPrizes.Count)
            {
                throw new ArgumentException("Bucket sizes is incompatible with the number of prizes");
            }

            // Take the first unperfect_prize and generate nice numbers list
            var nicePrize = PerfectPrizes(unperfectPrizes);

            var prizes = new Prizes(); // Will contains the first attempt of good prizes

            var position = 0;

            double leftover = 0;

            foreach (var bucketSize in bucketSizes)
            {
                var list = new Prizes();

                for (var index = position; index < position + bucketSize; index++)
                {
                    list.Add(unperfectPrizes[index]);
                }

                // rounding the first tentative prize to nearest nice number
                var currentUnperfectPrize = (list.Sum(x => x.ToDouble()) + leftover) / bucketSize;

                var currentPerfectPrize = RoundPerfectPrize(currentUnperfectPrize, nicePrize);

                prizes.Add(new Prize(currentPerfectPrize));

                // Then compute leftover
                leftover = (currentUnperfectPrize - currentPerfectPrize) * bucketSize;

                position += bucketSize;
            }

            return (prizes, new PayoutLeftover(leftover));
        }


        // TODO: This need to be optimized for large number like 5000000 (very slow).
        public static List<int> PerfectPrizes(IPrizes prizes)
        {
            var perfectPrizes = new List<int>();

            for (var perfectPrize = 1; perfectPrize < Convert.ToInt64(prizes.First()) + 1; perfectPrize++)
            {
                if (IsPerfectPrize(perfectPrize))
                {
                    perfectPrizes.Add(perfectPrize);
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

        public static int RoundPerfectPrize(double number, List<int> perfectPrizes)
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

            return currentPerfectPrize;
        }
    }
}