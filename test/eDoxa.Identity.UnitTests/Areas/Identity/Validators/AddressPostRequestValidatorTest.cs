// Filename: AddressPostRequestValidatorTest.cs// Date Created: 2019-08-23
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
    public sealed class AddressPostRequestValidatorTest
    {
        [DataTestMethod]
        [DataRow("Canada")]
        [DataRow("United States")]
        public void Validate_WhenCountryIsValid_ShouldNotHaveValidationErrorFor(string country)
        {
            // Arrange
            var validator = new AddressPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Country, country);
        }

        [DataTestMethod]
        [DataRow(null, "Country is required")]
        [DataRow("", "Country is required")]
        [DataRow("Country", "Country invalid. Must be Canada or United States")]
        public void Validate_WhenCountryIsInvalid_ShouldHaveValidationErrorFor(string country, string errorMessage)
        {
            // Arrange
            var validator = new AddressPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Country, country);

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [DataTestMethod]
        [DataRow("4140 Av. Kindersley, ap 13")]
        public void Validate_WhenLine1IsValid_ShouldNotHaveValidationErrorFor(string line1)
        {
            // Arrange
            var validator = new AddressPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Line1, line1);
        }

        [DataTestMethod]
        [DataRow(null, "Main address is required")]
        [DataRow("", "Main address is required")]
        [DataRow("This_is_an_adress", "Main address invalid. Must not have special characters")]
        public void Validate_WhenLine1IsInvalid_ShouldHaveValidationErrorFor(string line1, string errorMessage)
        {
            // Arrange
            var validator = new AddressPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Line1, line1);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [DataTestMethod]
        [DataRow("4140 Av. Kindersley, ap 13")]
        public void Validate_WhenLine2IsValid_ShouldNotHaveValidationErrorFor(string line2)
        {
            // Arrange
            var validator = new AddressPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Line2, line2);
        }

        [DataTestMethod]
        [DataRow("This_is_an_adress", "Secondary address invalid. Must not have special characters")]
        public void Validate_WhenLine2IsInvalid_ShouldHaveValidationErrorFor(string line2, string errorMessage)
        {
            // Arrange
            var validator = new AddressPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Line2, line2);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [DataTestMethod]
        [DataRow("City")]
        [DataRow("City-of Testing")]
        public void Validate_WhenCityIsValid_ShouldNotHaveValidationErrorFor(string city)
        {
            // Arrange
            var validator = new AddressPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.City, city);
        }

        [DataTestMethod]
        [DataRow(null, "City is required")]
        [DataRow("", "City is required")]
        [DataRow("123City", "City is invalid. Only letters, spaces and hyphens allowed")]
        [DataRow("OK_Test", "City is invalid. Only letters, spaces and hyphens allowed")]
        public void Validate_WhenCityIsInvalid_ShouldHaveValidationErrorFor(string city, string errorMessage)
        {
            // Arrange
            var validator = new AddressPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.City, city);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [DataTestMethod]
        [DataRow("State")]
        [DataRow("State-of Testing")]
        public void Validate_WhenStateIsValid_ShouldNotHaveValidationErrorFor(string state)
        {
            // Arrange
            var validator = new AddressPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.State, state);
        }

        [DataTestMethod]
        [DataRow(null, "State is required")]
        [DataRow("", "State is required")]
        [DataRow("123State", "State is invalid. Only letters, spaces and hyphens allowed")]
        [DataRow("OK_Test", "State is invalid. Only letters, spaces and hyphens allowed")]
        public void Validate_WhenStateIsInvalid_ShouldHaveValidationErrorFor(string state, string errorMessage)
        {
            // Arrange
            var validator = new AddressPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.State, state);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [DataTestMethod]
        [DataRow("12345")]
        [DataRow("H4P1K8")]
        public void Validate_WhenPostalCodeIsValid_ShouldNotHaveValidationErrorFor(string postalCode)
        {
            // Arrange
            var validator = new AddressPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.PostalCode, postalCode);
        }

        [DataTestMethod]
        [DataRow(null, "Postal code is required")]
        [DataRow("", "Postal code is required")]
        [DataRow("1234", "Postal code must be between 5 and 6 characters long")]
        [DataRow("1234.5", "Postal code is invalid. Only letters and numbers allowed")]
        public void Validate_WhenPostalCodeIsInvalid_ShouldHaveValidationErrorFor(string postalCode, string errorMessage)
        {
            // Arrange
            var validator = new AddressPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.PostalCode, postalCode);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }
    }
}
