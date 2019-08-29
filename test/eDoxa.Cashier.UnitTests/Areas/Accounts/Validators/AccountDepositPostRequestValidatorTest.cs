// Filename: AccountDepositPostRequestValidatorTest.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Cashier.Api.Areas.Accounts.Validators;
using eDoxa.Cashier.Domain.AggregateModels;

using FluentValidation.TestHelper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Areas.Accounts.Validators
{
    [TestClass]
    public sealed class AccountDepositPostRequestValidatorTest
    {
        [DataTestMethod]
        [DynamicData(nameof(ValidCurrencies), DynamicDataSourceType.Method)]
        public void Validate_WhenCurrencyIsValid_ShouldNotHaveValidationErrorFor(Currency currency)
        {
            // Arrange
            var validator = new AccountDepositPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Currency, currency);
        }

        private static IEnumerable<object[]> ValidCurrencies()
        {
            yield return new object[] {Currency.Money};
            yield return new object[] {Currency.Token};
        }

        [DataTestMethod]
        [DynamicData(nameof(InvalidCurrencies), DynamicDataSourceType.Method)]
        public void Validate_WhenCurrencyIsInvalid_ShouldHaveValidationErrorFor(Currency currency)
        {
            // Arrange
            var validator = new AccountDepositPostRequestValidator();

            // Act - Assert
            validator.ShouldHaveValidationErrorFor(request => request.Currency, currency);
        }

        private static IEnumerable<object[]> InvalidCurrencies()
        {
            yield return new object[] {null};
            yield return new object[] {Currency.All};
            yield return new object[] {new Currency()};
        }
    }
}
