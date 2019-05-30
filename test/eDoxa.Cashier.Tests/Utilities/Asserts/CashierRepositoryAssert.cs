// Filename: CashierRepositoryAssert.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;

using FluentAssertions;

namespace eDoxa.Cashier.Tests.Utilities.Asserts
{
    internal static class CashierRepositoryAssert
    {
        public static void IsMapped(Balance balance)
        {
            balance.Should().NotBeNull();

            balance.Available.As<decimal>().Should().BeGreaterOrEqualTo(decimal.Zero);

            balance.Pending.As<decimal>().Should().BeGreaterOrEqualTo(decimal.Zero);
        }
    }
}
