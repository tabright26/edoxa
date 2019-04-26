// Filename: CashierAssert.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Cashier.Domain;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;

using FluentAssertions;

namespace eDoxa.Cashier.Infrastructure.Tests.Asserts
{
    internal static class CashierAssert
    {
        public static void IsMapped(User user)
        {
            user.Should().NotBeNull();

            user.Id.Should().NotBeNull();

            IsMapped(user.MoneyAccount);

            IsMapped(user.TokenAccount);
        }

        private static void IsMapped(IMoneyAccount account)
        {
            account.Should().NotBeNull();

            account.Id.Should().NotBeNull();

            account.Balance.As<decimal>().Should().BeGreaterOrEqualTo(decimal.Zero);

            account.Pending.As<decimal>().Should().BeGreaterOrEqualTo(decimal.Zero);

            IsMapped(account.Transactions);
        }

        private static void IsMapped(IReadOnlyCollection<IMoneyTransaction> transactions)
        {
            transactions.Should().NotBeNull();

            foreach (var transaction in transactions)
            {
                IsMapped(transaction);
            }
        }

        private static void IsMapped(IMoneyTransaction transaction)
        {
            transaction.Should().NotBeNull();

            transaction.Id.Should().NotBeNull();

            transaction.Timestamp.Should().BeBefore(DateTime.UtcNow);

            transaction.Amount.Should().NotBeNull();
        }

        private static void IsMapped(ITokenAccount account)
        {
            account.Should().NotBeNull();

            account.Id.Should().NotBeNull();

            account.Balance.As<long>().Should().BeGreaterOrEqualTo(0);

            account.Pending.As<long>().Should().BeGreaterOrEqualTo(0);

            IsMapped(account.Transactions);
        }

        private static void IsMapped(IReadOnlyCollection<ITokenTransaction> transactions)
        {
            transactions.Should().NotBeNull();

            foreach (var transaction in transactions)
            {
                IsMapped(transaction);
            }
        }

        private static void IsMapped(ITokenTransaction transaction)
        {
            transaction.Should().NotBeNull();

            transaction.Id.Should().NotBeNull();

            transaction.Timestamp.Should().BeBefore(DateTime.UtcNow);

            transaction.Amount.Should().NotBeNull();
        }
    }
}