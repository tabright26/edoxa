﻿// Filename: BucketUtils.cs
// Date Created: 2019-04-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public static class BucketUtils
    {
        public static Buckets SpendLeftover(Buckets buckets, PrizePool prizePool)
        {
            var leftover = buckets.Leftover(prizePool);

            var prizes = new Prizes(buckets.Prizes);

            var bucketSizes = new BucketSizes(buckets.BucketSizes);

            var perfectPrizes = PrizeUtils.PerfectPrizes(prizes);

            // First : Spend as much of possible leftover on singleton bucket 2 through 3.
            for (var index = 1; index < 3; index++)
            {
                var unperfectAverage = Math.Min(prizes[index] + leftover, (prizes[index - 1] + prizes[index]) / 2);

                var perfectAverage = PrizeUtils.PerfectAverage(unperfectAverage, perfectPrizes);

                leftover = leftover + prizes[index] - perfectAverage;

                prizes[index] = new Prize(perfectAverage);
            }

            prizes[0] = new Prize(prizes[0] + leftover);

            return new Buckets(bucketSizes, prizes);
        }
    }
}