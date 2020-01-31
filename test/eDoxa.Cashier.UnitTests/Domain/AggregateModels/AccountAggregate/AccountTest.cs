// Filename: AccountTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels.AccountAggregate
{
    public sealed class AccountTest : UnitTest
    {
        public AccountTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(testData, testMapper, testValidator)
        {
        }

        public static TheoryData<CurrencyType> ValidCurrencyDataSets =>
            new TheoryData<CurrencyType>
            {
                CurrencyType.Money,
                CurrencyType.Token
            };

        public static TheoryData<CurrencyType> InvalidCurrencyDataSets =>
            new TheoryData<CurrencyType>
            {
                new CurrencyType(),
                CurrencyType.All
            };

        [Theory]
        [MemberData(nameof(ValidCurrencyDataSets))]
        public void GetBalanceFor_WithValidCurrency_ShouldBeCurrency(CurrencyType currencyType)
        {
            var account = new Account(new UserId());

            var balance = account.GetBalanceFor(currencyType);

            balance.CurrencyType.Should().Be(currencyType);
        }

        [Theory]
        [MemberData(nameof(InvalidCurrencyDataSets))]
        public void GetBalanceFor_WithInvalidCurrency_ShouldThrowArgumentException(CurrencyType currencyType)
        {
            var account = new Account(new UserId());

            var action = new Action(() => account.GetBalanceFor(currencyType));

            action.Should().Throw<ArgumentException>();
        }
    }
}
