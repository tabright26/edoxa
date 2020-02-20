// Filename: ChallengePayoutBucketPrizeTest.cs
// Date Created: 2020-02-20
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

using eDoxa.Cashier.Api.Application.Factories;
using eDoxa.Cashier.Api.Application.Strategies;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengePayoutTest : UnitTest
    {
        public ChallengePayoutTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public void GetHashCode_ShouldNotBeZero()
        {
            // Arrange
            var factory = new ChallengePayoutFactory();
            var instance = factory.CreateInstance();

            var payout = instance.GetChallengePayout(ChallengePayoutEntries.OneHundred, new EntryFee(500, CurrencyType.Money));

            // Act Assert
            payout.GetHashCode().Should().NotBe(0);
        }

        [Fact]
        public void ToString_ShouldNotBeNullOrEmpty()
        {
            // Arrange
            var factory = new ChallengePayoutFactory(new DefaultChallengePayoutStrategy());
            var instance = factory.CreateInstance();

            var payout = instance.GetChallengePayout(ChallengePayoutEntries.OneHundred, new EntryFee(500, CurrencyType.Money));

            // Act
            var result = payout.ToString();

            // Assert
            result.Should().NotBeNullOrEmpty();
        }
    }
}
