// Filename: PersonalInfoPostRequestValidatorTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Identity.Api.Areas.Identity.ErrorDescribers;
using eDoxa.Identity.Api.Areas.Identity.Validators;
using eDoxa.Seedwork.Domain.Miscellaneous;

using FluentAssertions;

using FluentValidation.TestHelper;

using Xunit;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Validators
{
    public sealed class PersonalInfoPostRequestValidatorTest
    {
        public static TheoryData<string> ValidFirstNames =>
            new TheoryData<string>
            {
                "Gabriel",
                "Gabriel-Roy",
                "Gabriel-Roy-R"
            };

        public static TheoryData<string> ValidLastNames =>
            new TheoryData<string>
            {
                "Gabriel",
                "Gabriel-Roy",
                "Gabriel-Roy-R"
            };

        public static TheoryData<string, string> InvalidFirstNames =>
            new TheoryData<string, string>
            {
                {null, PersonalInfoErrorDescriber.FirstNameRequired()},
                {"", PersonalInfoErrorDescriber.FirstNameRequired()},
                {"G", PersonalInfoErrorDescriber.FirstNameLength()},
                {"Gabriel-Roy-Gab-R", PersonalInfoErrorDescriber.FirstNameLength()},
                {"Gab123", PersonalInfoErrorDescriber.FirstNameInvalid()},
                {"Gabriel-Ro_Roy", PersonalInfoErrorDescriber.FirstNameInvalid()},
                {"gabriel-Roy", PersonalInfoErrorDescriber.FirstNameUppercase()},
                {"Gabriel-roy", PersonalInfoErrorDescriber.FirstNameUppercase()}
            };

        public static TheoryData<string, string> InvalidLastNames =>
            new TheoryData<string, string>
            {
                {null, PersonalInfoErrorDescriber.LastNameRequired()},
                {"", PersonalInfoErrorDescriber.LastNameRequired()},
                {"G", PersonalInfoErrorDescriber.LastNameLength()},
                {"Gabriel-Roy-Gab-R", PersonalInfoErrorDescriber.LastNameLength()},
                {"Gab123", PersonalInfoErrorDescriber.LastNameInvalid()},
                {"Gabriel-Ro_Roy", PersonalInfoErrorDescriber.LastNameInvalid()},
                {"gabriel-Roy", PersonalInfoErrorDescriber.LastNameUppercase()},
                {"Gabriel-roy", PersonalInfoErrorDescriber.LastNameUppercase()}
            };

        public static TheoryData<Gender> ValidGenders =>
            new TheoryData<Gender>
            {
                Gender.Male,
                Gender.Female,
                Gender.Other
            };

        public static TheoryData<Gender, string> InvalidGenders =>
            new TheoryData<Gender, string>
            {
                {null, PersonalInfoErrorDescriber.GenderRequired()}
            };

        public static TheoryData<DateTime> ValidBirthDates =>
            new TheoryData<DateTime>
            {
                new DateTime(1995, 08, 04)
            };

        public static TheoryData<DateTime?, string> InvalidBirthDates =>
            new TheoryData<DateTime?, string>
            {
                {null, PersonalInfoErrorDescriber.BirthDateRequired()},
                {new DateTime(), PersonalInfoErrorDescriber.BirthDateRequired()}
            };

        [Theory]
        [MemberData(nameof(ValidFirstNames))]
        public void Validate_WhenFirstNameIsValid_ShouldNotHaveValidationErrorFor(string firstName)
        {
            // Arrange
            var validator = new PersonalInfoPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.FirstName, firstName);
        }

        [Theory]
        [MemberData(nameof(InvalidFirstNames))]
        public void Validate_WhenFirstNameIsInvalid_ShouldHaveValidationErrorFor(string firstName, string errorMessage)
        {
            // Arrange
            var validator = new PersonalInfoPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.FirstName, firstName);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [Theory]
        [MemberData(nameof(ValidLastNames))]
        public void Validate_WhenLastNameIsValid_ShouldNotHaveValidationErrorFor(string lastName)
        {
            // Arrange
            var validator = new PersonalInfoPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.LastName, lastName);
        }

        [Theory]
        [MemberData(nameof(InvalidLastNames))]
        public void Validate_WhenLastNameIsInvalid_ShouldHaveValidationErrorFor(string lastName, string errorMessage)
        {
            // Arrange
            var validator = new PersonalInfoPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.LastName, lastName);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [Theory]
        [MemberData(nameof(ValidGenders))]
        public void Validate_WhenGenderIsValid_ShouldNotHaveValidationErrorFor(Gender gender)
        {
            // Arrange
            var validator = new PersonalInfoPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Gender, gender);
        }

        [Theory]
        [MemberData(nameof(InvalidGenders))]
        public void Validate_WhenGenderIsInvalid_ShouldHaveValidationErrorFor(Gender gender, string errorMessage)
        {
            // Arrange
            var validator = new PersonalInfoPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Gender, gender);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [Theory]
        [MemberData(nameof(ValidBirthDates))]
        public void Validate_WhenBirthDateIsValid_ShouldNotHaveValidationErrorFor(DateTime birthDate)
        {
            // Arrange
            var validator = new PersonalInfoPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.BirthDate, birthDate);
        }

        [Theory]
        [MemberData(nameof(InvalidBirthDates))]
        public void Validate_WhenBirthDateIsInvalid_ShouldHaveValidationErrorFor(DateTime birthDate, string errorMessage)
        {
            //Arrange
            var validator = new PersonalInfoPostRequestValidator();

            //Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.BirthDate, birthDate);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }
    }
}
