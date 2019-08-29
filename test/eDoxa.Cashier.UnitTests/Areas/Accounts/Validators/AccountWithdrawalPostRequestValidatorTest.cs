// Filename: AccountWithdrawalPostRequestValidatorTest.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Cashier.Api.Areas.Accounts.Validators;
using eDoxa.Cashier.Domain.AggregateModels;

using FluentValidation.TestHelper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Areas.Accounts.Validators
{
    [TestClass]
    public sealed class AccountWithdrawalPostRequestValidatorTest
    {
        [DataTestMethod]
        [DynamicData(nameof(ValidWithdrawalAmounts), DynamicDataSourceType.Method)]
        public void Validate_WhenAmountIsValid_ShouldNotHaveValidationErrorFor(Money money)
        {
            // Arrange
            var validator = new AccountWithdrawalPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Amount, money.Amount);
        }

        private static IEnumerable<object[]> ValidWithdrawalAmounts()
        {
            return Money.WithdrawalAmounts().Select(money => new object[] {money});
        }

        [DataTestMethod]
        [DynamicData(nameof(InvalidWithdrawalAmounts), DynamicDataSourceType.Method)]
        public void Validate_WhenAmountIsInvalid_ShouldHaveValidationErrorFor(Money money)
        {
            // Arrange
            var validator = new AccountWithdrawalPostRequestValidator();

            // Act - Assert
            validator.ShouldHaveValidationErrorFor(request => request.Amount, money.Amount);
        }

        private static IEnumerable<object[]> InvalidWithdrawalAmounts()
        {
            yield return new object[] {Money.Five};
            yield return new object[] {Money.Ten};
            yield return new object[] {Money.Twenty};
            yield return new object[] {Money.TwentyFive};
            yield return new object[] {Money.FiveHundred};
        }
    }
}
