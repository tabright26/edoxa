// Filename: DoxatagPostRequestValidatorTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.ErrorDescribers;
using eDoxa.Identity.Api.Validators;

using FluentAssertions;

using FluentValidation.TestHelper;

using Xunit;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Validators
{
    public sealed class DoxatagPostRequestValidatorTest
    {
        public static TheoryData<string> ValidDoxatags =>
            new TheoryData<string>
            {
                "DoxatagName",
                "Doxa_Tag_Name",
                "aaaaaaaaaaaaaaaa"
            };

        public static TheoryData<string, string> InvalidDoxatags =>
            new TheoryData<string, string>
            {
                {null, DoxatagErrorDescriber.Required()},
                {"", DoxatagErrorDescriber.Required()},
                {"D", DoxatagErrorDescriber.Length()},
                {"aaaaaaaaaaaaaaaaa", DoxatagErrorDescriber.Length()},
                {"@DoxatagName", DoxatagErrorDescriber.Invalid()},
                {"DoxatagName1", DoxatagErrorDescriber.Invalid()},
                {"_DoxatagName", DoxatagErrorDescriber.InvalidUnderscore()},
                {"DoxatagName_", DoxatagErrorDescriber.InvalidUnderscore()}
            };

        [Theory]
        [MemberData(nameof(ValidDoxatags))]
        public void Validate_WhenNameIsValid_ShouldNotHaveValidationErrorFor(string name)
        {
            // Arrange
            var validator = new DoxatagPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Name, name);
        }

        [Theory]
        [MemberData(nameof(InvalidDoxatags))]
        public void Validate_WhenNameIsInvalid_ShouldHaveValidationErrorFor(string name, string errorMessage)
        {
            // Arrange
            var validator = new DoxatagPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Name, name);

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }
    }
}
