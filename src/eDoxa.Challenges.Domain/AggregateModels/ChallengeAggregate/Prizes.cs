// Filename: Prizes.cs
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

using eDoxa.Seedwork.Domain.Common;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class Prizes : List<Prize>, IPrizes
    {
        public Prizes(PayoutEntries payoutEntries, PrizePool prizePool, EntryFee entryFee)
        {
            var winnerPrize = new FirstPrize(prizePool);

            var alpha = Alpha(payoutEntries, prizePool, entryFee, winnerPrize);

            for (var index = 1; index < payoutEntries + 1; index++)
            {
                this.Add(new Prize(Convert.ToDecimal(entryFee.ToDouble() + (winnerPrize.ToDouble() - entryFee.ToDouble()) / Math.Pow(index, alpha))));
            }
        }

        public Prizes(IEnumerable<Prize> prizes) : base(prizes)
        {
        }

        public Prizes()
        {
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