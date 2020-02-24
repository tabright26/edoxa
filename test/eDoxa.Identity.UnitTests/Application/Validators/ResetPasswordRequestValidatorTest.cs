// Filename: ResetPasswordRequestValidatorTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Application.ErrorDescribers;
using eDoxa.Identity.Api.Application.Validators;

using FluentAssertions;

using FluentValidation.TestHelper;

using Xunit;

namespace eDoxa.Identity.UnitTests.Application.Validators
{
    public sealed class ResetPasswordRequestValidatorTest
    {
        public static TheoryData<string> ValidEmails =>
            new TheoryData<string>
            {
                "gabriel@edoxa.gg",
                "francis.love.skyrim123@edoxa.gg",
                "!gab_riel/$@edoxa.gg"
            };

        public static TheoryData<string> InvalidEmails =>
            new TheoryData<string>
            {
                string.Empty,
                "gabrieledoxa.gg"
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
                {"", PasswordResetErrorDescriber.PasswordRequired()},
                {"short", PasswordResetErrorDescriber.PasswordLength()},
                {"shorting", PasswordResetErrorDescriber.PasswordInvalid()},
                {"Shorting", PasswordResetErrorDescriber.PasswordInvalid()},
                {"Shorting123", PasswordResetErrorDescriber.PasswordSpecial()}
            };

        //[Theory]
        //[MemberData(nameof(ValidEmails))]
        //public void Validate_WhenEmailIsValid_ShouldNotHaveValidationErrorFor(string email)
        //{
        //    // Arrange
        //    var validator = new ResetPasswordRequestValidator();

        //    // Act - Assert
        //    validator.ShouldNotHaveValidationErrorFor(request => request.Email, email);
        //}

        //[Theory]
        //[MemberData(nameof(InvalidEmails))]
        //public void Validate_WhenEmailIsInvalid_ShouldNotHaveValidationErrorFor(string email)
        //{
        //    // Arrange
        //    var validator = new ResetPasswordRequestValidator();

        //    // Act - Assert
        //    validator.ShouldHaveValidationErrorFor(request => request.Email, email);
        //}

        [Theory]
        [MemberData(nameof(ValidPasswords))]
        public void Validate_WhenPasswordIsValid_ShouldNotHaveValidationErrorFor(string password)
        {
            // Arrange
            var validator = new ResetPasswordRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Password, password);
        }

        [Theory]
        [MemberData(nameof(InvalidPasswords))]
        public void Validate_WhenPasswordIsInvalid_ShouldNotHaveValidationErrorFor(string password, string errorMessage)
        {
            // Arrange
            var validator = new ResetPasswordRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Password, password);

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }
    }
}
