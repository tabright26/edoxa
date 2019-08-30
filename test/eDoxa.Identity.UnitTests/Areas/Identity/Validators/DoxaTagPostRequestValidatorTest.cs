// Filename: DoxaTagPostRequestValidatorTest.cs
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
    public sealed class DoxaTagPostRequestValidatorTest
    {
        [DataTestMethod]
        [DataRow("DoxaTagName")]
        [DataRow("Doxa_Tag_Name")]
        [DataRow("aaaaaaaaaaaaaaaa")]
        public void Validate_WhenNameIsValid_ShouldNotHaveValidationErrorFor(string name)
        {
            // Arrange
            var validator = new DoxaTagPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Name, name);
        }

        [DataTestMethod]
        [DataRow(null,"DoxaTag is required")]
        [DataRow("", "DoxaTag is required")]
        [DataRow("@DoxaTagName", "DoxaTag invalid. May only contains (a-z,A-Z,_)")]
        [DataRow("DoxaTagName1", "DoxaTag invalid. May only contains (a-z,A-Z,_)")]
        [DataRow("_DoxaTagName", "DoxaTag invalid. Cannot start or end with _")]
        [DataRow("DoxaTagName_", "DoxaTag invalid. Cannot start or end with _")]
        [DataRow("D", "DoxaTag must be between 2 and 16 characters long")]
        [DataRow("aaaaaaaaaaaaaaaaa","DoxaTag must be between 2 and 16 characters long")]
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
