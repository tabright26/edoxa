// Filename: FluentAssertionsExtensions.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.ViewModels;
using eDoxa.Seedwork.Domain.Extensions;

using FluentAssertions;

namespace eDoxa.Cashier.UnitTests.Helpers.Extensions
{
    public static class FluentAssertionsExtensions
    {
        public static void AssertStateIsValid(this Balance balance)
        {
            balance.Should().NotBeNull();

            balance.Available.As<decimal>().Should().BeGreaterOrEqualTo(decimal.Zero);

            balance.Pending.As<decimal>().Should().BeGreaterOrEqualTo(decimal.Zero);
        }

        public static void AssertStateIsValid(this IEnumerable<TransactionViewModel> transactions)
        {
            transactions.ForEach(AssertStateIsValid);
        }

        public static void AssertStateIsValid(this TransactionViewModel transaction)
        {
            transaction.Should().NotBeNull();

            transaction.Id.Should().NotBeEmpty();

            transaction.Amount.Should().BeGreaterOrEqualTo(decimal.Zero);

            transaction.CurrencyType.Should().NotBeNull();

            transaction.Type.Should().NotBeNull();

            transaction.Description.Should().NotBeNullOrEmpty();
        }

        public static void AssertStateIsValid(this BalanceViewModel balance)
        {
            balance.Should().NotBeNull();

            balance.CurrencyType.Should().NotBeNull();

            balance.Available.Should().BeGreaterOrEqualTo(decimal.Zero);

            balance.Pending.Should().BeGreaterOrEqualTo(decimal.Zero);
        }
    }
}
