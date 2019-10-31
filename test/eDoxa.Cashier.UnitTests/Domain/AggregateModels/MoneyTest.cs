// Filename: MoneyTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels
{
    public sealed class MoneyTest : UnitTest
    {
        public MoneyTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
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
    }
}
