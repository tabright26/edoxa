// Filename: CashierAssert.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

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

        public static void IsMapped(Account account)
        {
            account.Should().NotBeNull();

            account.Id.Should().NotBeNull();

            IsMapped(account.Funds);

            IsMapped(account.Tokens);
        }

        public static void IsMapped<TCurrency>(Account<TCurrency> account)
        where TCurrency : Currency<TCurrency>, new()
        {
            account.Should().NotBeNull();

            IsMapped(account.Balance);

            IsMapped(account.Pending);
        }

        public static void IsMapped<TCurrency>(Currency<TCurrency> currency)
        where TCurrency : Currency<TCurrency>, new()
        {
            currency.Should().NotBeNull();

            currency.ToDecimal().Should().BeGreaterOrEqualTo(decimal.Zero);
        }
    }
}