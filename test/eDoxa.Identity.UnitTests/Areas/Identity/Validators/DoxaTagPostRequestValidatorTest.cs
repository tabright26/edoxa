// Filename: DoxaTagPostRequestValidatorTest.cs
// Date Created: 2019-08-22
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
    public sealed class DoxaTagPostRequestValidatorTest
    {
        [DataTestMethod]
        [DynamicData(nameof(ValidDoxaTags), DynamicDataSourceType.Method)]
        public void Validate_WhenNameIsValid_ShouldNotHaveValidationErrorFor(string name)
        {
            // Arrange
            var validator = new DoxaTagPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Name, name);
        }

        private static IEnumerable<object[]> ValidDoxaTags()
        {
            yield return new object[] { "DoxaTagName" };
            yield return new object[] { "Doxa_Tag_Name" };
            yield return new object[] { "aaaaaaaaaaaaaaaa" };
        }

        [DataTestMethod]
        [DynamicData(nameof(InvalidDoxaTags), DynamicDataSourceType.Method)]
        public void Validate_WhenNameIsInvalid_ShouldHaveValidationErrorFor(string name, string errorMessage)
        {
            // Arrange
            var validator = new DoxaTagPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Name, name);

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        private static IEnumerable<object[]> InvalidDoxaTags()
        {
            yield return new object[] { null, DoxaTagErrorDescriber.Required() };
            yield return new object[] { "", DoxaTagErrorDescriber.Required() };
            yield return new object[] { "D", DoxaTagErrorDescriber.Length() };
            yield return new object[] { "aaaaaaaaaaaaaaaaa", DoxaTagErrorDescriber.Length() };
            yield return new object[] { "@DoxaTagName", DoxaTagErrorDescriber.Invalid() };
            yield return new object[] { "DoxaTagName1", DoxaTagErrorDescriber.Invalid() };
            yield return new object[] { "_DoxaTagName", DoxaTagErrorDescriber.InvalidUnderscore() };
            yield return new object[] { "DoxaTagName_", DoxaTagErrorDescriber.InvalidUnderscore() };
        }
    }
}
