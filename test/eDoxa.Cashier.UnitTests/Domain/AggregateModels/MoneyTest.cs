// Filename: MoneyTest.cs
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
    public sealed class MoneyTest : UnitTest
    {
        public MoneyTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(testData, testMapper, testValidator)
        {
        }

        [InlineData(10, 10, 20)]
        [InlineData(10, 20, 30)]
        [InlineData(20, 50, 70)]
        [InlineData(100, 10, 110)]
        [InlineData(100, 50, 150)]
        [Theory]
        public void Add_ShouldBeResult(int amount1, int amount2, int result)
        {
            // Arrange
            var money1 = new Money(amount1);
            var money2 = new Money(amount2);

            // Act
            var money = money1 + money2;

            // Assert
            money.As<decimal>().Should().Be(result);
        }

        [InlineData(10, 10, 0)]
        [InlineData(20, 10, 10)]
        [InlineData(50, 20, 30)]
        [InlineData(100, 10, 90)]
        [InlineData(100, 50, 50)]
        [Theory]
        public void Subtract_ShouldBeResult(int amount1, int amount2, int result)
        {
            // Arrange
            var money1 = new Money(amount1);
            var money2 = new Money(amount2);

            // Act
            var money = money1 - money2;

            // Assert
            money.As<decimal>().Should().Be(result);
        }

        [Theory]
        [InlineData(10, 1000)]
        [InlineData(20, 2000)]
        [InlineData(50, 5000)]
        public void ToToken_ShouldBeResult(int amount, int result)
        {
            // Arrange
            var money = new Money(amount);

            // Act
            var token = money.ToToken();

            // Assert
            token.Amount.Should().Be(result);
        }
    }
}
