// Filename: PasswordResetPostRequestValidatorTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.ErrorDescribers;
using eDoxa.Identity.Api.Validators;

using FluentAssertions;

using FluentValidation.TestHelper;

using Xunit;

namespace eDoxa.Identity.UnitTests.Validators
{
    public sealed class ResetPasswordRequestValidatorTest
    {
        public static TheoryData<string> ValidEmails =>
            new TheoryData<string>
            {
                "gabriel@edoxa.gg",
                "francis.love.skyrim123@edoxa.gg"
            };

        public static TheoryData<string, string> InvalidEmails =>
            new TheoryData<string, string>
            {
                {null, PasswordForgotErrorDescriber.EmailRequired()},
                {"", PasswordForgotErrorDescriber.EmailRequired()},
                {"gabrieledoxa.gg", PasswordForgotErrorDescriber.EmailInvalid()},
                {"!gab_riel/$@edoxa.gg", PasswordForgotErrorDescriber.EmailInvalid()}
            };

        public static TheoryData<string> ValidPasswords =>
            new TheoryData<string>
            {
                "Testing123!",
                "th1s_Is-a-p4ssw3rd"
            };

        public static TheoryData<string, string> InvalidPasswords =>
            new TheoryData<string, string>
            {
                {null, PasswordResetErrorDescriber.PasswordRequired()},
                {"", PasswordResetErrorDescriber.PasswordRequired()},
                {"short", PasswordResetErrorDescriber.PasswordLength()},
                {"shorting", PasswordResetErrorDescriber.PasswordInvalid()},
                {"Shorting", PasswordResetErrorDescriber.PasswordInvalid()},
                {"Shorting123", PasswordResetErrorDescriber.PasswordSpecial()}
            };

        [Theory]
        [MemberData(nameof(ValidEmails))]
        public void Validate_WhenEmailIsValid_ShouldNotHaveValidationErrorFor(string email)
        {
            // Arrange
            var validator = new ResetPasswordRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Email, email);
        }

        [Theory]
        [MemberData(nameof(InvalidEmails))]
        public void Validate_WhenEmailIsInvalid_ShouldNotHaveValidationErrorFor(string email, string errorMessage)
        {
            // Arrange
            var validator = new ResetPasswordRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Email, email);

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [Theory]
        [MemberData(nameof(ValidEmails))]
        public void Validate_WhenPasswordIsValid_ShouldNotHaveValidationErrorFor(string email)
        {
            // Arrange
            var validator = new ResetPasswordRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Email, email);
        }

        [Theory]
        [MemberData(nameof(InvalidEmails))]
        public void Validate_WhenPasswordIsInvalid_ShouldNotHaveValidationErrorFor(string email, string errorMessage)
        {
            // Arrange
            var validator = new ResetPasswordRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Email, email);

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }
    }
}
