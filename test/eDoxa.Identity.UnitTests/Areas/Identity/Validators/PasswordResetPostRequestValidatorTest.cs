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
    public sealed class PasswordResetPostRequestValidatorTest
    {
        [DataTestMethod]
        [DynamicData(nameof(ValidEmails), DynamicDataSourceType.Method)]
        public void Validate_WhenEmailIsValid_ShouldNotHaveValidationErrorFor(string email)
        {
            // Arrange
            var validator = new PasswordResetPostRequestValidator();

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
            var validator = new PasswordResetPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Email, email);

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        private static IEnumerable<object[]> InvalidEmails()
        {
            yield return new object[] { null, PasswordResetErrorDescriber.EmailRequired() };
            yield return new object[] { "", PasswordResetErrorDescriber.EmailRequired() };
            yield return new object[] { "gabrieledoxa.gg", PasswordResetErrorDescriber.EmailInvalid() };
            yield return new object[] { "!gab_riel/$@edoxa.gg", PasswordResetErrorDescriber.EmailInvalid() };
        }

        [DataTestMethod]
        [DynamicData(nameof(ValidEmails), DynamicDataSourceType.Method)]
        public void Validate_WhenPasswordIsValid_ShouldNotHaveValidationErrorFor(string email)
        {
            // Arrange
            var validator = new PasswordResetPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Email, email);
        }

        private static IEnumerable<object[]> ValidPasswords()
        {
            yield return new object[] { "Testing123!" };
            yield return new object[] { "th1s_Is-a-p4ssw3rd" };
        }

        [DataTestMethod]
        [DynamicData(nameof(InvalidEmails), DynamicDataSourceType.Method)]
        public void Validate_WhenPasswordIsInvalid_ShouldNotHaveValidationErrorFor(string email, string errorMessage)
        {
            // Arrange
            var validator = new PasswordResetPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Email, email);

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        private static IEnumerable<object[]> InvalidPasswords()
        {
            yield return new object[] { null, PasswordResetErrorDescriber.PasswordRequired() };
            yield return new object[] { "", PasswordResetErrorDescriber.PasswordRequired() };
            yield return new object[] { "short", PasswordResetErrorDescriber.PasswordLength() };
            yield return new object[] { "shorting", PasswordResetErrorDescriber.PasswordInvalid() };
            yield return new object[] { "Shorting", PasswordResetErrorDescriber.PasswordInvalid() };
            yield return new object[] { "Shorting123", PasswordResetErrorDescriber.PasswordSpecial() };
        }
    }
}
