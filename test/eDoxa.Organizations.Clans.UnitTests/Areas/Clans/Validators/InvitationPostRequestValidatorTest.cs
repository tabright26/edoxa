// Filename: InvitationPostRequestValidatorTest.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Organizations.Clans.Api.Areas.Clans.ErrorDescribers;
using eDoxa.Organizations.Clans.Api.Areas.Clans.Validators;
using eDoxa.Organizations.Clans.TestHelpers;
using eDoxa.Organizations.Clans.TestHelpers.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using FluentValidation.TestHelper;

using Xunit;

namespace eDoxa.Organizations.Clans.UnitTests.Areas.Clans.Validators
{
    public sealed class InvitationPostRequestValidatorTest : UnitTest
    {
        public InvitationPostRequestValidatorTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        public static TheoryData<UserId> ValidUserId =>
            new TheoryData<UserId>
            {
                new UserId()
            };

        public static TheoryData<UserId, string> InvalidUserIds =>
            new TheoryData<UserId, string>
            {
                {null, InvitationErrorDescriber.UserIdRequired()}
            };

        public static TheoryData<ClanId> ValidClanId =>
            new TheoryData<ClanId>
            {
                new ClanId()
            };

        public static TheoryData<ClanId, string> InvalidClanIds =>
            new TheoryData<ClanId, string>
            {
                {null, InvitationErrorDescriber.ClanIdRequired()}
            };

        [Theory]
        [MemberData(nameof(ValidUserId))]
        public void Validate_WhenUserIdIsValid_ShouldNotHaveValidationErrorFor(UserId userId)
        {
            // Arrange
            var validator = new InvitationPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.UserId, userId);
        }

        [Theory]
        [MemberData(nameof(InvalidUserIds))]
        public void Validate_WhenUserIdIsInvalid_ShouldHaveValidationErrorFor(UserId userId, string errorMessage)
        {
            // Arrange
            var validator = new InvitationPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.UserId, userId);

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [Theory]
        [MemberData(nameof(ValidClanId))]
        public void Validate_WhenClanIdIsValid_ShouldNotHaveValidationErrorFor(ClanId clanId)
        {
            // Arrange
            var validator = new InvitationPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.ClanId, clanId);
        }

        [Theory]
        [MemberData(nameof(InvalidClanIds))]
        public void Validate_WhenClanIdIsInvalid_ShouldHaveValidationErrorFor(ClanId clanId, string errorMessage)
        {
            // Arrange
            var validator = new InvitationPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.ClanId, clanId);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }
    }
}
