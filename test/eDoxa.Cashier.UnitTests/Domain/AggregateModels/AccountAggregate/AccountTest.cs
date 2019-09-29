// Filename: AccountTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.TestHelpers;
using eDoxa.Cashier.TestHelpers.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels.AccountAggregate
{
    public sealed class AccountTest : UnitTest
    {
        public AccountTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        public static IEnumerable<object[]> ValidCurrencyDataSets => Currency.GetEnumerations().Select(currency => new object[] {currency});

        public static IEnumerable<object[]> InvalidCurrencyDataSets => new[] {new object[] {new Currency()}, new object[] {Currency.All}};

        [Theory]
        [MemberData(nameof(ValidCurrencyDataSets))]
        public void GetBalanceFor_WithValidCurrency_ShouldBeCurrency(Currency currency)
        {
            var account = new Account(new UserId());

            var balance = account.GetBalanceFor(currency);

            balance.Currency.Should().Be(currency);
        }

        [Theory]
        [MemberData(nameof(InvalidCurrencyDataSets))]
        public void GetBalanceFor_WithInvalidCurrency_ShouldThrowArgumentException(Currency currency)
        {
            var account = new Account(new UserId());

            var action = new Action(() => account.GetBalanceFor(currency));

            action.Should().Throw<ArgumentException>();
        }
    }
}
