// Filename: AddressPutRequestValidatorTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Identity.Api.Areas.Identity.ErrorDescribers;
using eDoxa.Identity.Api.Areas.Identity.Validators;

using FluentAssertions;

using FluentValidation.TestHelper;

using Xunit;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Validators
{
    public sealed class AddressPutRequestValidatorTest
    {
        public static TheoryData<string, string> InvalidLine1Address =>
            new TheoryData<string, string>
            {
                {null, AddressBookErrorDescriber.Line1Required()},
                {"", AddressBookErrorDescriber.Line1Required()},
                {"This_is_an_adress", AddressBookErrorDescriber.Line1Invalid()}
            };

        public static TheoryData<string, string> InvalidCities =>
            new TheoryData<string, string>
            {
                {null, AddressBookErrorDescriber.CityRequired()},
                {"", AddressBookErrorDescriber.CityRequired()},
                {"123City", AddressBookErrorDescriber.CityInvalid()},
                {"OK_Test", AddressBookErrorDescriber.CityInvalid()}
            };

        public static TheoryData<string, string> InvalidStates =>
            new TheoryData<string, string>
            {
                {null, AddressBookErrorDescriber.StateRequired()},
                {"", AddressBookErrorDescriber.StateRequired()},
                {"123City", AddressBookErrorDescriber.StateInvalid()},
                {"OK_Test", AddressBookErrorDescriber.StateInvalid()}
            };

        public static TheoryData<string, string> InvalidPostalCodes =>
            new TheoryData<string, string>
            {
                {null, AddressBookErrorDescriber.PostalCodeRequired()},
                {"", AddressBookErrorDescriber.PostalCodeRequired()},
                {"1234", AddressBookErrorDescriber.PostalCodeLength()},
                {"1234.5", AddressBookErrorDescriber.PostalCodeInvalidError()}
            };

        public static TheoryData<string> ValidLine2Address =>
            new TheoryData<string>
            {
                "4140 Av. Kindersley, ap 13"
            };

        public static TheoryData<string> ValidCities =>
            new TheoryData<string>
            {
                "City",
                "City-of Testing"
            };

        public static TheoryData<string> ValidStates =>
            new TheoryData<string>
            {
                "State",
                "State-of Testing"
            };

        public static TheoryData<string> ValidPostalCodes =>
            new TheoryData<string>
            {
                "12345",
                "H4P1K8"
            };

        [Theory]
        [MemberData(nameof(ValidLine1Address))]
        public void Validate_WhenLine1IsValid_ShouldNotHaveValidationErrorFor(string line1)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Line1, line1);
        }

        public static IEnumerable<object[]> ValidLine1Address()
        {
            yield return new object[] {"4140 Av. Kindersley, ap 13"};
        }

        [Theory]
        [MemberData(nameof(InvalidLine1Address))]
        public void Validate_WhenLine1IsInvalid_ShouldHaveValidationErrorFor(string line1, string errorMessage)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Line1, line1);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [Theory]
        [MemberData(nameof(ValidLine2Address))]
        public void Validate_WhenLine2IsValid_ShouldNotHaveValidationErrorFor(string line2)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Line2, line2);
        }

        [Theory]
        [MemberData(nameof(InvalidLine2Address))]
        public void Validate_WhenLine2IsInvalid_ShouldHaveValidationErrorFor(string line2, string errorMessage)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Line2, line2);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        public static IEnumerable<object[]> InvalidLine2Address()
        {
            yield return new object[] {"This_is_an_adress", AddressBookErrorDescriber.Line2Invalid()};
        }

        [Theory]
        [MemberData(nameof(ValidCities))]
        public void Validate_WhenCityIsValid_ShouldNotHaveValidationErrorFor(string city)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.City, city);
        }

        [Theory]
        [MemberData(nameof(InvalidCities))]
        public void Validate_WhenCityIsInvalid_ShouldHaveValidationErrorFor(string city, string errorMessage)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.City, city);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [Theory]
        [MemberData(nameof(ValidStates))]
        public void Validate_WhenStateIsValid_ShouldNotHaveValidationErrorFor(string state)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.State, state);
        }

        [Theory]
        [MemberData(nameof(InvalidStates))]
        public void Validate_WhenStateIsInvalid_ShouldHaveValidationErrorFor(string state, string errorMessage)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.State, state);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [Theory]
        [MemberData(nameof(ValidPostalCodes))]
        public void Validate_WhenPostalCodeIsValid_ShouldNotHaveValidationErrorFor(string postalCode)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.PostalCode, postalCode);
        }

        [Theory]
        [MemberData(nameof(InvalidPostalCodes))]
        public void Validate_WhenPostalCodeIsInvalid_ShouldHaveValidationErrorFor(string postalCode, string errorMessage)
        {
            // Arrange
            var validator = new AddressPutRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.PostalCode, postalCode);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }
    }
}
