// Filename: AccountTest.cs
// Date Created: 2019-11-25
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using Bogus.DataSets;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels.AccountAggregate
{
    public sealed class AccountDecoratorTest : UnitTest
    {
        public AccountDecoratorTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(testData, testMapper, testValidator)
        {
        }

        [Fact]
        public void TransactionExists_WithExistingTransactionId_ShouldBeTrue()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);
            var decorator = new MoneyAccountDecorator(account);

            // Act
            var transaction = new TransactionBuilder(TransactionType.Deposit, new Money(50)).Build();
            decorator.CreateTransaction(transaction);

            // Assert
            decorator.TransactionExists(transaction.Id).Should().BeTrue();
        }

        [Fact]
        public void TransactionExists_WithExistingTransactionMetadata_ShouldBeTrue()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);
            var decorator = new MoneyAccountDecorator(account);

            // Act
            var transaction = new TransactionBuilder(TransactionType.Deposit, new Money(50)).WithMetadata(new TransactionMetadata() { { "test", "value" } }).Build();
            decorator.CreateTransaction(transaction);

            // Assert
            decorator.TransactionExists(transaction.Metadata).Should().BeTrue();
        }

        [Fact]
        public void TransactionExists_WhenEmpty_ShouldBeFalse()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);
            var decorator = new MoneyAccountDecorator(account);

            // Assert
            decorator.TransactionExists(new TransactionId()).Should().BeFalse();
        }

        [Fact]
        public void FindTransaction_WithExistingTransactionId_ShouldBeTrue()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);
            var decorator = new MoneyAccountDecorator(account);

            // Act
            var transaction = new TransactionBuilder(TransactionType.Deposit, new Money(50)).Build();
            decorator.CreateTransaction(transaction);

            // Assert
            var result = decorator.FindTransaction(transaction.Id);
            result.Should().NotBeNull();
            result.Should().BeSameAs(transaction);
        }

        [Fact]
        public void FindTransaction_WithExistingTransactionMetadata_ShouldBeTrue()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);
            var decorator = new MoneyAccountDecorator(account);

            // Act
            var transaction = new TransactionBuilder(TransactionType.Deposit, new Money(50)).WithMetadata(new TransactionMetadata() { { "test", "value" } }).Build();
            decorator.CreateTransaction(transaction);

            // Assert
            var result = decorator.FindTransaction(transaction.Metadata);
            result.Should().NotBeNull();
            result.Should().BeSameAs(transaction);
        }

        [Fact]
        public void FindTransaction_WhenEmpty_ShouldBeNull()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);
            var decorator = new MoneyAccountDecorator(account);

            // Act
            decorator.FindTransaction(new TransactionId()).Should().BeNull();
        }

        [Fact]
        public void GetBalanceFor_Money_ShouldBeNull()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);
            var decorator = new MoneyAccountDecorator(account);

            // Act
            var transactionList = new List<Transaction>
            {
                new Transaction(new Money(50), new TransactionDescription("deposit"), TransactionType.Deposit, new UtcNowDateTimeProvider()),
                new Transaction(new Money(20), new TransactionDescription("withdrawal"), TransactionType.Withdrawal, new UtcNowDateTimeProvider())
            };


            foreach (var transaction in transactionList)
            {
                transaction.MarkAsSucceeded();
                decorator.CreateTransaction(transaction);
            }

            // Assert
            var result = decorator.GetBalanceFor(CurrencyType.Money);
            result.Available.Should().Be(30);
            result.Pending.Should().Be(0);
        }
    }
}
