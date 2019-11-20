// Filename: ClanPostRequestValidatorTest.cs
// Date Created: 2019-10-02
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Challenges.Api.Areas.Challenges.ErrorDescribers;
using eDoxa.Challenges.Api.Areas.Challenges.Validators;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;

using FluentAssertions;

using FluentValidation.TestHelper;

using Xunit;

namespace eDoxa.Challenges.UnitTests.Areas.Challenges.Validators
{
    public sealed class ChallengePostRequestValidatorTest : UnitTest
    {
        public ChallengePostRequestValidatorTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator validator) : base(testData, testMapper, validator)
        {
        }

        public static TheoryData<string> ValidChallengesName =>
            new TheoryData<string>
            {
                "test challenge",
                "test top kek challenge",
                "test kappa challenge"
            };

        public static TheoryData<string, string> InvalidChallengesName =>
            new TheoryData<string, string>
            {
                { null, ChallengeErrorDescriber.NameInvalid()},
                { "", ChallengeErrorDescriber.NameInvalid()}
            };


        [Theory]
        [MemberData(nameof(ValidChallengesName))]
        public void Validate_WhenChallengeNameIsValid_ShouldNotHaveValidationErrorFor(string name)
        {
            // Arrange
            var validator = new ChallengePostRequestValidator(TestValidator.OptionsWrapper);

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Name, name);
        }

        [Theory]
        [MemberData(nameof(InvalidChallengesName))]
        public void Validate_WhenNameIsInvalid_ShouldHaveValidationErrorFor(string name, string errorMessage)
        {
            // Arrange
            var validator = new ChallengePostRequestValidator(TestValidator.OptionsWrapper);

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Name, name);

            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }
    }
}
