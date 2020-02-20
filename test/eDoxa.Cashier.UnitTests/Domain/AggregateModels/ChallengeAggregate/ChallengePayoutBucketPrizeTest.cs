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
    public sealed class ChallengePayoutBucketPrizeTest : UnitTest
    {
        public ChallengePayoutBucketPrizeTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public void Constructor_WhenAmountNegative_ShouldThrowArgumentException()
        {
            // Arrange Act Assert
            var action = new Action(() => new ChallengePayoutBucketPrize(-1, CurrencyType.Money));
            action.Should().Throw<ArgumentException>();
        }
    }
}
