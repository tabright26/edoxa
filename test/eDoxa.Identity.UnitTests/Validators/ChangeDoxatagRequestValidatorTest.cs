// Filename: ChangeDoxatagRequestValidatorTest.cs
// Date Created: 2019-11-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Application.Validators;

using FluentAssertions;

using FluentValidation.TestHelper;

using Xunit;

namespace eDoxa.Identity.UnitTests.Validators
{
    public sealed partial class ChangeDoxatagRequestValidatorTest
    {
        [Theory]
        [MemberData(nameof(ValidNameData))]
        public void Validate_WhenNameIsValid_ShouldNotHaveValidationErrorFor(string name)
        {
            // Arrange
            var validator = new ChangeDoxatagRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Name, name);
        }

        [Theory]
        [MemberData(nameof(InvalidNameData))]
        public void Validate_WhenNameIsInvalid_ShouldHaveValidationErrorFor(string name, string errorMessage)
        {
            // Arrange
            var validator = new ChangeDoxatagRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Name, name);

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }
    }
}
