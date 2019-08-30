// Filename: AddressPutRequestValidatorTest.cs
// Date Created: 2019-08-23
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Identity.Api.Areas.Identity.ErrorDescribers;
using eDoxa.Identity.Api.Areas.Identity.Validators;

using FluentAssertions;

using FluentValidation.TestHelper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Validators
{
    [TestClass]
    public sealed class AddressPutRequestValidatorTest
    {
        [DataTestMethod]
        [DynamicData(nameof(ValidLine1Address), DynamicDataSourceType.Method)]
        public void Validate_WhenLine1IsValid_ShouldNotHaveValidationErrorFor(string line1)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Line1, line1);
        }

        private static IEnumerable<object[]> ValidLine1Address()
        {
            yield return new object[] { "4140 Av. Kindersley, ap 13" };
        }

        [DataTestMethod]
        [DynamicData(nameof(InvalidLine1Address), DynamicDataSourceType.Method)]
        public void Validate_WhenLine1IsInvalid_ShouldHaveValidationErrorFor(string line1, string errorMessage)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Line1, line1);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        private static IEnumerable<object[]> InvalidLine1Address()
        {
            yield return new object[] { null, AddressBookErrorDescriber.Line1Required() };
            yield return new object[] { "", AddressBookErrorDescriber.Line1Required() };
            yield return new object[] { "This_is_an_adress", AddressBookErrorDescriber.Line1Invalid() };
        }

        [DataTestMethod]
        [DynamicData(nameof(ValidLine2Address), DynamicDataSourceType.Method)]
        public void Validate_WhenLine2IsValid_ShouldNotHaveValidationErrorFor(string line2)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Line2, line2);
        }

        private static IEnumerable<object[]> ValidLine2Address()
        {
            yield return new object[] { "4140 Av. Kindersley, ap 13" };
        }

        [DataTestMethod]
        [DynamicData(nameof(InvalidLine2Address), DynamicDataSourceType.Method)]
        public void Validate_WhenLine2IsInvalid_ShouldHaveValidationErrorFor(string line2, string errorMessage)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Line2, line2);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        private static IEnumerable<object[]> InvalidLine2Address()
        {
            yield return new object[] { "This_is_an_adress", AddressBookErrorDescriber.Line2Invalid() };
        }

        [DataTestMethod]
        [DynamicData(nameof(ValidCities), DynamicDataSourceType.Method)]
        public void Validate_WhenCityIsValid_ShouldNotHaveValidationErrorFor(string city)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.City, city);
        }

        private static IEnumerable<object[]> ValidCities()
        {
            yield return new object[] { "City" };
            yield return new object[] { "City-of Testing" };
        }

        [DataTestMethod]
        [DynamicData(nameof(InvalidCities), DynamicDataSourceType.Method)]
        public void Validate_WhenCityIsInvalid_ShouldHaveValidationErrorFor(string city, string errorMessage)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.City, city);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        private static IEnumerable<object[]> InvalidCities()
        {
            yield return new object[] { null, AddressBookErrorDescriber.CityRequired() };
            yield return new object[] { "", AddressBookErrorDescriber.CityRequired() };
            yield return new object[] { "123City", AddressBookErrorDescriber.CityInvalid() };
            yield return new object[] { "OK_Test", AddressBookErrorDescriber.CityInvalid() };
        }

        [DataTestMethod]
        [DynamicData(nameof(ValidStates), DynamicDataSourceType.Method)]
        public void Validate_WhenStateIsValid_ShouldNotHaveValidationErrorFor(string state)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.State, state);
        }

        private static IEnumerable<object[]> ValidStates()
        {
            yield return new object[] { "State" };
            yield return new object[] { "State-of Testing" };
        }

        [DataTestMethod]
        [DynamicData(nameof(InvalidStates), DynamicDataSourceType.Method)]
        public void Validate_WhenStateIsInvalid_ShouldHaveValidationErrorFor(string state, string errorMessage)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.State, state);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        private static IEnumerable<object[]> InvalidStates()
        {
            yield return new object[] { null, AddressBookErrorDescriber.StateRequired() };
            yield return new object[] { "", AddressBookErrorDescriber.StateRequired() };
            yield return new object[] { "123State", AddressBookErrorDescriber.StateInvalid() };
            yield return new object[] { "OK_Test", AddressBookErrorDescriber.StateInvalid() };
        }

        [DataTestMethod]
        [DynamicData(nameof(ValidPostalCodes), DynamicDataSourceType.Method)]
        public void Validate_WhenPostalCodeIsValid_ShouldNotHaveValidationErrorFor(string postalCode)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.PostalCode, postalCode);
        }

        private static IEnumerable<object[]> ValidPostalCodes()
        {
            yield return new object[] { "12345" };
            yield return new object[] { "H4P1K8" };
        }

        [DataTestMethod]
        [DynamicData(nameof(InvalidPostalCodes), DynamicDataSourceType.Method)]
        public void Validate_WhenPostalCodeIsInvalid_ShouldHaveValidationErrorFor(string postalCode, string errorMessage)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.PostalCode, postalCode);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        private static IEnumerable<object[]> InvalidPostalCodes()
        {
            yield return new object[] { null, AddressBookErrorDescriber.PostalCodeRequired() };
            yield return new object[] { "", AddressBookErrorDescriber.PostalCodeRequired() };
            yield return new object[] { "1234", AddressBookErrorDescriber.PostalCodeLength() };
            yield return new object[] { "1234.5", AddressBookErrorDescriber.PostalCodeInvalidError() };
        }
    }
}
