// Filename: Prizes.cs
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
    public static class Prizes
    {
        public static (List<int> initialPrizes, double leftover) InitPrizes(List<double> unperfectPrizes, List<int> buckets)
        {
            // Need to check if sum(bucket_sizes) == len(unperfect_prize)
            if (buckets.Sum() != unperfectPrizes.Count)
            {
                throw new ArgumentException("Bucket sizes is incompatible with the number of prizes");
            }

            // Take the first unperfect_prize and generate nice numbers list
            var nicePrize = NiceNumber.PossibleNiceNumbers(Convert.ToInt32(unperfectPrizes[0]));

            var prizes = new List<int>(); // Will contains the first attempt of good prizes

            var position = 0;

            double leftover = 0;

            foreach (var bucket in buckets)
            {
                var list = new List<double>();

                for (var i = position; i < position + bucket; i++)
                {
                    list.Add(unperfectPrizes[i]);
                }

                // rounding the first tentative prize to nearest nice number
                var currentPrize = (list.Sum() + leftover) / bucket;

                var currentNicePrize = NiceNumber.Round(currentPrize, nicePrize);

                prizes.Add(currentNicePrize);

                // Then compute leftover
                leftover = (currentPrize - currentNicePrize) * bucket;

                position += bucket;
            }

            return (prizes, leftover);
        }

        public static List<double> GetUnperfectPrize(int payoutEntries, int prizePool, double firstPrize, int entryFee)
        {
            if (prizePool <= payoutEntries * entryFee)
            {
                throw new ArgumentException("Wrong choice of parameters, prize_pool must be increase.");
            }

            var alpha = Alpha(payoutEntries, prizePool, firstPrize, entryFee);

            var prizes = new List<double>();

            for (var index = 1; index < payoutEntries + 1; index++)
            {
                prizes.Add(entryFee + (firstPrize - entryFee) / Math.Pow(index, alpha));
            }

            return prizes;
        }

        private static double Alpha(int payoutEntries, int prizePool, double firstPrize, int entryFee)
        {
            var b = 1 - Math.Log((prizePool - payoutEntries * entryFee) / (firstPrize - entryFee)) / Math.Log(payoutEntries);

            return Optimization.Bisection(a =>
            {
                double count = 0;

                for (var index = 1; index < payoutEntries + 1; index++)
                {
                    count += 1 / Math.Pow(index, a);
                }

                return prizePool - payoutEntries * entryFee - (firstPrize - entryFee) * count;
            }, 0, b);
        }

        public static (List<int> finalPrizes, List<int> finalBucketSizes, double finalLeftover) SpendLeftover(
            List<int> initialPrizes,
            List<int> bucketSizes,
            double leftover)
        {
            var niceNumbers = NiceNumber.PossibleNiceNumbers(initialPrizes[0]);

            // First : Spend as much of possible leftover on singleton bucket 2 through 4.
            for (var index = 1; index < 4; index++)
            {
                var minVal = Math.Min(initialPrizes[index] + leftover, (initialPrizes[index - 1] + initialPrizes[index]) / 2D);

                var niceVal = NiceNumber.Round(minVal, niceNumbers);

                leftover += initialPrizes[index] - niceVal;

                initialPrizes[index] = niceVal;

                // Could choose another value
                if (Math.Abs(leftover) < 0.01)
                {
                    return (initialPrizes, bucketSizes, leftover);
                }
            }

            // Otherwise, we try to adjust starting form the final bucket.
            var bucketNum = bucketSizes.Count - 1;

            while (bucketNum > 0)
            {
                while (leftover >= bucketSizes[bucketNum])
                {
                    initialPrizes[bucketNum] += 1; // Could lead to nice number violations

                    leftover -= bucketSizes[bucketNum];

                    // Could choose another value
                    if (Math.Abs(leftover) < 0.01)
                    {
                        return (initialPrizes, bucketSizes, leftover);
                    }
                }

                bucketNum -= 1;

                if (Math.Abs(leftover % initialPrizes[bucketNum]) < 0.01)
                {
                    bucketSizes[bucketNum] += Optimization.FloorDiv(leftover, initialPrizes[bucketNum]); // number of winners increase
                }
            }

            return (initialPrizes, bucketSizes, leftover);
        }
    }
}