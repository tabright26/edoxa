// Filename: FakeChallengesCommandValidatorTest.cs
// Date Created: 2019-06-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Api.Application.Commands.Validations;
using eDoxa.Arena.Challenges.Domain.Abstractions.Queries;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using FluentValidation.TestHelper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Commands.Validations
{
    [TestClass]
    public sealed class FakeChallengesCommandValidatorTest
    {
        private FakeChallengesCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            var mockChallengeQuery = new Mock<IChallengeQuery>();
            _validator = new FakeChallengesCommandValidator(mockChallengeQuery.Object);
        }

        [TestMethod]
        public void Validate_CreateChallengeCommand_ShouldBeTrue()
        {
            // Assert
            _validator.ShouldNotHaveValidationErrorFor(command => command.Game, ChallengeGame.LeagueOfLegends);
        }
    }
}
