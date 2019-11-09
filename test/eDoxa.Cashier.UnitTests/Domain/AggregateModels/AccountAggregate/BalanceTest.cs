// Filename: BalanceTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

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
            yield return new MoneyDepositTransaction(new TransactionId(), Money.Ten);
            yield return MarkAsSucceded(new MoneyDepositTransaction(new TransactionId(), Money.Fifty));
            yield return MarkAsFailed(new MoneyDepositTransaction(new TransactionId(), Money.FiveHundred));
            yield return MarkAsSucceded(new MoneyChargeTransaction(new TransactionId(), Money.Fifty));
            yield return MarkAsSucceded(new MoneyPayoutTransaction(new TransactionId(), Money.OneHundred));
            yield return MarkAsSucceded(new MoneyWithdrawTransaction(new TransactionId(), Money.Fifty));
            yield return MarkAsFailed(new TokenChargeTransaction(new TransactionId(), Token.FiveHundredThousand));
            yield return MarkAsSucceded(new TokenDepositTransaction(new TransactionId(), Token.OneHundredThousand));
            yield return MarkAsSucceded(new TokenPayoutTransaction(new TransactionId(), Token.TwoHundredFiftyThousand));
            yield return MarkAsSucceded(new TokenChargeTransaction(new TransactionId(), Token.OneHundredThousand));
            yield return MarkAsSucceded(new TokenChargeTransaction(new TransactionId(), Token.TwoHundredFiftyThousand));
            yield return MarkAsSucceded(new TokenRewardTransaction(new TransactionId(), Token.FiftyThousand));
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
