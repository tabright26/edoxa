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

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class PayoutTest
    {
        [DataRow(2500, 25D, PayoutRatio.Default, ServiceChargeRatio.Default)]
        //[DataRow(2500, 25D, PayoutRatio.Default, ServiceChargeRatio.Min)]
        //[DataRow(2500, 25D, PayoutRatio.Default, ServiceChargeRatio.Max)]
        //[DataRow(2500, 25D, PayoutRatio.Min, ServiceChargeRatio.Default)]
        //[DataRow(2500, 25D, PayoutRatio.Max, ServiceChargeRatio.Default)]
        //[DataRow(2500, EntryFee.Min, PayoutRatio.Default, ServiceChargeRatio.Default)]
        //[DataRow(2500, EntryFee.Max, PayoutRatio.Default, ServiceChargeRatio.Default)]
        //[DataRow(Entries.Min, EntryFee.Default, PayoutRatio.Default, ServiceChargeRatio.Default)]
        //[DataRow(Entries.Max, EntryFee.Default, PayoutRatio.Default, ServiceChargeRatio.Default)]
        [DataTestMethod]
        public void M(int e, double ef, float pr, float scr)
        {
            // Arrange
            var entries = new Entries(e);
            var entryFee = new EntryFee(new decimal(ef));
            var payoutRatio = new PayoutRatio(pr);
            var serviceChargeRatio = new ServiceChargeRatio(scr);
            var payoutEntries = new PayoutEntries(entries, payoutRatio);
            var prizePool = new PrizePool(entries, entryFee, serviceChargeRatio);

            // Act
            var payout = new Payout(payoutEntries, prizePool, entryFee);

            // Assert
            payoutEntries.Should().Be(new PayoutEntries(payout));
            prizePool.Should().Be(new PrizePool(payout));
        }
    }
}