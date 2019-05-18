// Filename: CashierQueryAssert.cs
// Date Created: 2019-05-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain;
using eDoxa.Cashier.DTO;

using FluentAssertions;

namespace eDoxa.Cashier.Tests.Asserts
{
    public static class CashierQueryAssert
    {
        public static void IsMapped(AccountDTO account)
        {
            account.Should().NotBeNull();

            account.Currency.Should().BeInRange(AccountCurrency.Money, AccountCurrency.Token);

            account.Balance.Should().BeGreaterOrEqualTo(decimal.Zero);

            account.Pending.Should().BeGreaterOrEqualTo(decimal.Zero);
        }

        public static void IsMapped(TransactionListDTO transactions)
        {
            transactions.Should().NotBeNull();

            foreach (var transaction in transactions)
            {
                IsMapped(transaction);
            }
        }

        public static void IsMapped(TransactionDTO transaction)
        {
            transaction.Should().NotBeNull();

            transaction.Id.Should().NotBeEmpty();

            transaction.Amount.Should().BeGreaterOrEqualTo(decimal.Zero);

            transaction.Type.Should().BeInRange(TransactionType.Deposit, TransactionType.Service);

            transaction.Currency.Should().BeInRange(AccountCurrency.Money, AccountCurrency.Token);

            transaction.Description.Should().NotBeNullOrEmpty();
        }
    }
}