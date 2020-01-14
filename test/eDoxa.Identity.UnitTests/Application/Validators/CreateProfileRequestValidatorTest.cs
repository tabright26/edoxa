// Filename: InformationsPostRequestValidatorTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Enums;
using eDoxa.Identity.Api.Application.ErrorDescribers;
using eDoxa.Identity.Api.Application.Validators;

using FluentAssertions;

using FluentValidation.TestHelper;

using Xunit;

namespace eDoxa.Identity.UnitTests.Application.Validators
{
    public sealed class CreateProfileRequestValidatorTest
    {
        public static TheoryData<string> ValidFirstNames =>
            new TheoryData<string>
            {
                "Gabriel",
                "Gabriel-Roy",
                "Gabriel-Roy-R",
                "gabriel-Roy",
                "Gabriel-roy"
            };

        public static TheoryData<string> ValidLastNames =>
            new TheoryData<string>
            {
                "Gabriel",
                "Gabriel-Roy",
                "Gabriel-Roy-R",
                "gabriel-Roy",
                "Gabriel-roy"
            };

        public static TheoryData<string, string> InvalidFirstNames =>
            new TheoryData<string, string>
            {
                {"", ProfileErrorDescriber.FirstNameRequired()},
                {"G", ProfileErrorDescriber.FirstNameLength()},
                {"Gabriel-Roy-Gab-R", ProfileErrorDescriber.FirstNameLength()},
                {"Gab123", ProfileErrorDescriber.FirstNameInvalid()},
                {"Gabriel-Ro_Roy", ProfileErrorDescriber.FirstNameInvalid()}
            };

        public static TheoryData<string, string> InvalidLastNames =>
            new TheoryData<string, string>
            {
                {"", ProfileErrorDescriber.LastNameRequired()},
                {"G", ProfileErrorDescriber.LastNameLength()},
                {"Gabriel-Roy-Gab-R", ProfileErrorDescriber.LastNameLength()},
                {"Gab123", ProfileErrorDescriber.LastNameInvalid()},
                {"Gabriel-Ro_Roy", ProfileErrorDescriber.LastNameInvalid()}
            };

        public static TheoryData<EnumGender> ValidGenders =>
            new TheoryData<EnumGender>
            {
                EnumGender.Male,
                EnumGender.Female,
                EnumGender.Other
            };

        public static TheoryData<EnumGender, string> InvalidGenders =>
            new TheoryData<EnumGender, string>
            {
                {EnumGender.None, ProfileErrorDescriber.GenderRequired()}
            };

        public static TheoryData<DobDto> ValidDob =>
            new TheoryData<DobDto>
            {
                new DobDto
                {
                    Year = 1995,
                    Month = 8,
                    Day = 4
                }
            };

        //public static TheoryData<DobRequest, string> InvalidDob
        //{
        //    get
        //    {
        //        var dob = new DateTime();

        //        return new TheoryData<DobRequest, string>
        //        {
        //            {new DobRequest(dob.Year, dob.Month, dob.Day), InformationsErrorDescriber.BirthDateInvalid()}
        //        };
        //    }
        //}

        [Theory]
        [MemberData(nameof(ValidFirstNames))]
        public void Validate_WhenFirstNameIsValid_ShouldNotHaveValidationErrorFor(string firstName)
        {
            // Arrange
            var validator = new CreateProfileRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.FirstName, firstName);
        }

        [Theory]
        [MemberData(nameof(InvalidFirstNames))]
        public void Validate_WhenFirstNameIsInvalid_ShouldHaveValidationErrorFor(string firstName, string errorMessage)
        {
            // Arrange
            var validator = new CreateProfileRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.FirstName, firstName);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [Theory]
        [MemberData(nameof(ValidLastNames))]
        public void Validate_WhenLastNameIsValid_ShouldNotHaveValidationErrorFor(string lastName)
        {
            // Arrange
            var validator = new CreateProfileRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.LastName, lastName);
        }

        [Theory]
        [MemberData(nameof(InvalidLastNames))]
        public void Validate_WhenLastNameIsInvalid_ShouldHaveValidationErrorFor(string lastName, string errorMessage)
        {
            // Arrange
            var validator = new CreateProfileRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.LastName, lastName);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [Theory]
        [MemberData(nameof(ValidGenders))]
        public void Validate_WhenGenderIsValid_ShouldNotHaveValidationErrorFor(EnumGender gender)
        {
            // Arrange
            var validator = new CreateProfileRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Gender, gender);
        }

        [Theory]
        [MemberData(nameof(InvalidGenders))]
        public void Validate_WhenGenderIsInvalid_ShouldHaveValidationErrorFor(EnumGender gender, string errorMessage)
        {
            // Arrange
            var validator = new CreateProfileRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Gender, gender);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [Theory]
        [MemberData(nameof(ValidDob))]
        public void Validate_WhenDobIsValid_ShouldNotHaveValidationErrorFor(DobDto dob)
        {
            // Arrange
            var validator = new CreateProfileRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Dob, dob);
        }

        //[Theory]
        //[MemberData(nameof(InvalidDob))]
        //public void Validate_WhenDobIsInvalid_ShouldHaveValidationErrorFor(DobRequest dob, string errorMessage)
        //{
        //    // Arrange
        //    var validator = new InformationsPostRequestValidator();

        //    // Act
        //    var failures = validator.ShouldHaveValidationErrorFor(request => request.Dob, dob);

        //    // Assert
        //    failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        //}
    }
}
