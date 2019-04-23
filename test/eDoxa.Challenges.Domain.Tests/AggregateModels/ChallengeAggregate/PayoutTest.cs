// Filename: PayoutTest.cs
// Date Created: 2019-04-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class PayoutTest
    {
        [TestMethod]
        public void M()
        {
            var entries = new Entries(2500);

            var entryFee = new EntryFee(25);

            var payoutRatio = new PayoutRatio(0.5F);

            var serviceChargeRatio = new ServiceChargeRatio(0.2F);

            var bucketCount = new BucketCount(10);

            var payoutEntries = new PayoutEntries(entries, payoutRatio);

            var prizePool = new PrizePool(entries, entryFee, serviceChargeRatio);

            var prizes = new Prizes(payoutEntries, prizePool, entryFee);

            var bucketSizes = new BucketSizes(payoutEntries, bucketCount);

            var (initialPrizes, leftover) = Prizes.InitPrizes(prizes, bucketSizes);

            var (finalPrizes, finalBucketSizes, finalLeftover) = Prizes.SpendLeftover(initialPrizes, bucketSizes, leftover);

            var buckets = new Buckets(finalBucketSizes, finalPrizes);

            var payout = new ChallengePayout(buckets, finalLeftover);
        }
    }
}