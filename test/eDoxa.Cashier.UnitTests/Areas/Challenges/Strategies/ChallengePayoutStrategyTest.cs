﻿// Filename: PayoutFactoryTest.cs
// Date Created: 2019-09-16
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Cashier.Api.Areas.Challenges.Factories;
using eDoxa.Cashier.Api.Areas.Challenges.Strategies;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Strategies;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

using FluentAssertions;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Areas.Challenges.Strategies
{
    public sealed class ChallengePayoutStrategyTest : UnitTest
    {
        public ChallengePayoutStrategyTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void GetPayout_WithEntries_ShouldNotBeNull()
        {
            // Arrange
            var payoutStrategy = new ChallengePayoutStrategy();

            var bucket = new Bucket(Prize.None, BucketSize.Individual);

            var buckets = new Buckets(
                new List<Bucket>()
                {
                    bucket
                });

            var payoutEntries = new PayoutEntries(buckets);

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

            var bucket = new Bucket(Prize.None, BucketSize.Individual);

            var buckets = new Buckets(
                new List<Bucket>()
                {
                });

            var payoutEntries = new PayoutEntries(buckets);

            // Act
            var action = new Func<IPayout>( () => payoutStrategy.GetPayout(payoutEntries, new EntryFee(5000, Currency.Token)));

            // Assert
            action.Should().Throw<NotSupportedException>();
        }

    }
}
