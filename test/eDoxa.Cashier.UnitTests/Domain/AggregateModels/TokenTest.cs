﻿// Filename: TokenTest.cs
// Date Created: 2019-11-25
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels
{
    public sealed class ChallengePayoutBucketPrizeTest : UnitTest
    {
        public ChallengePayoutBucketPrizeTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(testData, testMapper, testValidator)
        {
        }

        [InlineData(50000, 100000, 150000)]
        [InlineData(100000, 50000, 150000)]
        [InlineData(100000, 500000, 600000)]
        [InlineData(500000, 100000, 600000)]
        [InlineData(500000, 1000000, 1500000)]
        [InlineData(1000000, 500000, 1500000)]
        [Theory]
        public void Add_ShouldBeResult(int amount1, int amount2, int result)
        {
            // Arrange
            var token1 = new Token(amount1);
            var token2 = new Token(amount2);

            // Act
            var token = token1 + token2;

            // Assert
            token.As<decimal>().Should().Be(result);
        }

        [InlineData(100000, 50000, 50000)]
        [InlineData(1000000, 500000, 500000)]
        [InlineData(1000000, 150000, 850000)]
        [InlineData(1000000, 250000, 750000)]
        [InlineData(5000000, 1000000, 4000000)]
        [Theory]
        public void Subtract_ShouldBeResult(int amount1, int amount2, int result)
        {
            // Arrange
            var token1 = new Token(amount1);
            var token2 = new Token(amount2);

            // Act
            var token = token1 - token2;

            // Assert
            token.As<decimal>().Should().Be(result);
        }

        [Theory]
        [InlineData(1000, 10)]
        [InlineData(2000, 20)]
        [InlineData(5000, 50)]
        public void ToMoney_ShouldBeResult(int amount, int result)
        {
            // Arrange
            var token = new Token(amount);

            // Act
            var money = token.ToMoney();

            // Assert
            money.Amount.Should().Be(result);
        }
    }
}
