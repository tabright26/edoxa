// Filename: UpdateProfileRequestValidatorTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Application.Validators;

using FluentAssertions;

using FluentValidation.TestHelper;

using Xunit;

namespace eDoxa.Identity.UnitTests.Application.Validators
{
    public sealed class UpdateProfileRequestValidatorTest
    {
        public static TheoryData<string> ValidFirstNames =>
            new TheoryData<string>
            {
                "Gabriel",
                "Gabriel-Roy",
                "Gabriel-Roy-R",
                "gabriel-Roy",
                "Gabriel-roy"
            };

        public static TheoryData<string, string> InvalidFirstNames =>
            new TheoryData<string, string>
            {
                {"", "First name is required"},
                {"G", "First name must be between 2 and 16 characters long"},
                {"Gabriel-Roy-Gab-R", "First name must be between 2 and 16 characters long"},
                {"Gab123", "First name invalid. Only letters and hyphens allowed"},
                {"Gabriel-Ro_Roy", "First name invalid. Only letters and hyphens allowed"}
            };

        [Theory]
        [MemberData(nameof(ValidFirstNames))]
        public void Validate_WhenFirstNameIsValid_ShouldNotHaveValidationErrorFor(string firstName)
        {
            // Arrange
            var validator = new CreateProfileRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.FirstName, firstName);
        }

        [Theory]
        [MemberData(nameof(InvalidFirstNames))]
        public void Validate_WhenFirstNameIsInvalid_ShouldHaveValidationErrorFor(string firstName, string errorMessage)
        {
            // Arrange
            var validator = new CreateProfileRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.FirstName, firstName);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }
    }
}
