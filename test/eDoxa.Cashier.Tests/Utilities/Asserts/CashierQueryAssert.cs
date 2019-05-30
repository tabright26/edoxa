// Filename: CashierQueryAssert.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.DTO;

using FluentAssertions;

namespace eDoxa.Cashier.Tests.Utilities.Asserts
{
    public static class CashierQueryAssert
    {
        public static void IsMapped(BalanceDTO balance)
        {
            balance.Should().NotBeNull();

            balance.CurrencyType.Should().NotBeNull();

            balance.Available.Should().BeGreaterOrEqualTo(decimal.Zero);

            balance.Pending.Should().BeGreaterOrEqualTo(decimal.Zero);
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

            transaction.CurrencyType.Should().NotBeNull();
            
            transaction.Type.Should().NotBeNull();
            
            transaction.Description.Should().NotBeNullOrEmpty();
        }
    }
}
