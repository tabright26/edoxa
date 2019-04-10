// Filename: TokenTest.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Domain.Tests.AggregateModels.UserAggregate
{
    [TestClass]
    public sealed class TokenTest
    {
        [DataRow(50000, 100000, 150000)]
        [DataRow(100000, 50000, 150000)]
        [DataRow(100000, 500000, 600000)]
        [DataRow(500000, 100000, 600000)]
        [DataRow(500000, 1000000, 1500000)]
        [DataRow(1000000, 500000, 1500000)]
        [DataTestMethod]
        public void Add_Amount_ShouldBeResult(int amount1, int amount2, int result)
        {
            // Arrange
            var token1 = Token.FromDecimal(amount1);
            var token2 = Token.FromDecimal(amount2);

            // Act
            var token = token1 + token2;

            // Assert
            token.ToDecimal().Should().Be(result);
        }

        [DataRow(100000, 50000, 50000)]
        [DataRow(1000000, 500000, 500000)]
        [DataRow(1000000, 150000, 850000)]
        [DataRow(1000000, 250000, 750000)]
        [DataRow(5000000, 1000000, 4000000)]
        [DataTestMethod]
        public void Subtract_Amount_ShouldBeResult(int amount1, int amount2, int result)
        {
            // Arrange
            var token1 = Token.FromDecimal(amount1);
            var token2 = Token.FromDecimal(amount2);

            // Act
            var token = token1 - token2;

            // Assert
            token.ToDecimal().Should().Be(result);
        }

        [DataRow(100000, 3, 300000)]
        [DataRow(1000000, 5, 5000000)]
        [DataRow(500000, 2, 1000000)]
        [DataRow(100000, 5, 500000)]
        [DataRow(10000, 10, 100000)]
        [DataTestMethod]
        public void Muliply_Amount_ShouldBeResult(int amount, int multiplier, int result)
        {
            // Arrange
            var token = Token.FromDecimal(amount);

            // Act
            token = token * multiplier;

            // Assert
            token.ToDecimal().Should().Be(result);
        }
    }
}