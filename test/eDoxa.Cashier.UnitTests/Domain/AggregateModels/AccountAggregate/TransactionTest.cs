// Filename: TransactionTest.cs
// Date Created: 2019-12-18
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels.AccountAggregate
{
    public sealed class TransactionTest : UnitTest
    {
        public TransactionTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(testData, testMapper, testValidator)
        {
        }

        [Fact]
        public void MarkAsCanceled_WhenTransactionStatusPending_ShouldBeStatusSucceded()
        {
            // Arrange
            var currency = Money.Twenty;
            var description = new TransactionDescription("Transaction to cancel");
            var type = TransactionType.Withdrawal;
            var provider = new UtcNowDateTimeProvider();

            var transaction = new Transaction(
                currency,
                description,
                type,
                provider);

            // Act
            transaction.MarkAsCanceled();

            // Assert
            transaction.Status.Should().Be(TransactionStatus.Canceled);
        }

        [Fact]
        public void MarkAsCanceled_WhenTransactionStatusSucceeded_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var currency = Money.Twenty;
            var description = new TransactionDescription("Transaction to cancel");
            var type = TransactionType.Withdrawal;
            var provider = new UtcNowDateTimeProvider();

            var transaction = new Transaction(
                currency,
                description,
                type,
                provider);

            transaction.MarkAsSucceeded();

            // Act
            var action = new Action(() => transaction.MarkAsCanceled());

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void MarkAsFailed_StatusPending_ShouldBeStatusFailed()
        {
            // Arrange
            var currency = Money.Fifty;
            var description = new TransactionDescription("Test");
            var type = TransactionType.Deposit;
            var provider = new UtcNowDateTimeProvider();

            var transaction = new Transaction(
                currency,
                description,
                type,
                provider);

            // Act
            transaction.MarkAsFailed();

            // Assert
            transaction.Status.Should().Be(TransactionStatus.Failed);
        }

        [Fact]
        public void MarkAsFailed_WhenTransactionStatusSucceeded_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var currency = Money.Fifty;
            var description = new TransactionDescription("Test");
            var type = TransactionType.Deposit;
            var provider = new UtcNowDateTimeProvider();

            var transaction = new Transaction(
                currency,
                description,
                type,
                provider);

            transaction.MarkAsSucceeded();

            // Act
            var action = new Action(() => transaction.MarkAsFailed());

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void MarkAsSucceeded_StatusPending_ShouldBeStatusSucceded()
        {
            // Arrange
            var currency = Money.Fifty;
            var description = new TransactionDescription("Test");
            var type = TransactionType.Deposit;
            var provider = new UtcNowDateTimeProvider();

            var transaction = new Transaction(
                currency,
                description,
                type,
                provider);

            // Act
            transaction.MarkAsSucceeded();

            // Assert
            transaction.Status.Should().Be(TransactionStatus.Succeeded);
        }

        [Fact]
        public void MarkAsSucceeded_WhenTransactionStatusFailed_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var currency = Money.Fifty;
            var description = new TransactionDescription("Test");
            var type = TransactionType.Deposit;
            var provider = new UtcNowDateTimeProvider();

            var transaction = new Transaction(
                currency,
                description,
                type,
                provider);

            transaction.MarkAsFailed();

            // Act
            var action = new Action(() => transaction.MarkAsSucceeded());

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Price_WithTransactionFromMoney_ShouldBePriceMoney()
        {
            // Arrange
            var currency = Money.Twenty;
            var description = new TransactionDescription("Price to check");
            var type = TransactionType.Withdrawal;
            var provider = new UtcNowDateTimeProvider();

            var transaction = new Transaction(
                currency,
                description,
                type,
                provider);

            // Act Assert
            transaction.Price.Should().NotBeNull();
            var price = transaction.Price;
            price.Amount.Should().Be(20);
            price.Type.Should().Be(CurrencyType.Money);
        }

        [Fact]
        public void Price_WithTransactionFromToken_ShouldBePriceMoney()
        {
            // Arrange
            var currency = Token.FiftyThousand;
            var description = new TransactionDescription("Price to check");
            var type = TransactionType.Deposit;
            var provider = new UtcNowDateTimeProvider();

            var transaction = new Transaction(
                currency,
                description,
                type,
                provider);

            // Act Assert
            transaction.Price.Should().NotBeNull();
            var price = transaction.Price;
            price.Amount.Should().Be(500);
            price.Type.Should().Be(CurrencyType.Money);
        }

        [Fact]
        public void Transaction_WithConstructor_ShouldBeValid()
        {
            // Arrange
            var currency = Money.Fifty;
            var description = new TransactionDescription("Test");
            var type = TransactionType.Deposit;
            var provider = new UtcNowDateTimeProvider();

            // Act
            var transaction = new Transaction(
                currency,
                description,
                type,
                provider);

            // Assert
            transaction.Timestamp.Should().Be(provider.DateTime);
            transaction.Currency.Should().Be(currency);
            transaction.Description.Should().Be(description);
            transaction.Type.Should().Be(type);
            transaction.Status.Should().Be(TransactionStatus.Pending);
        }
    }
}
