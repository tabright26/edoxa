// Filename: CandidaturePostRequestValidatorTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Clans.Api.Application.Validators;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentValidation.TestHelper;

using Xunit;

namespace eDoxa.Clans.UnitTests.Application.Validators
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

        //public static TheoryData<UserId, string> InvalidUserIds =>
        //    new TheoryData<UserId, string>
        //    {
        //        {UserId.Empty, CandidatureErrorDescriber.UserIdRequired()}
        //    };

        public static TheoryData<ClanId> ValidClanId =>
            new TheoryData<ClanId>
            {
                new ClanId()
            };

        //public static TheoryData<ClanId, string> InvalidClanIds =>
        //    new TheoryData<ClanId, string>
        //    {
        //        {ClanId.Empty, CandidatureErrorDescriber.ClanIdRequired()}
        //    };

        [Theory]
        [MemberData(nameof(ValidUserId))]
        public void Validate_WhenUserIdIsValid_ShouldNotHaveValidationErrorFor(UserId userId)
        {
            // Arrange
            var validator = new CandidaturePostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.UserId, userId.ToString());
        }

        //[Theory]
        //[MemberData(nameof(InvalidUserIds))]
        //public void Validate_WhenUserIdIsInvalid_ShouldHaveValidationErrorFor(UserId userId, string errorMessage)
        //{
        //    // Arrange
        //    var validator = new CandidaturePostRequestValidator();

        //    // Act - Assert
        //    var failures = validator.ShouldHaveValidationErrorFor(request => request.UserId, userId.ToString());

        //    failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        //}

        [Theory]
        [MemberData(nameof(ValidClanId))]
        public void Validate_WhenClanIdIsValid_ShouldNotHaveValidationErrorFor(ClanId clanId)
        {
            // Arrange
            var validator = new CandidaturePostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.ClanId, clanId.ToString());
        }

        //[Theory]
        //[MemberData(nameof(InvalidClanIds))]
        //public void Validate_WhenClanIdIsInvalid_ShouldHaveValidationErrorFor(ClanId clanId, string errorMessage)
        //{
        //    // Arrange
        //    var validator = new CandidaturePostRequestValidator();

        //    // Act - Assert
        //    var failures = validator.ShouldHaveValidationErrorFor(request => request.ClanId, clanId.ToString());

        //    failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        //}
    }
}
