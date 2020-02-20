// Filename: ChallengePayoutBucketPrizeTest.cs
// Date Created: 2020-02-20
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengePayoutBucketTest : UnitTest
    {
        public ChallengePayoutBucketTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public void ToString_ShouldContainPrizeAndType()
        {
            // Arrange
            const int amount = 5;
            var type = CurrencyType.Money;
            var bucket = new ChallengePayoutBucket(new ChallengePayoutBucketPrize(amount, type), ChallengePayoutBucketSize.Individual);

            // Act
            var result = bucket.ToString();

            // Assert
            result.Should().Contain(amount.ToString(), type.ToString());
        }

        [Fact]
        public void GetHashCode_ShouldNotBeZero()
        {
            // Arrange
            const int amount = 5;
            var type = CurrencyType.Money;
            var bucket = new ChallengePayoutBucket(new ChallengePayoutBucketPrize(amount, type), ChallengePayoutBucketSize.Individual);

            // Act Assert
            bucket.GetHashCode().Should().NotBe(0);
        }
    }
}
