// Filename: RegisterParticipantCommandValidatorTest.cs
// Date Created: 2019-06-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Api.Application.Commands.Validations;
using eDoxa.Arena.Challenges.Api.Application.Fakers;
using eDoxa.Arena.Challenges.Api.Application.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Queries;

using FluentValidation.TestHelper;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Commands.Validations
{
    [TestClass]
    public sealed class RegisterParticipantCommandValidatorTest
    {
        private Mock<IChallengeQuery> _mockChallengeQuery;
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockChallengeQuery = new Mock<IChallengeQuery>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        }

        [TestMethod]
        public void ShouldBeValidChallengeId()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();

            var challengeViewModel = challengeFaker.GenerateViewModel();

            _mockChallengeQuery.Setup(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(challengeViewModel)
                .Verifiable();

            var validator = new RegisterParticipantCommandValidator(_mockHttpContextAccessor.Object, _mockChallengeQuery.Object);

            // Assert
            validator.ShouldNotHaveValidationErrorFor(command => command.ChallengeId, ChallengeId.FromGuid(challengeViewModel.Id));

            _mockChallengeQuery.Verify(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);
        }
    }
}
