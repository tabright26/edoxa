// Filename: TransactionFakerTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Infrastructure.Data.Fakers
{
    public sealed class TransactionFakerTest
    {
        [Fact]
        public void Generate_SingleNegativeTransaction_ShouldNotBeNull()
        {
            // Arrange
            var transactionFaker = new TransactionFaker();

            // Act
            var transaction = transactionFaker.Generate(TransactionFaker.NegativeTransaction);

            // Assert
            transaction.Should().NotBeNull();
        }

        [Fact]
        public void Generate_SinglePositiveTransaction_ShouldNotBeNull()
        {
            // Arrange
            var transactionFaker = new TransactionFaker();

            // Act
            var transaction = transactionFaker.Generate(TransactionFaker.PositiveTransaction);

            // Assert
            transaction.Should().NotBeNull();
        }

        [Fact]
        public void Generate_TenNegativeTransactions_ShouldHaveCountTen()
        {
            // Arrange
            var transactionFaker = new TransactionFaker();

            // Act
            var transactions = transactionFaker.Generate(10, TransactionFaker.NegativeTransaction);

            // Assert
            transactions.Should().HaveCount(10);
        }

        [Fact]
        public void Generate_TenPositiveTransactions_ShouldHaveCountTen()
        {
            // Arrange
            var transactionFaker = new TransactionFaker();

            // Act
            var transactions = transactionFaker.Generate(10, TransactionFaker.PositiveTransaction);

            // Assert
            transactions.Should().HaveCount(10);
        }
    }
}
