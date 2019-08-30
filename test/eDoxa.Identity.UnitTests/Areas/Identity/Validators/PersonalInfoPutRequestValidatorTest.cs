// Filename: PersonalInfoPutRequestValidatorTest.cs
// Date Created: 2019-08-22
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Areas.Identity.Validators;

using FluentAssertions;

using FluentValidation.TestHelper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Validators
{
    [TestClass]
    public sealed class PersonalInfoPutRequestValidatorTest
    {
        [DataTestMethod]
        [DataRow("Gabriel")]
        [DataRow("Gabriel-Roy")]
        [DataRow("Gabriel-Roy-R")]
        public void Validate_WhenFirstNameIsValid_ShouldNotHaveValidationErrorFor(string firstName)
        {
            // Arrange
            var validator = new PersonalInfoPutRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.FirstName, firstName);
        }

        [DataTestMethod]
        [DataRow(null, "First name is required")]
        [DataRow("", "First name is required")]
        [DataRow("G", "First name must be between 2 and 16 characters long")]
        [DataRow("Gabriel-Roy-Gab-R", "First name must be between 2 and 16 characters long")]
        [DataRow("Gab123", "First name invalid. Only letters and hyphens allowed")]
        [DataRow("Gabriel-Ro_Roy", "First name invalid. Only letters and hyphens allowed")]
        [DataRow("gabriel-Roy", "First name invalid. Every part must start with an uppercase")]
        [DataRow("Gabriel-roy", "First name invalid. Every part must start with an uppercase")]
        public void Validate_WhenFirstNameIsInvalid_ShouldHaveValidationErrorFor(string firstName, string errorMessage)
        {
            // Arrange
            var validator = new PersonalInfoPutRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.FirstName, firstName);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }
    }
}
