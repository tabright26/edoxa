// Filename: CashierAssert.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

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

            IsMapped(user.Account);
        }

        private static void IsMapped(Account account)
        {
            account.Should().NotBeNull();

            account.Id.Should().NotBeNull();

            IsMapped(account.Funds);

            IsMapped(account.Tokens);
        }

        private static void IsMapped<TCurrency>(IAccount<TCurrency> account)
        where TCurrency : Currency<TCurrency>
        {
            account.Should().NotBeNull();

            IsMapped(account.Balance);

            IsMapped(account.Pending);
        }

        private static void IsMapped<TCurrency>(TCurrency currency)
        where TCurrency : Currency<TCurrency>
        {
            currency.Should().NotBeNull();

            currency.As<decimal>().Should().BeGreaterOrEqualTo(decimal.Zero);
        }
    }
}