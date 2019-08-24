// Filename: PersonalInfoPutRequestValidatorTest.cs
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
    public sealed class PersonalInfoPutRequestValidatorTest
    {
        [DataTestMethod]
        [DataRow("DoxaTagName")]
        [DataRow("aaaaaaaaaaaaaaaa")]
        public void Validate_WhenFirstNameIsValid_ShouldNotHaveValidationErrorFor(string firstName)
        {
            // Arrange
            var validator = new PersonalInfoPutRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.FirstName, firstName);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("D")]
        [DataRow("aaaaaaaaaaaaaaaaa")]
        public void Validate_WhenFirstNameIsInvalid_ShouldHaveValidationErrorFor(string firstName)
        {
            // Arrange
            var validator = new PersonalInfoPutRequestValidator();

            // Act - Assert
            validator.ShouldHaveValidationErrorFor(request => request.FirstName, firstName);
        }
    }
}
