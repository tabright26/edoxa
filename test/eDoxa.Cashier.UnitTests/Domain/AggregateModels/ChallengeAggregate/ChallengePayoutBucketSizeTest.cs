// Filename: ChallengePayoutBucketPrizeTest.cs
// Date Created: 2020-02-20
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengePayoutBucketSizeTest : UnitTest
    {
        public ChallengePayoutBucketSizeTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public void Constructor_WhenAmountIsZero_ShouldThrowArgumentException()
        {
            // Arrange Act Assert
            var action = new Action(() =>  new ChallengePayoutBucketSize(0));
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void SizeComparedTo_WhenDifferentAmount_ShouldBeTrue()
        {
            // Arrange
            var size1 = ChallengePayoutBucketSize.Individual;

            var size2 = new
            {
                size = 100
            };

            // Act Assert
            Convert.ToBoolean(size1.CompareTo(size2)).Should().BeTrue();
        }

        [Fact]
        public void SizeComparedTo_WhenSameAmount_ShouldBeFalse()
        {
            // Arrange
            var size1 = ChallengePayoutBucketSize.Individual;

            var size2 = new ChallengePayoutBucketSize(size1);

            // Act Assert
            Convert.ToBoolean(size1.CompareTo(size2)).Should().BeFalse();
        }

    }
}
