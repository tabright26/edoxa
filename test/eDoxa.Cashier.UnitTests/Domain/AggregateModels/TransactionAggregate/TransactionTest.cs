// Filename: TransactionTest.cs
// Date Created: 2019-07-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Seedwork.Common;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels.TransactionAggregate
{
    [TestClass]
    public sealed class TransactionTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            // Arrange
            var currency = Money.Fifty;
            var description = new TransactionDescription("Test");
            var type = TransactionType.Deposit;
            var provider = new UtcNowDateTimeProvider();

            // Act
            var transaction = new Transaction(currency, description, type, provider);

            // Assert
            transaction.Timestamp.Should().Be(provider.DateTime);
            transaction.Currency.Should().Be(currency);
            transaction.Description.Should().Be(description);
            transaction.Type.Should().Be(type);
            transaction.Status.Should().Be(TransactionStatus.Pending);
        }

        [TestMethod]
        public void MarkAsSucceded_StatusPending_ShouldBeStatusSucceded()
        {
            // Arrange
            var currency = Money.Fifty;
            var description = new TransactionDescription("Test");
            var type = TransactionType.Deposit;
            var provider = new UtcNowDateTimeProvider();
            var transaction = new Transaction(currency, description, type, provider);

            // Act
            transaction.MarkAsSucceded();

            // Assert
            transaction.Status.Should().Be(TransactionStatus.Succeded);
        }

        [TestMethod]
        public void MarkAsFailed_StatusPending_ShouldBeStatusFailed()
        {
            // Arrange
            var currency = Money.Fifty;
            var description = new TransactionDescription("Test");
            var type = TransactionType.Deposit;
            var provider = new UtcNowDateTimeProvider();
            var transaction = new Transaction(currency, description, type, provider);

            // Act
            transaction.MarkAsFailed();

            // Assert
            transaction.Status.Should().Be(TransactionStatus.Failed);
        }

        [TestMethod]
        public void MarkAsSucceded_StatusSucceded_ShouldBeStatusSucceded()
        {
            // Arrange
            var currency = Money.Fifty;
            var description = new TransactionDescription("Test");
            var type = TransactionType.Deposit;
            var provider = new UtcNowDateTimeProvider();
            var transaction = new Transaction(currency, description, type, provider);
            transaction.MarkAsSucceded();

            // Act
            transaction.MarkAsFailed();

            // Assert
            transaction.Status.Should().Be(TransactionStatus.Succeded);
        }

        [TestMethod]
        public void MarkAsFailed_StatusFailed_ShouldBeStatusFailed()
        {
            // Arrange
            var currency = Money.Fifty;
            var description = new TransactionDescription("Test");
            var type = TransactionType.Deposit;
            var provider = new UtcNowDateTimeProvider();
            var transaction = new Transaction(currency, description, type, provider);
            transaction.MarkAsFailed();

            // Act
            transaction.MarkAsSucceded();

            // Assert
            transaction.Status.Should().Be(TransactionStatus.Failed);
        }
    }
}
