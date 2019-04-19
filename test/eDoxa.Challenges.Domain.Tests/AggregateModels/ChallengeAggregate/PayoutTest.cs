// Filename: PayoutTest.cs
// Date Created: 2019-04-18
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

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class PayoutTest
    {
        [TestMethod]
        public void M()
        {
            var winners = 1250;
            var prizePool = 22500;
            var firstPrize = 4500;
            var entryFee = 5;
            var numBuck = 10;

            var unperfectPrizes = Prizes.GetUnperfectPrize(winners, prizePool, firstPrize, entryFee);
            var bucketSizes = Buckets.InitBuckSize(winners, numBuck);
            var (initialPrizes, leftover) = Prizes.InitPrizes(unperfectPrizes, bucketSizes);
            var (finalPrizes, finalBucketSizes, finalLeftover) = Prizes.SpendLeftover(initialPrizes, bucketSizes, leftover);
        }
    }
}