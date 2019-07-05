// Filename: AccountTest.cs
// Date Created: 2019-07-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Common.ValueObjects;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels.AccountAggregate
{
    [TestClass]
    public sealed class AccountTest
    {
        public static IEnumerable<object[]> ValidCurrencyDataSets => Currency.GetEnumerations().Select(currency => new object[] {currency});

        public static IEnumerable<object[]> InvalidCurrencyDataSets => new[] {new object[] {new Currency()}, new object[] { Currency.All}};

        [DataTestMethod]
        [DynamicData(nameof(ValidCurrencyDataSets))]
        public void GetBalanceFor_ValidCurrency_ShouldThrowArgumentException(Currency currency)
        {
            var account = new Account(new UserId());

            var balance = account.GetBalanceFor(currency);

            balance.Currency.Should().Be(currency);
        }

        [DataTestMethod]
        [DynamicData(nameof(InvalidCurrencyDataSets))]
        public void GetBalanceFor_InvalidCurrency_ShouldThrowArgumentException(Currency currency)
        {
            var account = new Account(new UserId());

            var action = new Action(() => account.GetBalanceFor(currency));

            action.Should().Throw<ArgumentException>();
        }
    }
}
