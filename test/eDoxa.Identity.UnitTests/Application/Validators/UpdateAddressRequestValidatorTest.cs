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

        public static TheoryData<EnumCountry> ValidCountries =>
            new TheoryData<EnumCountry>
            {
                EnumCountry.CA,
                EnumCountry.US
            };

        public static TheoryData<EnumCountry> InvalidCountries =>
            new TheoryData<EnumCountry>
            {
                EnumCountry.None,
                EnumCountry.All
            };

        public static TheoryData<EnumCountry, string> ValidLine1Address =>
            new TheoryData<EnumCountry, string>
            {
                {EnumCountry.CA, "4140 Av. Kindersley, ap 13"}
            };

        public static TheoryData<EnumCountry, string> InvalidLine1Address =>
            new TheoryData<EnumCountry, string>
            {
                {EnumCountry.CA, ""},
                {EnumCountry.CA, "This_is_an_adress"}
            };

        public static TheoryData<EnumCountry, string> ValidLine2Address =>
            new TheoryData<EnumCountry, string>
            {
                {EnumCountry.CA, "4140 Av. Kindersley, ap 13"}
            };

        public static TheoryData<EnumCountry, string> InvalidLine2Address =>
            new TheoryData<EnumCountry, string>
            {
                {EnumCountry.CA, "This_is_an_adress"}
            };

        public static TheoryData<EnumCountry, string> ValidCities =>
            new TheoryData<EnumCountry, string>
            {
                {EnumCountry.CA, "City"},
                {EnumCountry.CA, "City-of Testing"}
            };

        public static TheoryData<EnumCountry, string> InvalidCities =>
            new TheoryData<EnumCountry, string>
            {
                {EnumCountry.CA, ""},
                {EnumCountry.CA, "123City"},
                {EnumCountry.CA, "OK_Test"}
            };

        public static TheoryData<EnumCountry, string> ValidStates =>
            new TheoryData<EnumCountry, string>
            {
                {EnumCountry.CA, "State"},
                {EnumCountry.CA, "State-of Testing"}
            };

        public static TheoryData<EnumCountry, string> InvalidStates =>
            new TheoryData<EnumCountry, string>
            {
                {EnumCountry.CA, null},
                {EnumCountry.CA, ""},
                {EnumCountry.CA, "123State"},
                {EnumCountry.CA, "OK_Test"}
            };

        public static TheoryData<EnumCountry, string> ValidPostalCodes =>
            new TheoryData<EnumCountry, string>
            {
                {EnumCountry.CA, "H4P 1K8"}
            };

        public static TheoryData<EnumCountry, string> InvalidPostalCodes =>
            new TheoryData<EnumCountry, string>
            {
                {EnumCountry.CA, null},
                {EnumCountry.CA, null},
                {EnumCountry.CA, "1234"},
                {EnumCountry.CA, "1234.5"}
            };

        [Theory]
        [MemberData(nameof(ValidCountries))]
        public void Validate_WhenCountryIsValid_ShouldNotHaveValidationErrorFor(EnumCountry country)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Country, country);
        }

        [Theory]
        [MemberData(nameof(InvalidCountries))]
        public void Validate_WhenCountryIsInvalid_ShouldHaveValidationErrorFor(EnumCountry country)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act
            validator.ShouldHaveValidationErrorFor(request => request.Country, country);
        }

        [Theory]
        [MemberData(nameof(ValidLine1Address))]
        public void Validate_WhenLine1IsValid_ShouldNotHaveValidationErrorFor(EnumCountry country, string line1)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(
                request => request.Line1,
                new CreateAddressRequest
                {
                    Country = country,
                    Line1 = line1
                });
        }

        [Theory]
        [MemberData(nameof(InvalidLine1Address))]
        public void Validate_WhenLine1IsInvalid_ShouldHaveValidationErrorFor(EnumCountry country, string line1)
        {
            // Arrange
            var errors = TestOptionsWrapper.Value.GetLine1ErrorsFor(country);
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act
            var failures = validator.ShouldHaveValidationErrorFor(
                request => request.Line1,
                new CreateAddressRequest
                {
                    Country = country,
                    Line1 = line1
                });

            // Assert
            failures.Select(failure => failure.ErrorMessage).Any(error => errors.Contains(error)).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(ValidLine2Address))]
        public void Validate_WhenLine2IsValid_ShouldNotHaveValidationErrorFor(EnumCountry country, string line2)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(
                request => request.Line2,
                new CreateAddressRequest
                {
                    Country = country,
                    Line2 = line2
                });
        }

        [Theory]
        [MemberData(nameof(InvalidLine2Address))]
        public void Validate_WhenLine2IsInvalid_ShouldHaveValidationErrorFor(EnumCountry country, string line2)
        {
            // Arrange
            var errors = TestOptionsWrapper.Value.GetLine2ErrorsFor(country);
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act
            var failures = validator.ShouldHaveValidationErrorFor(
                request => request.Line2,
                new CreateAddressRequest
                {
                    Country = country,
                    Line2 = line2
                });

            // Assert
            failures.Select(failure => failure.ErrorMessage).Any(error => errors.Contains(error)).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(ValidCities))]
        public void Validate_WhenCityIsValid_ShouldNotHaveValidationErrorFor(EnumCountry country, string city)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(
                request => request.City,
                new CreateAddressRequest
                {
                    Country = country,
                    City = city
                });
        }

        [Theory]
        [MemberData(nameof(InvalidCities))]
        public void Validate_WhenCityIsInvalid_ShouldHaveValidationErrorFor(EnumCountry country, string city)
        {
            // Arrange
            var errors = TestOptionsWrapper.Value.GetCityErrorsFor(country);
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act
            var failures = validator.ShouldHaveValidationErrorFor(
                request => request.City,
                new CreateAddressRequest
                {
                    Country = country,
                    City = city
                });

            // Assert
            failures.Select(failure => failure.ErrorMessage).Any(error => errors.Contains(error)).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(ValidStates))]
        public void Validate_WhenStateIsValid_ShouldNotHaveValidationErrorFor(EnumCountry country, string state)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(
                request => request.State,
                new CreateAddressRequest
                {
                    Country = country,
                    State = state
                });
        }

        [Theory]
        [MemberData(nameof(InvalidStates))]
        public void Validate_WhenStateIsInvalid_ShouldHaveValidationErrorFor(EnumCountry country, string state)
        {
            // Arrange
            var errors = TestOptionsWrapper.Value.GetStateErrorsFor(country);
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act
            var failures = validator.ShouldHaveValidationErrorFor(
                request => request.State,
                new CreateAddressRequest
                {
                    Country = country,
                    State = state
                });

            // Assert
            failures.Select(failure => failure.ErrorMessage).Any(error => errors.Contains(error)).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(ValidPostalCodes))]
        public void Validate_WhenPostalCodeIsValid_ShouldNotHaveValidationErrorFor(EnumCountry country, string postalCode)
        {
            // Arrange
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(
                request => request.PostalCode,
                new CreateAddressRequest
                {
                    Country = country,
                    PostalCode = postalCode
                });
        }

        [Theory]
        [MemberData(nameof(InvalidPostalCodes))]
        public void Validate_WhenPostalCodeIsInvalid_ShouldHaveValidationErrorFor(EnumCountry country, string postalCode)
        {
            // Arrange
            var errors = TestOptionsWrapper.Value.GetPostalCodeErrorsFor(country);
            var validator = new CreateAddressRequestValidator(TestOptionsWrapper);

            // Act
            var failures = validator.ShouldHaveValidationErrorFor(
                request => request.PostalCode,
                new CreateAddressRequest
                {
                    Country = country,
                    PostalCode = postalCode
                });

            // Assert
            failures.Select(failure => failure.ErrorMessage).Any(error => errors.Contains(error)).Should().BeTrue();
        }
    }
}
