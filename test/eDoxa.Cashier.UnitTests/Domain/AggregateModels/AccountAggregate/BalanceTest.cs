﻿// Filename: BalanceTest.cs
// Date Created: 2019-11-25
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels.AccountAggregate
{
    public sealed class BalanceTest : UnitTest
    {
        public BalanceTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(testData, testMapper, testValidator)
        {
        }

        private static IEnumerable<ITransaction> CreateTransactions()
        {
            yield return new TransactionBuilder(TransactionType.Deposit, Money.Ten).Build();
            yield return MarkAsSucceeded(new TransactionBuilder(TransactionType.Deposit, Money.Fifty).Build());
            yield return MarkAsFailed(new TransactionBuilder(TransactionType.Deposit, Money.FiveHundred).Build());
            yield return MarkAsSucceeded(new TransactionBuilder(TransactionType.Charge, Money.Fifty).Build());
            yield return MarkAsSucceeded(new TransactionBuilder(TransactionType.Payout, Money.OneHundred).Build());
            yield return MarkAsSucceeded(new TransactionBuilder(TransactionType.Withdraw, Money.Fifty).Build());
            yield return MarkAsFailed(new TransactionBuilder(TransactionType.Charge, Token.FiveThousand).Build());
            yield return MarkAsSucceeded(new TransactionBuilder(TransactionType.Deposit, Token.OneThousand).Build());
            yield return MarkAsSucceeded(new TransactionBuilder(TransactionType.Payout, Token.TwoThousandFiveHundred).Build());
            yield return MarkAsSucceeded(new TransactionBuilder(TransactionType.Charge, Token.OneThousand).Build());
            yield return MarkAsSucceeded(new TransactionBuilder(TransactionType.Charge, Token.TwoThousandFiveHundred).Build());
            yield return MarkAsSucceeded(new TransactionBuilder(TransactionType.Reward, Token.FiveHundred).Build());
        }

        private static ITransaction MarkAsSucceeded(ITransaction transaction)
        {
            transaction.MarkAsSucceeded();

            return transaction;
        }

        private static ITransaction MarkAsFailed(ITransaction transaction)
        {
            transaction.MarkAsFailed();

            return transaction;
        }

        [Fact]
        public void Balance_WithTransactions_ShouldBeFifty()
        {
            // Arrange
            var transactions = CreateTransactions().ToList();

            // Act
            var balance = new Balance(transactions, CurrencyType.Money);

            // Assert
            balance.Available.Should().Be(Money.Fifty);
            balance.CurrencyType.Should().Be(CurrencyType.Money);
            balance.Pending.Should().Be(Money.Ten);
        }

        [Fact]
        public void Balance_WithTransactions_ShouldBeFiftyThousands()
        {
            // Arrange
            var transactions = CreateTransactions().ToList();

            // Act
            var balance = new Balance(transactions, CurrencyType.Token);

            // Assert
            balance.Available.Should().Be(Token.FiveHundred);
            balance.CurrencyType.Should().Be(CurrencyType.Token);
            balance.Pending.Should().Be(decimal.Zero);
        }

        [Fact]
        public void GetHashCode_ShouldNotBeZero()
        {
            // Arrange
            var transactions = CreateTransactions().ToList();
            var balance = new Balance(transactions, CurrencyType.Token);

            // Act Assert
            balance.GetHashCode().Should().NotBe(0);
        }
    }
}
