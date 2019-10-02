// Filename: BalanceTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.TestHelpers;
using eDoxa.Cashier.TestHelpers.Fixtures;

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
            yield return new MoneyDepositTransaction(Money.Ten);
            yield return MarkAsSucceded(new MoneyDepositTransaction(Money.Fifty));
            yield return MarkAsFailed(new MoneyDepositTransaction(Money.FiveHundred));
            yield return MarkAsSucceded(new MoneyChargeTransaction(Money.Fifty));
            yield return MarkAsSucceded(new MoneyPayoutTransaction(Money.OneHundred));
            yield return MarkAsSucceded(new MoneyWithdrawTransaction(Money.Fifty));
            yield return MarkAsFailed(new TokenChargeTransaction(Token.FiveHundredThousand));
            yield return MarkAsSucceded(new TokenDepositTransaction(Token.OneHundredThousand));
            yield return MarkAsSucceded(new TokenPayoutTransaction(Token.TwoHundredFiftyThousand));
            yield return MarkAsSucceded(new TokenChargeTransaction(Token.OneHundredThousand));
            yield return MarkAsSucceded(new TokenChargeTransaction(Token.TwoHundredFiftyThousand));
            yield return MarkAsSucceded(new TokenRewardTransaction(Token.FiftyThousand));
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
