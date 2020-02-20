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
    public sealed class CurrencyTest : UnitTest
    {
        public CurrencyTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(testData, testMapper, testValidator)
        {
        }

        // Francis: Je suis tu mieux d<utiliser les valeurs constante genre Money.FiftyThousand ???

        [Theory]
        [InlineData(0,50000)]
        [InlineData(500,0)]
        public void MoneyComparedTo_WhenDifferentAmount_ShouldBeTrue(int moneyAmount, int tokenAmount)
        {
            // Arrange
            var money = new Money(moneyAmount);

            var token = new
            {
                type = CurrencyType.Token,
                amount = tokenAmount
            };

            // Act Assert
            Convert.ToBoolean(money.CompareTo(token)).Should().BeTrue();
        }

        [Theory]
        [InlineData(0,500)]
        [InlineData(50000,0)]
        public void TokenComparedTo_WhenDifferentAmount_ShouldBeTrue(int tokenAmount, int moneyAmount)
        {
            // Arrange
            var token = new Token(tokenAmount);

            var money = new
            {
                type = CurrencyType.Money,
                amount = moneyAmount
            };

            // Act Assert
            Convert.ToBoolean(token.CompareTo(money)).Should().BeTrue();
        }

        [Theory]
        [InlineData(0,0)]
        [InlineData(500,500)]
        public void CurrencyTypeComparedTo_WhenSame_ShouldBeFalse(int moneyAmount, int tokenAmount)
        {
            // Arrange
            var money = new Money(moneyAmount);
            var token = new Token(tokenAmount);

            // Act Assert
            Convert.ToBoolean(money.CompareTo(token)).Should().BeFalse();
            Convert.ToBoolean(token.CompareTo(money)).Should().BeFalse();
        }

    }
}
