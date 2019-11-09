// Filename: TransactionTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels.TransactionAggregate
{
    public sealed class TransactionTest : UnitTest
    {
        public TransactionTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
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
                new TransactionId(),
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
        public void MarkAsFailed_WhenTransactionStatusSucceded_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var currency = Money.Fifty;
            var description = new TransactionDescription("Test");
            var type = TransactionType.Deposit;
            var provider = new UtcNowDateTimeProvider();

            var transaction = new Transaction(
                new TransactionId(),
                currency,
                description,
                type,
                provider);

            transaction.MarkAsSucceded();

            // Act
            var action = new Action(() => transaction.MarkAsFailed());

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void MarkAsSucceded_StatusPending_ShouldBeStatusSucceded()
        {
            // Arrange
            var currency = Money.Fifty;
            var description = new TransactionDescription("Test");
            var type = TransactionType.Deposit;
            var provider = new UtcNowDateTimeProvider();

            var transaction = new Transaction(
                new TransactionId(),
                currency,
                description,
                type,
                provider);

            // Act
            transaction.MarkAsSucceded();

            // Assert
            transaction.Status.Should().Be(TransactionStatus.Succeded);
        }

        [Fact]
        public void MarkAsSucceded_WhenTransactionStatusFailed_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var currency = Money.Fifty;
            var description = new TransactionDescription("Test");
            var type = TransactionType.Deposit;
            var provider = new UtcNowDateTimeProvider();

            var transaction = new Transaction(
                new TransactionId(),
                currency,
                description,
                type,
                provider);

            transaction.MarkAsFailed();

            // Act
            var action = new Action(() => transaction.MarkAsSucceded());

            // Assert
            action.Should().Throw<InvalidOperationException>();
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
                new TransactionId(),
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
