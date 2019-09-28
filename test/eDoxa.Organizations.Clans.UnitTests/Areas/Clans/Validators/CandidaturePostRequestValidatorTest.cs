// Filename: AccountDepositPostRequestValidatorTest.cs
// Date Created: 2019-08-28
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Organizations.Clans.Api.Areas.Clans.ErrorDescribers;
using eDoxa.Organizations.Clans.Api.Areas.Clans.Validators;
using eDoxa.Organizations.Clans.Domain.Models;

using FluentAssertions;

using FluentValidation.TestHelper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Organizations.Clans.UnitTests.Areas.Clans.Validators
{
    [TestClass]
    public sealed class CandidaturePostRequestValidatorTest
    {
        [DataTestMethod]
        [DynamicData(nameof(ValidClanId), DynamicDataSourceType.Method)]
        public void Validate_WhenClanIdIsValid_ShouldNotHaveValidationErrorFor(ClanId clanId)
        {
            // Arrange
            var validator = new CandidaturePostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.ClanId, clanId);
        }

        private static IEnumerable<object[]> ValidClanId()
        {
            yield return new object[] {new ClanId()};
        }

        [DataTestMethod]
        [DynamicData(nameof(InvalidClanIds), DynamicDataSourceType.Method)]
        public void Validate_WhenClanIdIsInvalid_ShouldHaveValidationErrorFor(ClanId clanId, string errorMessage)
        {
            // Arrange
            var validator = new CandidaturePostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.ClanId, clanId);

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        private static IEnumerable<object[]> InvalidClanIds()
        {
            yield return new object[] {null, CandidatureErrorDescriber.ClanIdRequired() };
        }
    }
}
