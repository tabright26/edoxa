// Filename: ClanPostRequestValidatorTest.cs
// Date Created: 2019-10-02
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Clans.Api.Areas.Clans.ErrorDescribers;
using eDoxa.Clans.Api.Areas.Clans.Validators;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;

using FluentAssertions;

using FluentValidation.TestHelper;

using Xunit;

namespace eDoxa.Clans.UnitTests.Areas.Clans.Validators
{
    public sealed class ClanPostRequestValidatorTest : UnitTest
    {
        public ClanPostRequestValidatorTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        public static TheoryData<string> ValidNames =>
            new TheoryData<string>
            {
                "MagicPotato",
                "PrettyLasagna"
            };

        public static TheoryData<string, string> InvalidNames =>
            new TheoryData<string, string>
            {
                {null, ClanErrorDescriber.NameRequired()},
                {"", ClanErrorDescriber.NameRequired()},
                {"Ba", ClanErrorDescriber.NameLength()},
                {"Ba!", ClanErrorDescriber.NameInvalid()}
            };

        [Theory]
        [MemberData(nameof(ValidNames))]
        public void Validate_WhenNameIsValid_ShouldNotHaveValidationErrorFor(string name)
        {
            // Arrange
            var validator = new ClanPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Name, name);
        }

        [Theory]
        [MemberData(nameof(InvalidNames))]
        public void Validate_WhenNameIsInvalid_ShouldHaveValidationErrorFor(string name, string errorMessage)
        {
            // Arrange
            var validator = new ClanPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Name, name);

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

    }
}
