// Filename: AddressPutRequestValidatorTest.cs
// Date Created: 2019-08-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Areas.Identity.Validators;

using FluentValidation.TestHelper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Validators
{
    [TestClass]
    public sealed class AddressPutRequestValidatorTest
    {
        [DataTestMethod]
        [DataRow("Line1")]
        public void Validate_WhenLine1IsValid_ShouldNotHaveValidationErrorFor(string line1)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Line1, line1);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void Validate_WhenLine1IsInvalid_ShouldHaveValidationErrorFor(string line1)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            validator.ShouldHaveValidationErrorFor(request => request.Line1, line1);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("Line2")]
        public void Validate_WhenLine2IsValid_ShouldNotHaveValidationErrorFor(string line2)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Line2, line2);
        }

        [DataTestMethod]
        [DataRow("")]
        public void Validate_WhenLine2IsInvalid_ShouldHaveValidationErrorFor(string line2)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            validator.ShouldHaveValidationErrorFor(request => request.Line2, line2);
        }

        [DataTestMethod]
        [DataRow("City")]
        public void Validate_WhenCityIsValid_ShouldNotHaveValidationErrorFor(string city)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.City, city);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void Validate_WhenCityIsInvalid_ShouldHaveValidationErrorFor(string city)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            validator.ShouldHaveValidationErrorFor(request => request.City, city);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("State")]
        public void Validate_WhenStateIsValid_ShouldNotHaveValidationErrorFor(string state)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.State, state);
        }

        [DataTestMethod]
        [DataRow("")]
        public void Validate_WhenStateIsInvalid_ShouldHaveValidationErrorFor(string state)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            validator.ShouldHaveValidationErrorFor(request => request.State, state);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("PostalCode")]
        public void Validate_WhenPostalCodeIsValid_ShouldNotHaveValidationErrorFor(string postalCode)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.PostalCode, postalCode);
        }

        [DataTestMethod]
        [DataRow("")]
        public void Validate_WhenPostalCodeIsInvalid_ShouldHaveValidationErrorFor(string postalCode)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            validator.ShouldHaveValidationErrorFor(request => request.PostalCode, postalCode);
        }
    }
}
