// Filename: BalanceTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
        public BalanceTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        private static IEnumerable<ITransaction> CreateTransactions()
        {
            yield return new TransactionBuilder(TransactionType.Deposit, Money.Ten).Build();
            yield return MarkAsSucceded(new TransactionBuilder(TransactionType.Deposit, Money.Fifty).Build());
            yield return MarkAsFailed(new TransactionBuilder(TransactionType.Deposit, Money.FiveHundred).Build());
            yield return MarkAsSucceded(new TransactionBuilder(TransactionType.Charge, Money.Fifty).Build());
            yield return MarkAsSucceded(new TransactionBuilder(TransactionType.Payout, Money.OneHundred).Build());
            yield return MarkAsSucceded(new TransactionBuilder(TransactionType.Withdrawal, Money.Fifty).Build());
            yield return MarkAsFailed(new TransactionBuilder(TransactionType.Charge, Token.FiveHundredThousand).Build());
            yield return MarkAsSucceded(new TransactionBuilder(TransactionType.Deposit, Token.OneHundredThousand).Build());
            yield return MarkAsSucceded(new TransactionBuilder(TransactionType.Payout, Token.TwoHundredFiftyThousand).Build());
            yield return MarkAsSucceded(new TransactionBuilder(TransactionType.Charge, Token.OneHundredThousand).Build());
            yield return MarkAsSucceded(new TransactionBuilder(TransactionType.Charge, Token.TwoHundredFiftyThousand).Build());
            yield return MarkAsSucceded(new TransactionBuilder(TransactionType.Reward, Token.FiftyThousand).Build());
        }

        private static ITransaction MarkAsSucceded(ITransaction transaction)
        {
            transaction.MarkAsSucceded();

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
            var balance = new Balance(transactions, Currency.Money);

            // Assert
            balance.Available.Should().Be(Money.Fifty);
            balance.Currency.Should().Be(Currency.Money);
            balance.Pending.Should().Be(Money.Ten);
        }

        [Fact]
        public void Balance_WithTransactions_ShouldBeFiftyThousands()
        {
            // Arrange
            var transactions = CreateTransactions().ToList();

            // Act
            var balance = new Balance(transactions, Currency.Token);

            // Assert
            balance.Available.Should().Be(Token.FiftyThousand);
            balance.Currency.Should().Be(Currency.Token);
            balance.Pending.Should().Be(decimal.Zero);
        }
    }
}
