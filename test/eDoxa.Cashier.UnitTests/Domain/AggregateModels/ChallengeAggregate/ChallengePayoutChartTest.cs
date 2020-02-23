// Filename: ChallengePayoutBucketPrizeTest.cs
// Date Created: 2020-02-20
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengePayoutChartTest : UnitTest
    {
        public ChallengePayoutChartTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact] // Francis: J<ai de la misère a comprendre c'est quoi le payoutChart, en pensant C'est Weighting, pas Weigthing
        public void GetHashCode_ShouldNotBeZero()
        {
            // Arrange
            var chart = new ChallengePayoutChart(ChallengePayoutBucketSize.Individual, 10);

            // Act Assert
            chart.GetHashCode().Should().NotBe(0);
        }

        [Fact]
        public void ToString_ShouldContainSize()
        {
            // Arrange
            var chart = new ChallengePayoutChart(ChallengePayoutBucketSize.Individual, 10);

            // Act
            var result = chart.ToString();

            // Assert
            result.Should().Contain("1");
        }
    }
}
