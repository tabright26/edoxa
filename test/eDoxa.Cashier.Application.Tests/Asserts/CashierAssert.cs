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
        public static void IsMapped([CanBeNull] AccountDTO account)
        {
            account.Should().NotBeNull();

            IsMapped(account?.Funds);

            IsMapped(account?.Tokens);
        }

        private static void IsMapped([CanBeNull] CurrencyDTO currency)
        {
            currency.Should().NotBeNull();

            currency?.Balance.Should().BeGreaterOrEqualTo(decimal.Zero);

            currency?.Pending.Should().BeGreaterOrEqualTo(decimal.Zero);
        }
    }
}