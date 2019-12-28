﻿// Filename: PasswordForgotPostRequestValidatorTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Application.ErrorDescribers;
using eDoxa.Identity.Api.Application.Validators;

using FluentAssertions;

using FluentValidation.TestHelper;

using Xunit;

namespace eDoxa.Identity.UnitTests.Application.Validators
{
    public sealed class ForgotPasswordRequestValidatorTest
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
                {"", PasswordForgotErrorDescriber.EmailRequired()},
                {"gabrieledoxa.gg", PasswordForgotErrorDescriber.EmailInvalid()},
                {"!gab_riel/$@edoxa.gg", PasswordForgotErrorDescriber.EmailInvalid()}
            };

        [Theory]
        [MemberData(nameof(ValidEmails))]
        public void Validate_WhenEmailIsValid_ShouldNotHaveValidationErrorFor(string email)
        {
            // Arrange
            var validator = new ForgotPasswordRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Email, email);
        }

        [Theory]
        [MemberData(nameof(InvalidEmails))]
        public void Validate_WhenEmailIsInvalid_ShouldNotHaveValidationErrorFor(string email, string errorMessage)
        {
            // Arrange
            var validator = new ForgotPasswordRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Email, email);

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }
    }
}