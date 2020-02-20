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
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate;
using eDoxa.Cashier.Domain.DomainEvents;
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

            var metaData = new Dictionary<string, string>
            {
                { "test", "value" }
            };

            // Act
            var transaction = new TransactionBuilder(TransactionType.Deposit, new Money(50)).WithMetadata(new TransactionMetadata(metaData)).Build();
            decorator.CreateTransaction(transaction);

            // Assert
            var result = decorator.FindTransaction(transaction.Metadata);
            result.Should().NotBeNull();
            result.Should().BeSameAs(transaction);
        }

        [Fact]
        public void FindTransaction_WhenEmpty_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);
            var decorator = new MoneyAccountDecorator(account);

            // Act Assert
            var action =  new Action(() => decorator.FindTransaction(new TransactionId()));
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void GetBalanceFor_Money_ShouldBeNull()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);
            var decorator = new MoneyAccountDecorator(account);

            var transactionList = new List<Transaction>
            {
                new Transaction(new Money(50), new TransactionDescription("deposit"), TransactionType.Deposit, new UtcNowDateTimeProvider()),
                new Transaction(new Money(20), new TransactionDescription("withdrawal"), TransactionType.Withdrawal, new UtcNowDateTimeProvider())
            };

            // Act
            foreach (var transaction in transactionList)
            {
                transaction.MarkAsSucceeded();
                decorator.CreateTransaction(transaction);
            }

            // Assert
            var result = decorator.GetBalanceFor(CurrencyType.Money);
            result.ToString().Should().Be("Money");
            // Francis: Missing assert
        }

        [Fact]
        public void AddDomainEvent_WhenEmpty_ShouldAddDomainEvent()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);
            var decorator = new MoneyAccountDecorator(account);

            // Act
            decorator.AddDomainEvent(new ChallengeClosedDomainEvent(new ChallengeId(), new ChallengeParticipantPayouts()));

            // Assert
            decorator.DomainEvents.Count.Should().Be(1);
        }

        [Fact]
        public void ClearDomainEvents_WhenContainingEvents_ShouldBeEmpty()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);
            var decorator = new MoneyAccountDecorator(account);

            // Act
            decorator.AddDomainEvent(new ChallengeClosedDomainEvent(new ChallengeId(), new ChallengeParticipantPayouts()));
            decorator.AddDomainEvent(new ChallengeParticipantPayoutDomainEvent(new UserId(), new Token(20)));
            decorator.AddDomainEvent(new PromotionRedeemedDomainEvent(new UserId(), new PromotionId(), CurrencyType.Money, 50));

            decorator.ClearDomainEvents();

            // Assert
            decorator.DomainEvents.Count.Should().Be(0);
        }

        [Fact]
        public void SetEntityId_ShouldChangeAccountId()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(new UserId());
            var decorator = new MoneyAccountDecorator(account);

            // Act
            decorator.SetEntityId(userId);

            // Assert
            decorator.Id.Should().Be(userId);
        }
    }
}
