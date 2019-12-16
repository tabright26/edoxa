// Filename: CreateAddressRequestValidatorTest.cs
// Date Created: 2019-11-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Identity.Enums;
using eDoxa.Identity.Api.Application.ErrorDescribers;
using eDoxa.Identity.Api.Application.Validators;

using FluentAssertions;

using FluentValidation.TestHelper;

using Xunit;

namespace eDoxa.Identity.UnitTests.Application.Validators
{
    public sealed class CreateAddressRequestValidatorTest
    {
        public static TheoryData<CountryDto> ValidCountries =>
            new TheoryData<CountryDto>
            {
                CountryDto.Canada,
                CountryDto.UnitedStates
            };

        public static TheoryData<CountryDto, string> InvalidCountries =>
            new TheoryData<CountryDto, string>
            {
                {CountryDto.None, ""},  // TODO: Add error message.
                {CountryDto.All, ""}    // TODO: Add error message.
            };

        public static TheoryData<string> ValidLine1Address =>
            new TheoryData<string>
            {
                "4140 Av. Kindersley, ap 13"
            };

        public static TheoryData<string, string> InvalidLine1Address =>
            new TheoryData<string, string>
            {
                {"", AddressBookErrorDescriber.Line1Required()},
                {"This_is_an_adress", AddressBookErrorDescriber.Line1Invalid()}
            };

        public static TheoryData<string> ValidLine2Address =>
            new TheoryData<string>
            {
                "4140 Av. Kindersley, ap 13"
            };

        public static TheoryData<string, string> InvalidLine2Address =>
            new TheoryData<string, string>
            {
                {"This_is_an_adress", AddressBookErrorDescriber.Line2Invalid()}
            };

        public static TheoryData<string> ValidCities =>
            new TheoryData<string>
            {
                "City",
                "City-of Testing"
            };

        public static TheoryData<string, string> InvalidCities =>
            new TheoryData<string, string>
            {
                {"", AddressBookErrorDescriber.CityRequired()},
                {"123City", AddressBookErrorDescriber.CityInvalid()},
                {"OK_Test", AddressBookErrorDescriber.CityInvalid()}
            };

        public static TheoryData<string> ValidStates =>
            new TheoryData<string>
            {
                "State",
                "State-of Testing"
            };

        public static TheoryData<string, string> InvalidStates =>
            new TheoryData<string, string>
            {
                {null, AddressBookErrorDescriber.StateRequired()},
                {"", AddressBookErrorDescriber.StateRequired()},
                {"123State", AddressBookErrorDescriber.StateInvalid()},
                {"OK_Test", AddressBookErrorDescriber.StateInvalid()}
            };

        public static TheoryData<string> ValidPostalCodes =>
            new TheoryData<string>
            {
                "12345",
                "H4P1K8"
            };

        public static TheoryData<string, string> InvalidPostalCodes =>
            new TheoryData<string, string>
            {
                {null, AddressBookErrorDescriber.PostalCodeRequired()},
                {null, AddressBookErrorDescriber.PostalCodeRequired()},
                {"1234", AddressBookErrorDescriber.PostalCodeLength()},
                {"1234.5", AddressBookErrorDescriber.PostalCodeInvalidError()}
            };

        [Theory(Skip = "TODO")]
        [MemberData(nameof(ValidCountries))]
        public void Validate_WhenCountryIsValid_ShouldNotHaveValidationErrorFor(CountryDto country)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Country, country);
        }

        [Theory(Skip = "TODO")]
        [MemberData(nameof(InvalidCountries))]
        public void Validate_WhenCountryIsInvalid_ShouldHaveValidationErrorFor(CountryDto country, string errorMessage)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator();

            // Act
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Country, country);

            // Assert
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [Theory]
        [MemberData(nameof(ValidLine1Address))]
        public void Validate_WhenLine1IsValid_ShouldNotHaveValidationErrorFor(string line1)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Line1, line1);
        }

        [Theory]
        [MemberData(nameof(InvalidLine1Address))]
        public void Validate_WhenLine1IsInvalid_ShouldHaveValidationErrorFor(string line1, string errorMessage)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Line1, line1);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [Theory]
        [MemberData(nameof(ValidLine2Address))]
        public void Validate_WhenLine2IsValid_ShouldNotHaveValidationErrorFor(string line2)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Line2, line2);
        }

        [Theory]
        [MemberData(nameof(InvalidLine2Address))]
        public void Validate_WhenLine2IsInvalid_ShouldHaveValidationErrorFor(string line2, string errorMessage)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Line2, line2);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [Theory]
        [MemberData(nameof(ValidCities))]
        public void Validate_WhenCityIsValid_ShouldNotHaveValidationErrorFor(string city)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.City, city);
        }

        [Theory]
        [MemberData(nameof(InvalidCities))]
        public void Validate_WhenCityIsInvalid_ShouldHaveValidationErrorFor(string city, string errorMessage)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.City, city);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [Theory]
        [MemberData(nameof(ValidStates))]
        public void Validate_WhenStateIsValid_ShouldNotHaveValidationErrorFor(string state)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.State, state);
        }

        [Theory]
        [MemberData(nameof(InvalidStates))]
        public void Validate_WhenStateIsInvalid_ShouldHaveValidationErrorFor(string state, string errorMessage)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.State, state);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [Theory]
        [MemberData(nameof(ValidPostalCodes))]
        public void Validate_WhenPostalCodeIsValid_ShouldNotHaveValidationErrorFor(string postalCode)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.PostalCode, postalCode);
        }

        [Theory]
        [MemberData(nameof(InvalidPostalCodes))]
        public void Validate_WhenPostalCodeIsInvalid_ShouldHaveValidationErrorFor(string postalCode, string errorMessage)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.PostalCode, postalCode);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }
    }
}
