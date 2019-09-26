// Filename: AccountDepositPostRequestValidatorTest.cs
// Date Created: 2019-08-28
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.using System.Collections.Generic;
using System.Collections.Generic;

using eDoxa.Organizations.Clans.Api.Areas.Clans.ErrorDescribers;
using eDoxa.Organizations.Clans.Api.Areas.Clans.Validators;

using FluentAssertions;

using FluentValidation.TestHelper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Organizations.Clans.UnitTests.Areas.Clans.Validators
{
    [TestClass]
    public sealed class ClanPostRequestValidatorTest
    {
        [DataTestMethod]
        [DynamicData(nameof(ValidNames), DynamicDataSourceType.Method)]
        public void Validate_WhenNameIsValid_ShouldNotHaveValidationErrorFor(string name)
        {
            // Arrange
            var validator = new ClanPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Name, name);
        }

        private static IEnumerable<object[]> ValidNames()
        {
            yield return new object[] { "MagicPotato" };
            yield return new object[] { "PrettyLasagna" };
        }

        [DataTestMethod]
        [DynamicData(nameof(InvalidNames), DynamicDataSourceType.Method)]
        public void Validate_WhenNameIsInvalid_ShouldHaveValidationErrorFor(string name, string errorMessage)
        {
            // Arrange
            var validator = new ClanPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Name, name);

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        private static IEnumerable<object[]> InvalidNames()
        {
            yield return new object[] { null, ClanErrorDescriber.NameRequired() };
            yield return new object[] {"", ClanErrorDescriber.NameRequired()};
            yield return new object[] {"Ba", ClanErrorDescriber.NameLength()};
            yield return new object[] {"Ba!", ClanErrorDescriber.NameInvalid()};
        }

        [DataTestMethod]
        [DynamicData(nameof(ValidSummaries), DynamicDataSourceType.Method)]
        public void Validate_WhenSummaryIsValid_ShouldNotHaveValidationErrorFor(string summary)
        {
            // Arrange
            var validator = new ClanPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Summary, summary);
        }

        private static IEnumerable<object[]> ValidSummaries()
        {
            yield return new object[] { "Pretty good clan." };
            yield return new object[] { "This-is-a,clan." };
        }

        [DataTestMethod]
        [DynamicData(nameof(InvalidSummaries), DynamicDataSourceType.Method)]
        public void Validate_WhenSummaryIsInvalid_ShouldHaveValidationErrorFor(string summary, string errorMessage)
        {
            // Arrange
            var validator = new ClanPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Summary, summary);

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        private static IEnumerable<object[]> InvalidSummaries()
        {
            yield return new object[] { "Pretty", ClanErrorDescriber.SummaryInvalid()};
            yield return new object[] { "This-is-not a ! Clan", ClanErrorDescriber.SummaryInvalid() };
        }
    }
}
