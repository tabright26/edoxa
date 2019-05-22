// Filename: ChallengePayoutFactoryTest.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using eDoxa.Arena.Challenges.Services.Factories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.Domain.Services.Tests.Factories
{
    [TestClass]
    public sealed class PayoutFactoryTest
    {
        private static readonly PayoutFactory PayoutFactory = PayoutFactory.Instance;

        [TestMethod]
        public void M()
        {
            var payout = PayoutFactory.Create(PayoutEntryType.Twenty, EntryFeeType.TwoAndHalf);

            var t = payout.Buckets.SelectMany(x => x.Items).Count();
        }
    }
}