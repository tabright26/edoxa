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
    public sealed class TokenAccountDecoratorTest : UnitTest
    {
        public TokenAccountDecoratorTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
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
            var tokenAccountDecorator = new TokenAccountDecorator(account);

            var transaction = new Transaction(
                new Token(100000),
                new TransactionDescription("Test"),
                TransactionType.Deposit,
                new UtcNowDateTimeProvider());

            transaction.MarkAsSucceeded();

            tokenAccountDecorator.CreateTransaction(transaction);

            var token = new Token(50000);

            // Act Assert
            var action = new Action(() => tokenAccountDecorator.Deposit(token));
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Charge_WhenBalanceIsZero_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);
            var tokenAccountDecorator = new TokenAccountDecorator(account);

            // Act Assert
            var action = new Action(() => tokenAccountDecorator.Charge(new Token(100000)));
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Promotion_WhenEmptyTransaction_ShouldHaveTransactionCountOfOne()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);
            var tokenAccountDecorator = new TokenAccountDecorator(account);

            // Act
            tokenAccountDecorator.Promotion(new Token(100000));

            // Assert
            tokenAccountDecorator.Transactions.Count.Should().Be(1);
        }
    }
}
