// Filename: CreateAddressRequestValidatorTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Grpc.Protos.Identity.Enums;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Api.Application.Validators;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;

using FluentAssertions;

using FluentValidation.TestHelper;

using Xunit;

namespace eDoxa.Identity.UnitTests.Application.Validators
{
    public sealed class CreateAddressRequestValidatorTest : UnitTest
    {
        public CreateAddressRequestValidatorTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        public static TheoryData<EnumCountryIsoCode> ValidCountries =>
            new TheoryData<EnumCountryIsoCode>
            {
                EnumCountryIsoCode.CA,
                EnumCountryIsoCode.US
            };

        public static TheoryData<EnumCountryIsoCode> InvalidCountries =>
            new TheoryData<EnumCountryIsoCode>
            {
                EnumCountryIsoCode.None,
                EnumCountryIsoCode.All
            };

        public static TheoryData<EnumCountryIsoCode, string> ValidLine1Address =>
            new TheoryData<EnumCountryIsoCode, string>
            {
                {EnumCountryIsoCode.CA, "4140 Av. Kindersley, ap 13"}
            };

        public static TheoryData<EnumCountryIsoCode, string> InvalidLine1Address =>
            new TheoryData<EnumCountryIsoCode, string>
            {
                {EnumCountryIsoCode.CA, ""},
                {EnumCountryIsoCode.CA, "This_is_an_adress"}
            };

        public static TheoryData<EnumCountryIsoCode, string> ValidLine2Address =>
            new TheoryData<EnumCountryIsoCode, string>
            {
                {EnumCountryIsoCode.CA, "4140 Av. Kindersley, ap 13"}
            };

        public static TheoryData<EnumCountryIsoCode, string> InvalidLine2Address =>
            new TheoryData<EnumCountryIsoCode, string>
            {
                {EnumCountryIsoCode.CA, "This_is_an_adress"}
            };

        public static TheoryData<EnumCountryIsoCode, string> ValidCities =>
            new TheoryData<EnumCountryIsoCode, string>
            {
                {EnumCountryIsoCode.CA, "City"},
                {EnumCountryIsoCode.CA, "City-of Testing"}
            };

        public static TheoryData<EnumCountryIsoCode, string> InvalidCities =>
            new TheoryData<EnumCountryIsoCode, string>
            {
                {EnumCountryIsoCode.CA, ""},
                {EnumCountryIsoCode.CA, "123City"},
                {EnumCountryIsoCode.CA, "OK_Test"}
            };

        public static TheoryData<EnumCountryIsoCode, string> ValidStates =>
            new TheoryData<EnumCountryIsoCode, string>
            {
                {EnumCountryIsoCode.CA, "QC"},
                {EnumCountryIsoCode.CA, "ON"}
            };

        public static TheoryData<EnumCountryIsoCode, string> InvalidStates =>
            new TheoryData<EnumCountryIsoCode, string>
            {
                {EnumCountryIsoCode.CA, null},
                {EnumCountryIsoCode.CA, ""},
                {EnumCountryIsoCode.CA, "123State"},
                {EnumCountryIsoCode.CA, "OK_Test"}
            };

        public static TheoryData<EnumCountryIsoCode, string> ValidPostalCodes =>
            new TheoryData<EnumCountryIsoCode, string>
            {
                {EnumCountryIsoCode.CA, "H4P 1K8"}
            };

        public static TheoryData<EnumCountryIsoCode, string> InvalidPostalCodes =>
            new TheoryData<EnumCountryIsoCode, string>
            {
                {EnumCountryIsoCode.CA, null},
                {EnumCountryIsoCode.CA, null},
                {EnumCountryIsoCode.CA, "1234"},
                {EnumCountryIsoCode.CA, "1234.5"}
            };

