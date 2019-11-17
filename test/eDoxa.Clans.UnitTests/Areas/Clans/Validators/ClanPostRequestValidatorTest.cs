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

        public static TheoryData<string> ValidSummaries =>
            new TheoryData<string>
            {
                "Pretty good clan.",
                "This-is-a,clan."
            };

        public static TheoryData<string, string> InvalidSummaries =>
            new TheoryData<string, string>
            {
                {"Pretty", ClanErrorDescriber.SummaryInvalid()},
                {"This-is-not a ! Clan", ClanErrorDescriber.SummaryInvalid()}
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

        [Theory]
        [MemberData(nameof(ValidSummaries))]
        public void Validate_WhenSummaryIsValid_ShouldNotHaveValidationErrorFor(string summary)
        {
            // Arrange
            var validator = new ClanPostRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Summary, summary);
        }

        [Theory]
        [MemberData(nameof(InvalidSummaries))]
        public void Validate_WhenSummaryIsInvalid_ShouldHaveValidationErrorFor(string summary, string errorMessage)
        {
            // Arrange
            var validator = new ClanPostRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Summary, summary);

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }
    }
}
