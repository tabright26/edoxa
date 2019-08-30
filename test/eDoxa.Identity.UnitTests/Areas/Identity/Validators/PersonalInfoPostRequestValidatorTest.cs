// Filename: PersonalInfoPostRequestValidatorTest.cs
// Date Created: 2019-08-22
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Bogus.DataSets;

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
        [DataRow("Gabriel")]
        [DataRow("Gabriel-Roy")]
        [DataRow("Gabriel-Roy-R")]
        public void Validate_WhenFirstNameIsValid_ShouldNotHaveValidationErrorFor(string firstName)
        {
            // Arrange
            var validator = new PersonalInfoPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.FirstName, firstName);
        }

        [DataTestMethod]
        [DataRow(null, "First name is required")]
        [DataRow("", "First name is required")]
        [DataRow("G", "First name must be between 2 and 16 characters long")]
        [DataRow("Gabriel-Roy-Gab-R", "First name must be between 2 and 16 characters long")]
        [DataRow("Gab123", "First name invalid. Only letters and hyphens allowed")]
        [DataRow("Gabriel-Ro_Roy", "First name invalid. Only letters and hyphens allowed")]
        [DataRow("gabriel-Roy", "First name invalid. Every part must start with an uppercase")]
        [DataRow("Gabriel-roy", "First name invalid. Every part must start with an uppercase")]
        public void Validate_WhenFirstNameIsInvalid_ShouldHaveValidationErrorFor(string firstName, string errorMessage)
        {
            // Arrange
            var validator = new PersonalInfoPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.FirstName, firstName);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [DataTestMethod]
        [DataRow("Gabriel")]
        [DataRow("Gabriel-Roy")]
        [DataRow("Gabriel-Roy-R")]
        public void Validate_WhenLastNameIsValid_ShouldNotHaveValidationErrorFor(string lastName)
        {
            // Arrange
            var validator = new PersonalInfoPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.LastName, lastName);
        }

        [DataTestMethod]
        [DataRow(null, "Last name is required")]
        [DataRow("", "Last name is required")]
        [DataRow("G", "Last name must be between 2 and 16 characters long")]
        [DataRow("Gabriel-Roy-Gab-R", "Last name must be between 2 and 16 characters long")]
        [DataRow("Gab123", "Last name invalid. Only letters and hyphens allowed")]
        [DataRow("Gabriel-Ro_Roy", "Last name invalid. Only letters and hyphens allowed")]
        [DataRow("gabriel-Roy", "Last name invalid. Every part must start with an uppercase")]
        [DataRow("Gabriel-roy", "Last name invalid. Every part must start with an uppercase")]
        public void Validate_WhenLastNameIsInvalid_ShouldHaveValidationErrorFor(string lastName, string errorMessage)
        {
            // Arrange
            var validator = new PersonalInfoPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.LastName, lastName);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        /*[DataTestMethod]
        [DataRow("Male")]
        [DataRow("Female")]
        [DataRow("Others")]
        public void Validate_WhenGenderIsValid_ShouldNotHaveValidationErrorFor(string gender)
        {
            // Arrange
            var validator = new PersonalInfoPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Gender, gender);
        }

        [DataTestMethod]
        [DataRow(null, "Gender is required")]
        [DataRow("", "Gender is required")]
        public void Validate_WhenGenderIsInvalid_ShouldHaveValidationErrorFor(string gender, string errorMessage)
        {
            // Arrange
            var validator = new PersonalInfoPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Gender, gender);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [DataTestMethod]
        [DataRow("04/08/1995")]
        public void Validate_WhenBirthDateIsValid_ShouldNotHaveValidationErrorFor(string birthDate)
        {
            // Arrange
            var validator = new PersonalInfoPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.BirthDate, birthDate);
        }*/

        /*[DataTestMethod]
        [DataRow(null, "Birth date is required")]
        [DataRow("", "Birth date is required")]
        [DataRow("1995/00/00", "Birth date invalid")]
        public void Validate_WhenBirthDateIsInvalid_ShouldHaveValidationErrorFor(DateTime birthDate, string errorMessage)
        {
            // Arrange
            var validator = new PersonalInfoPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.BirthDate, birthDate);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }*/

    }
}
