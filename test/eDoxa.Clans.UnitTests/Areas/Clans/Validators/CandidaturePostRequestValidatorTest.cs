// Filename: CandidaturePostRequestValidatorTest.cs
// Date Created: 2019-10-02
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Clans.Api.Areas.Clans.ErrorDescribers;
using eDoxa.Clans.Api.Areas.Clans.Validators;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using FluentValidation.TestHelper;

using Xunit;

namespace eDoxa.Clans.UnitTests.Areas.Clans.Validators
{
    public sealed class CandidaturePostRequestValidatorTest : UnitTest
    {
        public CandidaturePostRequestValidatorTest(TestMapperFixture testMapper) : base(testMapper)
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
                {UserId.Empty, CandidatureErrorDescriber.UserIdRequired()}
            };

        public static TheoryData<ClanId> ValidClanId =>
            new TheoryData<ClanId>
            {
                new ClanId()
            };

        public static TheoryData<ClanId, string> InvalidClanIds =>
            new TheoryData<ClanId, string>
            {
                {ClanId.Empty, CandidatureErrorDescriber.ClanIdRequired()}
            };

        [Theory]
        [MemberData(nameof(ValidUserId))]
        public void Validate_WhenUserIdIsValid_ShouldNotHaveValidationErrorFor(UserId userId)
        {
            // Arrange
            var validator = new CandidaturePostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.UserId, userId.ToGuid());
        }

        [Theory]
        [MemberData(nameof(InvalidUserIds))]
        public void Validate_WhenUserIdIsInvalid_ShouldHaveValidationErrorFor(UserId userId, string errorMessage)
        {
            // Arrange
            var validator = new CandidaturePostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.UserId, userId.ToGuid());

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [Theory]
        [MemberData(nameof(ValidClanId))]
        public void Validate_WhenClanIdIsValid_ShouldNotHaveValidationErrorFor(ClanId clanId)
        {
            // Arrange
            var validator = new CandidaturePostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.ClanId, clanId.ToGuid());
        }

        [Theory]
        [MemberData(nameof(InvalidClanIds))]
        public void Validate_WhenClanIdIsInvalid_ShouldHaveValidationErrorFor(ClanId clanId, string errorMessage)
        {
            // Arrange
            var validator = new CandidaturePostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.ClanId, clanId.ToGuid());

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }
    }
}
