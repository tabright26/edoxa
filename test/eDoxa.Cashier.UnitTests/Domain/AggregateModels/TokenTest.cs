﻿// Filename: TokenTest.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels
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

        [DataRow(100000, 50000, 50000)]
        [DataRow(1000000, 500000, 500000)]
        [DataRow(1000000, 150000, 850000)]
        [DataRow(1000000, 250000, 750000)]
        [DataRow(5000000, 1000000, 4000000)]
        [DataTestMethod]
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
    }
}
