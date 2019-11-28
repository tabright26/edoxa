// Filename: PersonalInfoPutRequestValidatorTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Validators;

using FluentAssertions;

using FluentValidation.TestHelper;

using Xunit;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Validators
{
    public sealed class PersonalInfoPutRequestValidatorTest
    {
        public static TheoryData<string> ValidFirstNames =>
            new TheoryData<string>
            {
                "Gabriel",
                "Gabriel-Roy",
                "Gabriel-Roy-R"
            };

        public static TheoryData<string, string> InvalidFirstNames =>
            new TheoryData<string, string>
            {
                {null, "First name is required"},
                {"", "First name is required"},
                {"G", "First name must be between 2 and 16 characters long"},
                {"Gabriel-Roy-Gab-R", "First name must be between 2 and 16 characters long"},
                {"Gab123", "First name invalid. Only letters and hyphens allowed"},
                {"Gabriel-Ro_Roy", "First name invalid. Only letters and hyphens allowed"},
                {"gabriel-Roy", "First name invalid. Every part must start with an uppercase"},
                {"Gabriel-roy", "First name invalid. Every part must start with an uppercase"}
            };

        [Theory]
        [MemberData(nameof(ValidFirstNames))]
        public void Validate_WhenFirstNameIsValid_ShouldNotHaveValidationErrorFor(string firstName)
        {
            // Arrange
            var validator = new InformationsPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.FirstName, firstName);
        }

        [Theory]
        [MemberData(nameof(InvalidFirstNames))]
        public void Validate_WhenFirstNameIsInvalid_ShouldHaveValidationErrorFor(string firstName, string errorMessage)
        {
            // Arrange
            var validator = new InformationsPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.FirstName, firstName);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }
    }
}
