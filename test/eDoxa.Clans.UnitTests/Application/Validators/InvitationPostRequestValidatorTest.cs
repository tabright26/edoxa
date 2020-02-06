// Filename: InvitationPostRequestValidatorTest.cs
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

        public static TheoryData<ClanId> ValidClanId =>
            new TheoryData<ClanId>
            {
                new ClanId()
            };

        [Theory]
        [MemberData(nameof(ValidUserId))]
        public void Validate_WhenUserIdIsValid_ShouldNotHaveValidationErrorFor(UserId userId)
        {
            // Arrange
            var validator = new InvitationPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.UserId, userId.ToString());
        }

        [Theory]
        [MemberData(nameof(ValidClanId))]
        public void Validate_WhenClanIdIsValid_ShouldNotHaveValidationErrorFor(ClanId clanId)
        {
            // Arrange
            var validator = new InvitationPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.ClanId, clanId.ToString());
        }
    }
}
