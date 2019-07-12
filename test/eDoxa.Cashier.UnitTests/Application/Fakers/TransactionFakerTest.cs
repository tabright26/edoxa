// Filename: TransactionFakerTest.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Application.Fakers
{
    [TestClass]
    public sealed class TransactionFakerTest
    {
        [TestMethod]
        public void FakePositiveTransactions_ShouldNotThrow()
        {
            // Arrange
            var transactionFaker = new TransactionFaker();

            // Act
            var transactions = transactionFaker.Generate(10, TransactionFaker.PositiveTransaction);

            // Assert
            transactions.Should().HaveCount(10);
        }

        [TestMethod]
        public void FakePositiveTransaction_ShouldNotThrow()
        {
            // Arrange
            var transactionFaker = new TransactionFaker();

            // Act
            var transaction = transactionFaker.Generate(TransactionFaker.PositiveTransaction);

            // Assert
            transaction.Should().NotBeNull();
        }

        [TestMethod]
        public void FakeNegativeTransactions_ShouldNotThrow()
        {
            // Arrange
            var transactionFaker = new TransactionFaker();

            // Act
            var transactions = transactionFaker.Generate(10, TransactionFaker.NegativeTransaction);

            // Assert
            transactions.Should().HaveCount(10);
        }

        [TestMethod]
        public void FakeNegativeTransaction_ShouldNotThrow()
        {
            // Arrange
            var transactionFaker = new TransactionFaker();

            // Act
            var transaction = transactionFaker.Generate(TransactionFaker.NegativeTransaction);

            // Assert
            transaction.Should().NotBeNull();
        }
    }
}
