// Filename: PersonalInfoPutRequestValidatorTest.cs
// Date Created: 2019-08-22
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Identity.Api.Areas.Identity.Validators;

using FluentAssertions;

using FluentValidation.TestHelper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Validators
{
    [TestClass]
    public sealed class PersonalInfoPutRequestValidatorTest
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
            yield return new object[] { null, "First name is required" };
            yield return new object[] { "", "First name is required" };
            yield return new object[] { "G", "First name must be between 2 and 16 characters long" };
            yield return new object[] { "Gabriel-Roy-Gab-R", "First name must be between 2 and 16 characters long" };
            yield return new object[] { "Gab123", "First name invalid. Only letters and hyphens allowed" };
            yield return new object[] { "Gabriel-Ro_Roy", "First name invalid. Only letters and hyphens allowed" };
            yield return new object[] { "gabriel-Roy", "First name invalid. Every part must start with an uppercase" };
            yield return new object[] { "Gabriel-roy", "First name invalid. Every part must start with an uppercase" };
        }
    }
}
