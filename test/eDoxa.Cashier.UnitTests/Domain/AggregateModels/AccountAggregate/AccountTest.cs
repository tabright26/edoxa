// Filename: AccountTest.cs
// Date Created: 2019-11-25
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

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
    public sealed class AccountTest : UnitTest
    {
        public AccountTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(testData, testMapper, testValidator)
        {
        }

        public static TheoryData<CurrencyType> ValidCurrencyDataSets =>
            new TheoryData<CurrencyType>
            {
                CurrencyType.Money,
                CurrencyType.Token
            };

        public static TheoryData<CurrencyType> InvalidCurrencyDataSets =>
            new TheoryData<CurrencyType>
            {
                new CurrencyType(),
                CurrencyType.All
            };

        [Theory]
        [MemberData(nameof(ValidCurrencyDataSets))]
        public void GetBalanceFor_WithValidCurrency_ShouldBeCurrency(CurrencyType currencyType)
        {
            var account = new Account(new UserId());

            var balance = account.GetBalanceFor(currencyType);

            balance.CurrencyType.Should().Be(currencyType);
        }

        [Theory]
        [MemberData(nameof(InvalidCurrencyDataSets))]
        public void GetBalanceFor_WithInvalidCurrency_ShouldThrowArgumentException(CurrencyType currencyType)
        {
            var account = new Account(new UserId());

            var action = new Action(() => account.GetBalanceFor(currencyType));

            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void FindTransaction_WithAdministratorAccount_ShouldBeTrue()
        {
            // Arrange
            var userId = new UserId();
            var admin = Account.CreateTestAdministrator(userId);
            var transaction = new TransactionBuilder(TransactionType.Deposit, new Money(50)).Build();

            // Act
            admin.CreateTransaction(transaction);

            // Assert
            admin.FindTransaction(transaction.Id).Should().NotBeNull();
            admin.FindTransaction(transaction.Id).Should().Be(transaction);
        }

        [Fact]
        public void FindTransaction_WithAdministratorAccount_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var userId = new UserId();
            var admin = Account.CreateTestAdministrator(userId);

            // Act Assert
            var action = new Action(() => admin.FindTransaction(new TransactionId()));
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Equals_WithSameId_ShouldBeTrue()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);

            var diffAccount = new Account(userId);
            var transaction = new TransactionBuilder(TransactionType.Deposit, new Money(50)).Build();
            diffAccount.CreateTransaction(transaction);

            // Act Assert
            account.Equals(diffAccount).Should().BeTrue();
        }

        [Fact]
        public void Equals_WithEmptyObject_ShouldBeFalse()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);

            var diffAccount =  new {
                userId
            };

            // Act Assert
            account.Equals(diffAccount).Should().BeFalse();
        }

        // Francis: Je sais vraiment pas si c"est nécessaire, mais je me rappel avoir vu dequoi par rapport au zéros dans les tests.
        [Fact]
        public void GetHashCode_ShouldNotBeZero()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId);

            // Act Assert
            account.GetHashCode().Should().NotBe(0);
        }
    }
}
