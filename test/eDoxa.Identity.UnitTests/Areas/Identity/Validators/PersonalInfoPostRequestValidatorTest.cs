// Filename: PersonalInfoPostRequestValidatorTest.cs
// Date Created: 2019-08-22
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using Bogus.DataSets;

using eDoxa.Identity.Api.Areas.Identity.ErrorDescribers;
using eDoxa.Identity.Api.Areas.Identity.Validators;
using eDoxa.Identity.Api.Infrastructure.Models;

using FluentAssertions;
using FluentAssertions.Primitives;

using FluentValidation.TestHelper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Validators
{
    [TestClass]
    public sealed class PersonalInfoPostRequestValidatorTest
    {
        [DataTestMethod]
        [DynamicData(nameof(ValidFirstNames), DynamicDataSourceType.Method)]
        public void Validate_WhenFirstNameIsValid_ShouldNotHaveValidationErrorFor(string firstName)
        {
            // Arrange
            var validator = new PersonalInfoPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.FirstName, firstName);
        }

        private static IEnumerable<object[]> ValidFirstNames()
        {
            yield return new object[] { "Gabriel" };
            yield return new object[] { "Gabriel-Roy" };
            yield return new object[] { "Gabriel-Roy-R" };
        }

        [DataTestMethod]
        [DynamicData(nameof(InvalidFirstNames), DynamicDataSourceType.Method)]
        public void Validate_WhenFirstNameIsInvalid_ShouldHaveValidationErrorFor(string firstName, string errorMessage)
        {
            // Arrange
            var validator = new PersonalInfoPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.FirstName, firstName);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        private static IEnumerable<object[]> InvalidFirstNames()
        {
            yield return new object[] { null, PersonalInfoErrorDescriber.FirstNameRequired() };
            yield return new object[] { "", PersonalInfoErrorDescriber.FirstNameRequired() };
            yield return new object[] { "G", PersonalInfoErrorDescriber.FirstNameLength() };
            yield return new object[] { "Gabriel-Roy-Gab-R", PersonalInfoErrorDescriber.FirstNameLength() };
            yield return new object[] { "Gab123", PersonalInfoErrorDescriber.FirstNameInvalid() };
            yield return new object[] { "Gabriel-Ro_Roy", PersonalInfoErrorDescriber.FirstNameInvalid() };
            yield return new object[] { "gabriel-Roy", PersonalInfoErrorDescriber.FirstNameUppercase() };
            yield return new object[] { "Gabriel-roy", PersonalInfoErrorDescriber.FirstNameUppercase() };
        }

        [DataTestMethod]
        [DynamicData(nameof(ValidLastNames), DynamicDataSourceType.Method)]
        public void Validate_WhenLastNameIsValid_ShouldNotHaveValidationErrorFor(string lastName)
        {
            // Arrange
            var validator = new PersonalInfoPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.LastName, lastName);
        }

        private static IEnumerable<object[]> ValidLastNames()
        {
            yield return new object[] { "Gabriel" };
            yield return new object[] { "Gabriel-Roy" };
            yield return new object[] { "Gabriel-Roy-R" };
        }

        [DataTestMethod]
        [DynamicData(nameof(InvalidLastNames), DynamicDataSourceType.Method)]
        public void Validate_WhenLastNameIsInvalid_ShouldHaveValidationErrorFor(string lastName, string errorMessage)
        {
            // Arrange
            var validator = new PersonalInfoPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.LastName, lastName);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        private static IEnumerable<object[]> InvalidLastNames()
        {
            yield return new object[] { null, PersonalInfoErrorDescriber.LastNameRequired() };
            yield return new object[] { "", PersonalInfoErrorDescriber.LastNameRequired() };
            yield return new object[] { "G", PersonalInfoErrorDescriber.LastNameLength() };
            yield return new object[] { "Gabriel-Roy-Gab-R", PersonalInfoErrorDescriber.LastNameLength() };
            yield return new object[] { "Gab123", PersonalInfoErrorDescriber.LastNameInvalid() };
            yield return new object[] { "Gabriel-Ro_Roy", PersonalInfoErrorDescriber.LastNameInvalid() };
            yield return new object[] { "gabriel-Roy", PersonalInfoErrorDescriber.LastNameUppercase() };
            yield return new object[] { "Gabriel-roy", PersonalInfoErrorDescriber.LastNameUppercase() };
        }

        [DataTestMethod]
        [DynamicData(nameof(ValidGenders), DynamicDataSourceType.Method)]
        public void Validate_WhenGenderIsValid_ShouldNotHaveValidationErrorFor(Gender gender)
        {
            // Arrange
            var validator = new PersonalInfoPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Gender, gender);
        }

        private static IEnumerable<object[]> ValidGenders()
        {
            yield return new object[] { Gender.Male };
            yield return new object[] { Gender.Female };
            yield return new object[] { Gender.Other };
        }

        [DataTestMethod]
        [DynamicData(nameof(InvalidGenders), DynamicDataSourceType.Method)]
        public void Validate_WhenGenderIsInvalid_ShouldHaveValidationErrorFor(Gender gender, string errorMessage)
        {
            // Arrange
            var validator = new PersonalInfoPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Gender, gender);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }


        private static IEnumerable<object[]> InvalidGenders()
        {
            yield return new object[] { null, PersonalInfoErrorDescriber.GenderRequired() };
        }

        [DataTestMethod]
        [DynamicData(nameof(ValidBirthDates), DynamicDataSourceType.Method)]
        public void Validate_WhenBirthDateIsValid_ShouldNotHaveValidationErrorFor(DateTime birthDate)
        {
            // Arrange
            var validator = new PersonalInfoPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.BirthDate, birthDate);
        }

        private static IEnumerable<object[]> ValidBirthDates()
        {
            yield return new object[] { new DateTime(1995,08,04) };
        }

        [DataTestMethod]
        [DynamicData(nameof(InvalidBirthDates), DynamicDataSourceType.Method)]
        public void Validate_WhenBirthDateIsInvalid_ShouldHaveValidationErrorFor(DateTime birthDate, string errorMessage)
        {
            //Arrange
            var validator = new PersonalInfoPostRequestValidator();

            //Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.BirthDate, birthDate);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        private static IEnumerable<object[]> InvalidBirthDates()
        {
            yield return new object[] { null, PersonalInfoErrorDescriber.BirthDateRequired() };
            yield return new object[] { new DateTime(), PersonalInfoErrorDescriber.BirthDateRequired() };
        }

    }
}
