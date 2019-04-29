// Filename: Prizes.cs
// Date Created: 2019-04-20
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
    public class Prizes : List<Prize>, IPrizes
    {
        public Prizes(PayoutEntries payoutEntries, PrizePool prizePool, EntryFee entryFee)
        {
            if (prizePool <= payoutEntries * entryFee)
            {
                throw new ArgumentException("Wrong choice of parameters, prize_pool must be increase.");
            }
            
            var winnerPrize = new FirstPrize(prizePool);

            var alpha = Alpha(payoutEntries, prizePool, entryFee, winnerPrize);

            for (var index = 1; index < payoutEntries + 1; index++)
            {
                this.Add(new Prize(Convert.ToDecimal(entryFee.ToDouble() + (winnerPrize.ToDouble() - entryFee.ToDouble()) / Math.Pow(index, alpha))));
            }
        }
        
        private Prizes(IPrizes prizes) : base(prizes)
        {
        }

        public Prizes()
        {
        }

        public static (IPrizes finalPrizes, IBucketSizes finalBucketSizes, PayoutLeftover finalLeftover) SpendLeftover(
            IPrizes initialPrizes,
            IBucketSizes initialBucketSizes,
            PayoutLeftover leftover)
        {
            var prizes = new Prizes(initialPrizes);
            var bucketSizes = new BucketSizes(initialBucketSizes);

            var niceNumbers = PrizeUtils.PerfectPrizes(prizes);

            // First : Spend as much of possible leftover on singleton bucket 2 through 4.
            for (var index = 1; index < 4; index++)
            {
                var minVal = Math.Min(prizes[index].ToDouble() + leftover, (prizes[index - 1].ToDouble() + prizes[index].ToDouble()) / 2D);

                var niceVal = PrizeUtils.RoundPerfectPrize(minVal, niceNumbers);

                leftover = new PayoutLeftover(leftover + prizes[index].ToDouble() - niceVal);

                prizes[index] = new Prize(niceVal);

                // Could choose another value
                if (Math.Abs(leftover) < 0.01)
                {
                    return (prizes, bucketSizes, leftover);
                }
            }

            // Otherwise, we try to adjust starting form the final bucket.
            var bucketNum = bucketSizes.Count - 1;

            while (bucketNum > 0)
            {
                while (leftover >= bucketSizes[bucketNum])
                {
                    prizes[bucketNum] = new Prize(prizes[bucketNum] + 1); // Could lead to nice number violations

                    leftover = new PayoutLeftover(leftover - bucketSizes[bucketNum]);

                    // Could choose another value
                    if (Math.Abs(leftover) < 0.01)
                    {
                        return (prizes, bucketSizes, leftover);
                    }
                }

                bucketNum -= 1;

                if (Math.Abs(leftover % prizes[bucketNum].ToDouble()) < 0.01)
                {
                    bucketSizes[bucketNum] =
                        new BucketSize(bucketSizes[bucketNum] +
                                       MathUtils.FloorDiv(leftover, prizes[bucketNum].ToDouble())); // number of winners increase
                }
            }

            return (prizes, bucketSizes, leftover);
        }

        private static double Alpha(PayoutEntries payoutEntries, PrizePool prizePool, EntryFee entryFee, FirstPrize firstPrize)
        {
            var b = 1 - Math.Log((prizePool.ToDouble() - payoutEntries * entryFee.ToDouble()) / (firstPrize.ToDouble() - entryFee.ToDouble())) /
                    Math.Log(payoutEntries);

            return MathUtils.Bisection(a =>
            {
                double count = 0;

                for (var index = 1; index < payoutEntries + 1; index++)
                {
                    count += 1 / Math.Pow(index, a);
                }

                return prizePool.ToDouble() - payoutEntries * entryFee.ToDouble() - (firstPrize.ToDouble() - entryFee.ToDouble()) * count;
            }, 0, b);
        }
    }
}