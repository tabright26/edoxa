// Filename: DoxaTagPostRequestValidatorTest.cs
// Date Created: 2019-08-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Areas.Identity.Validators;

using FluentValidation.TestHelper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Validators
{
    [TestClass]
    public sealed class DoxaTagPostRequestValidatorTest
    {
        [DataTestMethod]
        [DataRow("DoxaTagName")]
        [DataRow("aaaaaaaaaaaaaaaa")]
        public void Validate_WhenValid_ShouldNotHaveValidationErrorFor(string name)
        {
            // Arrange
            var validator = new DoxaTagPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Name, name);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("@DoxaTagName3")]
        [DataRow(" DoxaTagName3")]
        [DataRow("DoxaTag Name3")]
        [DataRow("DoxaTagName%")]
        [DataRow("D")]
        [DataRow("aaaaaaaaaaaaaaaaa")]
        public void Validate_WhenInvalid_ShouldHaveValidationErrorFor(string name)
        {
            // Arrange
            var validator = new DoxaTagPostRequestValidator();

            // Act - Assert
            validator.ShouldHaveValidationErrorFor(request => request.Name, name);
        }
    }
}
