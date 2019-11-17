// Filename: AccountTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels.AccountAggregate
{
    public sealed class AccountTest : UnitTest
    {
        public AccountTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        public static TheoryData<Currency> ValidCurrencyDataSets =>
            new TheoryData<Currency>
            {
                Currency.Money,
                Currency.Token
            };

        public static TheoryData<Currency> InvalidCurrencyDataSets =>
            new TheoryData<Currency>
            {
                new Currency(),
                Currency.All
            };

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