        [Theory]
        [MemberData(nameof(ValidCountries))]
        public void Validate_WhenCountryIsValid_ShouldNotHaveValidationErrorFor(EnumCountryIsoCode countryIsoCode)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.CountryIsoCode, countryIsoCode);
        }

        [Theory]
        [MemberData(nameof(InvalidCountries))]
        public void Validate_WhenCountryIsInvalid_ShouldHaveValidationErrorFor(EnumCountryIsoCode countryIsoCode)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act
            validator.ShouldHaveValidationErrorFor(request => request.CountryIsoCode, countryIsoCode);
        }

        [Theory]
        [MemberData(nameof(ValidLine1Address))]
        public void Validate_WhenLine1IsValid_ShouldNotHaveValidationErrorFor(EnumCountryIsoCode countryIsoCode, string line1)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(
                request => request.Line1,
                new CreateAddressRequest
                {
                    CountryIsoCode = countryIsoCode,
                    Line1 = line1
                });
        }

        [Theory]
        [MemberData(nameof(InvalidLine1Address))]
        public void Validate_WhenLine1IsInvalid_ShouldHaveValidationErrorFor(EnumCountryIsoCode countryIsoCode, string line1)
        {
            // Arrange
            var errors = TestOptionsWrapper.Value.GetLine1ErrorsFor(countryIsoCode);
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act
            var failures = validator.ShouldHaveValidationErrorFor(
                request => request.Line1,
                new CreateAddressRequest
                {
                    CountryIsoCode = countryIsoCode,
                    Line1 = line1
                });

            // Assert
            failures.Select(failure => failure.ErrorMessage).Any(error => errors.Contains(error)).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(ValidLine2Address))]
        public void Validate_WhenLine2IsValid_ShouldNotHaveValidationErrorFor(EnumCountryIsoCode countryIsoCode, string line2)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(
                request => request.Line2,
                new CreateAddressRequest
                {
                    CountryIsoCode = countryIsoCode,
                    Line2 = line2
                });
        }

        [Theory]
        [MemberData(nameof(InvalidLine2Address))]
        public void Validate_WhenLine2IsInvalid_ShouldHaveValidationErrorFor(EnumCountryIsoCode countryIsoCode, string line2)
        {
            // Arrange
            var errors = TestOptionsWrapper.Value.GetLine2ErrorsFor(countryIsoCode);
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act
            var failures = validator.ShouldHaveValidationErrorFor(
                request => request.Line2,
                new CreateAddressRequest
                {
                    CountryIsoCode = countryIsoCode,
                    Line2 = line2
                });

            // Assert
            failures.Select(failure => failure.ErrorMessage).Any(error => errors.Contains(error)).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(ValidCities))]
        public void Validate_WhenCityIsValid_ShouldNotHaveValidationErrorFor(EnumCountryIsoCode countryIsoCode, string city)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(
                request => request.City,
                new CreateAddressRequest
                {
                    CountryIsoCode = countryIsoCode,
                    City = city
                });
        }

        [Theory]
        [MemberData(nameof(InvalidCities))]
        public void Validate_WhenCityIsInvalid_ShouldHaveValidationErrorFor(EnumCountryIsoCode countryIsoCode, string city)
        {
            // Arrange
            var errors = TestOptionsWrapper.Value.GetCityErrorsFor(countryIsoCode);
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act
            var failures = validator.ShouldHaveValidationErrorFor(
                request => request.City,
                new CreateAddressRequest
                {
                    CountryIsoCode = countryIsoCode,
                    City = city
                });

            // Assert
            failures.Select(failure => failure.ErrorMessage).Any(error => errors.Contains(error)).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(ValidStates))]
        public void Validate_WhenStateIsValid_ShouldNotHaveValidationErrorFor(EnumCountryIsoCode countryIsoCode, string state)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(
                request => request.State,
                new CreateAddressRequest
                {
                    CountryIsoCode = countryIsoCode,
                    State = state
                });
        }

        [Theory]
        [MemberData(nameof(InvalidStates))]
        public void Validate_WhenStateIsInvalid_ShouldHaveValidationErrorFor(EnumCountryIsoCode countryIsoCode, string state)
        {
            // Arrange
            var errors = TestOptionsWrapper.Value.GetStateErrorsFor(countryIsoCode);
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act
            var failures = validator.ShouldHaveValidationErrorFor(
                request => request.State,
                new CreateAddressRequest
                {
                    CountryIsoCode = countryIsoCode,
                    State = state
                });

            // Assert
            failures.Select(failure => failure.ErrorMessage).Any(error => errors.Contains(error)).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(ValidPostalCodes))]
        public void Validate_WhenPostalCodeIsValid_ShouldNotHaveValidationErrorFor(EnumCountryIsoCode countryIsoCode, string postalCode)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(
                request => request.PostalCode,
                new CreateAddressRequest
                {
                    CountryIsoCode = countryIsoCode,
                    PostalCode = postalCode
                });
        }

        [Theory]
        [MemberData(nameof(InvalidPostalCodes))]
        public void Validate_WhenPostalCodeIsInvalid_ShouldHaveValidationErrorFor(EnumCountryIsoCode countryIsoCode, string postalCode)
        {
            // Arrange
            var errors = TestOptionsWrapper.Value.GetPostalCodeErrorsFor(countryIsoCode);
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act
            var failures = validator.ShouldHaveValidationErrorFor(
                request => request.PostalCode,
                new CreateAddressRequest
                {
                    CountryIsoCode = countryIsoCode,
                    PostalCode = postalCode
                });

            // Assert
            failures.Select(failure => failure.ErrorMessage).Any(error => errors.Contains(error)).Should().BeTrue();
        }
    }
}
