// Filename: CashierAssert.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
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

            IsMapped(user.Funds);
        }

        private static void IsMapped(MoneyAccount account)
        {
            account.Should().NotBeNull();

            account.Id.Should().NotBeNull();

            account.Balance.As<decimal>().Should().BeGreaterOrEqualTo(decimal.Zero);

            account.Pending.As<decimal>().Should().BeGreaterOrEqualTo(decimal.Zero);
        }
    }
}