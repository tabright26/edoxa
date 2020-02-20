// Filename: PriceTest.cs
// Date Created: 2020-02-17
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels
{
    public sealed class CurrencyTypeTest : UnitTest
    {
        public CurrencyTypeTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(testData, testMapper, testValidator)
        {
        }

        [Fact]
        public void ToCurrency_WhenMoneyType_ShouldBeMoney()
        {
            // Arrange
            var type = CurrencyType.Money;

            // Act
            var result = type.ToCurrency(Money.FiveHundred);

            // Assert
            result.Should().NotBeNull();
            result.Amount.Should().Be(Money.FiveHundred);
            result.Type.Should().Be(CurrencyType.Money);
        }

        [Fact]
        public void ToCurrency_WhenTokenType_ShouldBeToken()
        {
            // Arrange
            var type = CurrencyType.Token;

            // Act
            var result = type.ToCurrency(Token.FiftyThousand);

            // Assert
            result.Should().NotBeNull();
            result.Amount.Should().Be(Token.FiftyThousand);
            result.Type.Should().Be(CurrencyType.Token);
        }

        [Fact]
        public void ToCurrency_WhenEmptyType_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var type = new CurrencyType();

            // Act Assert
            var action = new Action(() => type.ToCurrency(0));
            action.Should().Throw<InvalidOperationException>();
        }
    }
}
