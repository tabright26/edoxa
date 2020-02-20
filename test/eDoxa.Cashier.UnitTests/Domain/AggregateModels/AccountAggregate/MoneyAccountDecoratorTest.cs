// Filename: TokenAccountDecoratorTest.cs
// Date Created: 2020-02-20
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

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
    public sealed class MoneyAccountDecoratorTest : UnitTest
    {
        public MoneyAccountDecoratorTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public void Deposit_WhenDepositRightBefore_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);
            var moneyAccountDecorator = new MoneyAccountDecorator(account);

            var transaction = new Transaction(
                new Money(500),
                new TransactionDescription("Test"),
                TransactionType.Deposit,
                new UtcNowDateTimeProvider());

            transaction.MarkAsSucceeded();

            moneyAccountDecorator.CreateTransaction(transaction);

            var money = new Money(500);

            // Act Assert
            var action = new Action(() => moneyAccountDecorator.Deposit(money));
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Charge_WhenBalanceIsZero_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);
            var moneyAccountDecorator = new MoneyAccountDecorator(account);

            // Act Assert
            var action = new Action(() => moneyAccountDecorator.Charge(new Money(500)));
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Withdrawal_WhenWithdrawalRightBefore_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);
            var moneyAccountDecorator = new MoneyAccountDecorator(account);

            var transaction = new Transaction(
                new Money(500),
                new TransactionDescription("Test"),
                TransactionType.Withdrawal,
                new UtcNowDateTimeProvider());

            transaction.MarkAsSucceeded();

            moneyAccountDecorator.CreateTransaction(transaction);

            var money = new Money(500);

            // Act Assert
            var action = new Action(() => moneyAccountDecorator.Withdrawal(money));
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Promotion_WhenEmptyTransaction_ShouldHaveTransactionCountOfOne()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);
            var moneyAccountDecorator = new MoneyAccountDecorator(account);

            // Act
            moneyAccountDecorator.Promotion(new Money(500));

            // Assert
            moneyAccountDecorator.Transactions.Count.Should().Be(1);
        }
    }
}
