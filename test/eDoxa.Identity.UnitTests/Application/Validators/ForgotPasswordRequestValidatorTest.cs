// Filename: ForgotPasswordRequestValidatorTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Application.Validators;

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
                "francis.love.skyrim123@edoxa.gg",
                "!gab_riel/$@edoxa.gg"
            };

        public static TheoryData<string> InvalidEmails =>
            new TheoryData<string>
            {
                "",
                "gabrieledoxa.gg",
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
        public void Validate_WhenEmailIsInvalid_ShouldNotHaveValidationErrorFor(string email)
        {
            // Arrange
            var validator = new ForgotPasswordRequestValidator();

            // Act - Assert
            validator.ShouldHaveValidationErrorFor(request => request.Email, email);
        }
    }
}
