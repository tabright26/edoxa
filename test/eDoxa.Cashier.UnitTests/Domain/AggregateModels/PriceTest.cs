// Filename: PriceTest.cs
// Date Created: 2020-02-17
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
    public sealed class PriceTest : UnitTest
    {
        public PriceTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(testData, testMapper, testValidator)
        {
        }

        [Theory]
        [InlineData(1000, 10)]
        [InlineData(2000, 20)]
        [InlineData(5000, 50)]
        public void FromToken_ShouldBeEqualToMoneyResult(int amount, int result)
        {
            // Arrange
            var token = new Token(amount);
            var price = new Price(token);

            // Assert
            price.Amount.Should().Be(result);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(50)]
        public void FromMoney_ShouldBeSameAsAmount(int amount)
        {
            // Arrange
            var money = new Money(amount);
            var price = new Price(money);

            // Assert
            price.Amount.Should().Be(amount);
        }
    }
}
