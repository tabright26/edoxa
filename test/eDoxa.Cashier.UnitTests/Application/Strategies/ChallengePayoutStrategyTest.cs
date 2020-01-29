// Filename: ChallengePayoutStrategyTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Cashier.Api.Application.Strategies;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Application.Strategies
{
    public sealed class ChallengePayoutStrategyTest : UnitTest
    {
        public ChallengePayoutStrategyTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public void GetPayout_WithEntries_ShouldNotBeNull()
        {
            // Arrange
            var payoutStrategy = new ChallengePayoutStrategy();

            var bucket = new ChallengePayoutBucket(Prize.None, ChallengePayoutBucketSize.Individual);

            var buckets = new ChallengePayoutBuckets(
                new List<ChallengePayoutBucket>
                {
                    bucket
                });

            var payoutEntries = new ChallengePayoutEntries(buckets);

            // Act
            var payout = payoutStrategy.GetPayout(payoutEntries, new EntryFee(5000, Currency.Token));

            // Assert
            payout.Should().NotBeNull();
        }

        [Fact]
        public void GetPayout_WithoutEntries_ShouldBeNull()
        {
            // Arrange
            var payoutStrategy = new ChallengePayoutStrategy();

            var bucket = new ChallengePayoutBucket(Prize.None, ChallengePayoutBucketSize.Individual);

            var buckets = new ChallengePayoutBuckets(new List<ChallengePayoutBucket>());

            var payoutEntries = new ChallengePayoutEntries(buckets);

            // Act
            var action = new Func<IChallengePayout>(() => payoutStrategy.GetPayout(payoutEntries, new EntryFee(5000, Currency.Token)));

            // Assert
            action.Should().Throw<NotSupportedException>();
        }
    }
}
