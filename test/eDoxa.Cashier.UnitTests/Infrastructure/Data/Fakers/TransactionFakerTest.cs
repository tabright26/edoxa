// Filename: TransactionFakerTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;
using eDoxa.Cashier.UnitTests.TestHelpers;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Infrastructure.Data.Fakers
{
    public sealed class TransactionFakerTest : UnitTest
    {
        public TransactionFakerTest(CashierFakerFixture faker) : base(faker)
        {
        }

        [Fact]
        public void Generate_SingleNegativeTransaction_ShouldNotBeNull()
        {
            // Arrange
            var transactionFaker = Faker.TransactionFactory.CreateFaker(null);

            // Act
            var transaction = transactionFaker.FakeTransaction(TransactionFaker.NegativeTransaction);

            // Assert
            transaction.Should().NotBeNull();
        }

        [Fact]
        public void Generate_SinglePositiveTransaction_ShouldNotBeNull()
        {
            // Arrange
            var transactionFaker = Faker.TransactionFactory.CreateFaker(null);

            // Act
            var transaction = transactionFaker.FakeTransaction(TransactionFaker.PositiveTransaction);

            // Assert
            transaction.Should().NotBeNull();
        }

        [Fact]
        public void Generate_TenNegativeTransactions_ShouldHaveCountTen()
        {
            // Arrange
            var transactionFaker = Faker.TransactionFactory.CreateFaker(null);

            // Act
            var transactions = transactionFaker.FakeTransactions(10, TransactionFaker.NegativeTransaction);

            // Assert
            transactions.Should().HaveCount(10);
        }

        [Fact]
        public void Generate_TenPositiveTransactions_ShouldHaveCountTen()
        {
            // Arrange
            var transactionFaker = Faker.TransactionFactory.CreateFaker(null);

            // Act
            var transactions = transactionFaker.FakeTransactions(10, TransactionFaker.PositiveTransaction);

            // Assert
            transactions.Should().HaveCount(10);
        }
    }
}
