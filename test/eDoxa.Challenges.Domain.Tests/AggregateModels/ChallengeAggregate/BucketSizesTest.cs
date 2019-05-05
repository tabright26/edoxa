// Filename: BucketSizesTest.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class BucketSizesTest
    {
        private static readonly ChallengeAggregateFactory ChallengeAggregateFactory = ChallengeAggregateFactory.Instance;

        [DataRow(5000, 15)]
        [DataRow(4000, 14)]
        [DataRow(3000, 13)]
        [DataRow(2000, 12)]
        [DataRow(1500, 11)]
        [DataRow(1000, 10)]
        [DataRow(500, 9)]
        [DataRow(100, 8)]
        [DataRow(50, 7)]
        [DataRow(20, 6)]
        [DataRow(19, 5)]
        [DataRow(6, 5)]
        [DataRow(5, 5)]
        [DataRow(4, 4)]
        [DataRow(3, 3)]
        [DataRow(2, 2)]
        [DataRow(1, 1)]
        [DataRow(0, 0)]
        [DataTestMethod]
        public void Constructor(int payoutEntries, int bucketCount)
        {
            // Act
            var bucketSizes = ChallengeAggregateFactory.CreateBucketSizes(payoutEntries);

            // Assert
            bucketSizes.PayoutEntries().Should().Be(payoutEntries);
            bucketSizes.Count.Should().Be(bucketCount);
        }
    }
}