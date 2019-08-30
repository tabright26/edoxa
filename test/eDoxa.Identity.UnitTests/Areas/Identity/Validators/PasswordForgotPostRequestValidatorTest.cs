// Filename: AddressPostRequestValidatorTest.cs// Date Created: 2019-08-23
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Identity.Api.Areas.Identity.ErrorDescribers;
using eDoxa.Identity.Api.Areas.Identity.Validators;

using FluentAssertions;

using FluentValidation.TestHelper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Validators
{
    [TestClass]
    public sealed class PasswordForgotPostRequestValidatorTest
    {
        [DataTestMethod]
        [DynamicData(nameof(ValidEmails), DynamicDataSourceType.Method)]
        public void Validate_WhenEmailIsValid_ShouldNotHaveValidationErrorFor(string email)
        {
            // Arrange
            var validator = new PasswordForgotPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Email, email);
        }

        private static IEnumerable<object[]> ValidEmails()
        {
            yield return new object[] { "gabriel@edoxa.gg" };
            yield return new object[] { "francis.love.skyrim123@edoxa.gg" };
        }

        [DataTestMethod]
        [DynamicData(nameof(InvalidEmails), DynamicDataSourceType.Method)]
        public void Validate_WhenEmailIsInvalid_ShouldNotHaveValidationErrorFor(string email, string errorMessage)
        {
            // Arrange
            var validator = new PasswordForgotPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Email, email);

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        private static IEnumerable<object[]> InvalidEmails()
        {
            yield return new object[] { null, PasswordForgotErrorDescriber.EmailRequired() };
            yield return new object[] { "", PasswordForgotErrorDescriber.EmailRequired() };
            yield return new object[] { "gabrieledoxa.gg", PasswordForgotErrorDescriber.EmailInvalid() };
            yield return new object[] { "!gab_riel/$@edoxa.gg", PasswordForgotErrorDescriber.EmailInvalid() };
        }
    }
}
