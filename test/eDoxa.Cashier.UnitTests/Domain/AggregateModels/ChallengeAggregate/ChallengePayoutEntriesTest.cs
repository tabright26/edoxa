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
    public sealed class ChallengePayoutEntriesTest : UnitTest
    {
        public ChallengePayoutEntriesTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public void Entries_AsString_ShouldBeEqualToAmount()
        {
            // Arrange
            var one = ChallengePayoutEntries.One;
            var two = ChallengePayoutEntries.Two;
            var three = ChallengePayoutEntries.Three;
            var four = ChallengePayoutEntries.Four;
            var five = ChallengePayoutEntries.Five;
            var ten = ChallengePayoutEntries.Ten;
            var fifteen = ChallengePayoutEntries.Fifteen;
            var twenty = ChallengePayoutEntries.Twenty;
            var twentyFive = ChallengePayoutEntries.TwentyFive;
            var fifty = ChallengePayoutEntries.Fifty;
            var seventyFive = ChallengePayoutEntries.SeventyFive;
            var oneHundred = ChallengePayoutEntries.OneHundred;

            // Act Assert
            one.ToString().Should().Be("1");
            two.ToString().Should().Be("2");
            three.ToString().Should().Be("3");
            four.ToString().Should().Be("4");
            five.ToString().Should().Be("5");
            ten.ToString().Should().Be("10");
            fifteen.ToString().Should().Be("15");
            twenty.ToString().Should().Be("20");
            twentyFive.ToString().Should().Be("25");
            fifty.ToString().Should().Be("50");
            seventyFive.ToString().Should().Be("75");
            oneHundred.ToString().Should().Be("100");
        }
    }
}
