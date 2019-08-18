// Filename: RegisterParticipantRequestValidatorTest.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Challenges.Api.Application.Requests.Validations;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Queries;

using FluentValidation.TestHelper;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Requests.Validations
{
    [TestClass]
    public sealed class RegisterParticipantRequestValidatorTest
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

            var challengeViewModel = challengeFaker.Generate();

            _mockChallengeQuery.Setup(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(challengeViewModel)
                .Verifiable();

            var validator = new RegisterParticipantValidator(_mockHttpContextAccessor.Object, _mockChallengeQuery.Object);

            // Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.ChallengeId, ChallengeId.FromGuid(challengeViewModel.Id));

            _mockChallengeQuery.Verify(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);
        }
    }
}
