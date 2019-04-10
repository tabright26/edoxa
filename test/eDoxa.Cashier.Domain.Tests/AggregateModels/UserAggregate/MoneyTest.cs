// Filename: MoneyTest.cs
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
    public sealed class MoneyTest
    {
        [DataRow(10, 10, 20)]
        [DataRow(10, 20, 30)]
        [DataRow(20, 50, 70)]
        [DataRow(100, 10, 110)]
        [DataRow(100, 50, 150)]
        [DataTestMethod]
        public void Add_Amount_ShouldBeResult(int amount1, int amount2, int result)
        {
            // Arrange
            var money1 = Money.FromDecimal(amount1);
            var money2 = Money.FromDecimal(amount2);

            // Act
            var money = money1 + money2;

            // Assert
            money.ToDecimal().Should().Be(result);
        }

        [DataRow(10, 10, 0)]
        [DataRow(20, 10, 10)]
        [DataRow(50, 20, 30)]
        [DataRow(100, 10, 90)]
        [DataRow(100, 50, 50)]
        [DataTestMethod]
        public void Subtract_Amount_ShouldBeResult(int amount1, int amount2, int result)
        {
            // Arrange
            var money1 = Money.FromDecimal(amount1);
            var money2 = Money.FromDecimal(amount2);

            // Act
            var money = money1 - money2;

            // Assert
            money.ToDecimal().Should().Be(result);
        }

        [DataRow(10, 3, 30)]
        [DataRow(20, 5, 100)]
        [DataRow(50, 2, 100)]
        [DataRow(100, 5, 500)]
        [DataRow(500, 1, 500)]
        [DataTestMethod]
        public void Muliply_Amount_ShouldBeResult(int amount, int multiplier, int result)
        {
            // Arrange
            var money = Money.FromDecimal(amount);

            // Act
            money = money * multiplier;

            // Assert
            money.ToDecimal().Should().Be(result);
        }
    }
}