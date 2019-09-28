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
    public sealed class InvitationPostRequestValidatorTest
    {
        [DataTestMethod]
        [DynamicData(nameof(ValidUserId), DynamicDataSourceType.Method)]
        public void Validate_WhenUserIdIsValid_ShouldNotHaveValidationErrorFor(UserId userId)
        {
            // Arrange
            var validator = new InvitationPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.UserId, userId);
        }

        private static IEnumerable<object[]> ValidUserId()
        {
            yield return new object[] {new UserId()};
        }

        [DataTestMethod]
        [DynamicData(nameof(InvalidUserIds), DynamicDataSourceType.Method)]
        public void Validate_WhenUserIdIsInvalid_ShouldHaveValidationErrorFor(UserId userId, string errorMessage)
        {
            // Arrange
            var validator = new InvitationPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.UserId, userId);

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        private static IEnumerable<object[]> InvalidUserIds()
        {
            yield return new object[] {null, InvitationErrorDescriber.UserIdRequired() };
        }

        [DataTestMethod]
        [DynamicData(nameof(ValidClanId), DynamicDataSourceType.Method)]
        public void Validate_WhenClanIdIsValid_ShouldNotHaveValidationErrorFor(ClanId clanId)
        {
            // Arrange
            var validator = new InvitationPostRequestValidator();

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
            var validator = new InvitationPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.ClanId, clanId);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        private static IEnumerable<object[]> InvalidClanIds()
        {
            yield return new object[] {null, InvitationErrorDescriber.ClanIdRequired() };
        }
    }
}
