// Filename: ChallengeHistoryControllerTest.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.ObjectModel;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Area.Challenge.Controllers;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Arena.Challenges.UnitTests.Helpers.Extensions;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Area.Challenge.Controllers
{
    [TestClass]
    public sealed class ChallengeHistoryControllerTest
    {
        private Mock<IMediator> _mockMediator;
        private Mock<IChallengeQuery> _mockChallengeQuery;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockChallengeQuery = new Mock<IChallengeQuery>();
            _mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(MapperExtensions.Mapper);
            _mockMediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task GetAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();
            challengeFaker.UseSeed(48392992);
            var challenges = challengeFaker.Generate(2);

            _mockChallengeQuery.Setup(challengeQuery => challengeQuery.FetchUserChallengeHistoryAsync(It.IsAny<ChallengeGame>(), It.IsAny<ChallengeState>()))
                .ReturnsAsync(challenges)
                .Verifiable();

            var controller = new ChallengeHistoryController(_mockChallengeQuery.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockChallengeQuery.Verify();

            _mockMediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            _mockChallengeQuery.Setup(queries => queries.FetchUserChallengeHistoryAsync(It.IsAny<ChallengeGame>(), It.IsAny<ChallengeState>()))
                .ReturnsAsync(new Collection<IChallenge>())
                .Verifiable();

            var controller = new ChallengeHistoryController(_mockChallengeQuery.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _mockChallengeQuery.Verify();

            _mockMediator.VerifyNoOtherCalls();
        }
    }
}
