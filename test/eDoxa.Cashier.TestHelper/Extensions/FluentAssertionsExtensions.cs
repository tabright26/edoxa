// Filename: FluentAssertionsExtensions.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Responses;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.CustomTypes;

using FluentAssertions;

namespace eDoxa.Cashier.TestHelper.Extensions
{
    public static class FluentAssertionsExtensions
    {
        public static void AssertStateIsValid(this Balance balance)
        {
            balance.Should().NotBeNull();

            balance.Available.As<decimal>().Should().BeGreaterOrEqualTo(decimal.Zero);

            balance.Pending.As<decimal>().Should().BeGreaterOrEqualTo(decimal.Zero);
        }

        public static void AssertStateIsValid(this IEnumerable<TransactionResponse> transactions)
        {
            foreach (var transaction in transactions)
            {
                transaction.AssertStateIsValid();
            }
        }

        public static void AssertStateIsValid(this TransactionResponse transaction)
        {
            transaction.Should().NotBeNull();

            transaction.Id.Should().NotBeEmpty();

            transaction.Amount.Should().BeGreaterOrEqualTo(decimal.Zero);

            transaction.Currency.Should().NotBeNull();

            transaction.Type.Should().NotBeNull();

            transaction.Description.Should().NotBeNullOrEmpty();
        }

        public static void AssertStateIsValid(this BalanceDto balance)
        {
            balance.Should().NotBeNull();

            balance.Currency.Should().NotBeNull();

            (balance.Available >= DecimalValue.FromDecimal(decimal.Zero)).Should().BeTrue();

            (balance.Pending >= DecimalValue.FromDecimal(decimal.Zero)).Should().BeTrue();
        }
    }
}
