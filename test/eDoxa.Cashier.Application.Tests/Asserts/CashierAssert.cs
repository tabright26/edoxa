// Filename: CashierAssert.cs
// Date Created: 2019-04-14
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

namespace eDoxa.Cashier.Application.Tests.Asserts
{
    internal static class CashierAssert
    {
        public static void IsMapped([CanBeNull] MoneyAccountDTO account)
        {
            account.Should().NotBeNull();

            account?.Balance.Should().BeGreaterOrEqualTo(decimal.Zero);

            account?.Pending.Should().BeGreaterOrEqualTo(decimal.Zero);
        }
    }
}