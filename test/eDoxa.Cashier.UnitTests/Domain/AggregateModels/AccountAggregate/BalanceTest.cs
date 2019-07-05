// Filename: BalanceTest.cs
// Date Created: 2019-07-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Seedwork.Common.Enumerations;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels.AccountAggregate
{
    [TestClass]
    public sealed class BalanceTest
    {
        [TestMethod]
        public void Available_CurrencyMoney_ShouldBeMoneyFifty()
        {
            // Arrange
            var transactions = CreateTransactions().ToList();

            // Act
            var balance = new Balance(transactions, CurrencyType.Money);

            // Assert
            balance.Currency.Should().Be(CurrencyType.Money);
            balance.Available.Should().Be(Money.Fifty);
            balance.Pending.Should().Be(Money.Ten);
        }

        [TestMethod]
        public void Available_CurrencyToken_ShouldBeTokenFiftyThousand()
        {
            // Arrange
            var transactions = CreateTransactions().ToList();

            // Act
            var balance = new Balance(transactions, CurrencyType.Token);

            // Assert
            balance.Currency.Should().Be(CurrencyType.Token);
            balance.Available.Should().Be(Token.FiftyThousand);
            balance.Pending.Should().Be(decimal.Zero);
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
    }
}
