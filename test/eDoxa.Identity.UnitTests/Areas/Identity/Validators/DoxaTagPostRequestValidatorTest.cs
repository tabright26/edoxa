// Filename: DoxaTagPostRequestValidatorTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Areas.Identity.ErrorDescribers;
using eDoxa.Identity.Api.Areas.Identity.Validators;

using FluentAssertions;

using FluentValidation.TestHelper;

using Xunit;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Validators
{
    public sealed class DoxaTagPostRequestValidatorTest
    {
        public static TheoryData<string> ValidDoxaTags =>
            new TheoryData<string>
            {
                "DoxaTagName",
                "Doxa_Tag_Name",
                "aaaaaaaaaaaaaaaa"
            };

        public static TheoryData<string, string> InvalidDoxaTags =>
            new TheoryData<string, string>
            {
                {null, DoxaTagErrorDescriber.Required()},
                {"", DoxaTagErrorDescriber.Required()},
                {"D", DoxaTagErrorDescriber.Length()},
                {"aaaaaaaaaaaaaaaaa", DoxaTagErrorDescriber.Length()},
                {"@DoxaTagName", DoxaTagErrorDescriber.Invalid()},
                {"DoxaTagName1", DoxaTagErrorDescriber.Invalid()},
                {"_DoxaTagName", DoxaTagErrorDescriber.InvalidUnderscore()},
                {"DoxaTagName_", DoxaTagErrorDescriber.InvalidUnderscore()}
            };

        [Theory]
        [MemberData(nameof(ValidDoxaTags))]
        public void Validate_WhenNameIsValid_ShouldNotHaveValidationErrorFor(string name)
        {
            // Arrange
            var validator = new DoxaTagPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Name, name);
        }

        [Theory]
        [MemberData(nameof(InvalidDoxaTags))]
        public void Validate_WhenNameIsInvalid_ShouldHaveValidationErrorFor(string name, string errorMessage)
        {
            // Arrange
            var validator = new DoxaTagPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Name, name);

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }
    }
}
