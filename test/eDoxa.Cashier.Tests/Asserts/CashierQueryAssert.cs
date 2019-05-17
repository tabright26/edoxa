// Filename: CashierQueryAssert.cs
// Date Created: 2019-05-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.DTO;

using FluentAssertions;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Tests.Asserts
{
    internal static class CashierQueryAssert
    {
        public static void IsMapped([CanBeNull] AccountDTO account)
        {
            account.Should().NotBeNull();

            account?.Balance.Should().BeGreaterOrEqualTo(decimal.Zero);

            account?.Pending.Should().BeGreaterOrEqualTo(decimal.Zero);
        }
    }
}