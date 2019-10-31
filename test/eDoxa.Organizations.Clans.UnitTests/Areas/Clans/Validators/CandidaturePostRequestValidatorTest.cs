// Filename: CandidaturePostRequestValidatorTest.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Organizations.Clans.Api.Areas.Clans.ErrorDescribers;
using eDoxa.Organizations.Clans.Api.Areas.Clans.Validators;
using eDoxa.Organizations.Clans.TestHelper;
using eDoxa.Organizations.Clans.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using FluentValidation.TestHelper;

using Xunit;

namespace eDoxa.Organizations.Clans.UnitTests.Areas.Clans.Validators
{
    public sealed class CandidaturePostRequestValidatorTest : UnitTest
    {
        public CandidaturePostRequestValidatorTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        public static TheoryData<ClanId> ValidClanId =>
            new TheoryData<ClanId>
            {
                new ClanId()
            };

        public static TheoryData<ClanId, string> InvalidClanIds =>
            new TheoryData<ClanId, string>
            {
                {null, CandidatureErrorDescriber.ClanIdRequired()}
            };

        [Theory]
        [MemberData(nameof(ValidClanId))]
        public void Validate_WhenClanIdIsValid_ShouldNotHaveValidationErrorFor(ClanId clanId)
        {
            // Arrange
            var validator = new CandidaturePostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.ClanId, clanId);
        }

        [Theory]
        [MemberData(nameof(InvalidClanIds))]
        public void Validate_WhenClanIdIsInvalid_ShouldHaveValidationErrorFor(ClanId clanId, string errorMessage)
        {
            // Arrange
            var validator = new CandidaturePostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.ClanId, clanId);

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }
    }
}
