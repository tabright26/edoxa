// Filename: TransactionMetadataTest.cs
// Date Created: 2019-12-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels.AccountAggregate
{
    public sealed partial class TransactionMetadataTest : UnitTest
    {
        


        [Theory]
        [MemberData(nameof(ValidTransactionMetadataTestData))]
        public void TransactionExists_WithValidTransactionMetadata_ShouldBeEquals(
            TransactionMetadata transactionMetadata,
            TransactionMetadata transactionMetadataQuery
        )
        {
            // Arrange
            var account = new Account(new UserId());
            var moneyAccount = new MoneyAccountDecorator(account);
            moneyAccount.Deposit(Money.Fifty).MarkAsSucceeded();
            moneyAccount.Charge(Money.Ten, transactionMetadata);

            // Act
            var transactionExists = account.TransactionExists(transactionMetadataQuery);

            // Assert
            transactionExists.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(InvalidTransactionMetadataTestData))]
        public void TransactionExists_WithInvalidTransactionMetadata_ShouldThrowInvalidOperationException(
            TransactionMetadata transactionMetadata,
            TransactionMetadata transactionMetadataQuery
        )
        {
            // Arrange
            var account = new Account(new UserId());
            var moneyAccount = new MoneyAccountDecorator(account);
            moneyAccount.Deposit(Money.Fifty).MarkAsSucceeded();
            moneyAccount.Charge(Money.Ten, transactionMetadata);

            // Act
            var transactionExists = account.TransactionExists(transactionMetadataQuery);

            // Assert
            transactionExists.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(ValidTransactionMetadataTestData))]
        public void FindTransaction_WithValidTransactionMetadata_ShouldBeEquals(
            TransactionMetadata transactionMetadata,
            TransactionMetadata transactionMetadataQuery
        )
        {
            // Arrange
            var account = new Account(new UserId());
            var moneyAccount = new MoneyAccountDecorator(account);
            moneyAccount.Deposit(Money.Fifty).MarkAsSucceeded();
            var expectedTransaction = moneyAccount.Charge(Money.Ten, transactionMetadata);

            // Act
            var actualTransaction = account.FindTransaction(transactionMetadataQuery);

            // Assert
            actualTransaction.Should().Be(expectedTransaction);
        }

        [Theory]
        [MemberData(nameof(InvalidTransactionMetadataTestData))]
        public void FindTransaction_WithInvalidTransactionMetadata_ShouldThrowInvalidOperationException(
            TransactionMetadata transactionMetadata,
            TransactionMetadata transactionMetadataQuery
        )
        {
            // Arrange
            var account = new Account(new UserId());
            var moneyAccount = new MoneyAccountDecorator(account);
            moneyAccount.Deposit(Money.Fifty).MarkAsSucceeded();
            moneyAccount.Charge(Money.Ten, transactionMetadata);

            // Act
            var action = new Action(() => account.FindTransaction(transactionMetadataQuery));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        public TransactionMetadataTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(testData, testMapper, testValidator)
        {
        }
    }
}
