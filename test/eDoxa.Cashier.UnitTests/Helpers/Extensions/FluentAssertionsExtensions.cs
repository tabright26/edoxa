﻿// Filename: FluentAssertionsExtensions.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
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

            transaction.Currency.Should().NotBeNull();

            transaction.Type.Should().NotBeNull();

            transaction.Description.Should().NotBeNullOrEmpty();
        }

        public static void AssertStateIsValid(this BalanceViewModel balance)
        {
            balance.Should().NotBeNull();

            balance.Currency.Should().NotBeNull();

            balance.Available.Should().BeGreaterOrEqualTo(decimal.Zero);

            balance.Pending.Should().BeGreaterOrEqualTo(decimal.Zero);
        }
    }
}
