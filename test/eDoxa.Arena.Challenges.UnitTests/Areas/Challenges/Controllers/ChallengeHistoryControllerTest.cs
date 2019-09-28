// Filename: ChallengeHistoryControllerTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.ObjectModel;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Controllers;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Arena.Challenges.UnitTests.TestHelpers;
using eDoxa.Arena.Challenges.UnitTests.TestHelpers.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Areas.Challenges.Controllers
{
    public sealed class ChallengeHistoryControllerTest : UnitTest
    {
        public ChallengeHistoryControllerTest(ChallengeFakerFixture challengeFaker) : base(challengeFaker)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var mockChallengeQuery = new Mock<IChallengeQuery>();

            mockChallengeQuery.Setup(queries => queries.FetchUserChallengeHistoryAsync(It.IsAny<ChallengeGame>(), It.IsAny<ChallengeState>()))
                .ReturnsAsync(new Collection<IChallenge>())
                .Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(MapperExtensions.Mapper).Verifiable();

            var controller = new ChallengeHistoryController(mockChallengeQuery.Object);

            controller.ControllerContext.ModelState.AddModelError("error", "error");

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockChallengeQuery.Verify(
                challengeQuery => challengeQuery.FetchUserChallengeHistoryAsync(It.IsAny<ChallengeGame>(), It.IsAny<ChallengeState>()),
                Times.Never);

            mockChallengeQuery.VerifyGet(challengeQuery => challengeQuery.Mapper, Times.Never);
        }

        [Fact]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var mockChallengeQuery = new Mock<IChallengeQuery>();

            mockChallengeQuery.Setup(queries => queries.FetchUserChallengeHistoryAsync(It.IsAny<ChallengeGame>(), It.IsAny<ChallengeState>()))
                .ReturnsAsync(new Collection<IChallenge>())
                .Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(MapperExtensions.Mapper).Verifiable();

            var controller = new ChallengeHistoryController(mockChallengeQuery.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockChallengeQuery.Verify(
                challengeQuery => challengeQuery.FetchUserChallengeHistoryAsync(It.IsAny<ChallengeGame>(), It.IsAny<ChallengeState>()),
                Times.Once);

            mockChallengeQuery.VerifyGet(challengeQuery => challengeQuery.Mapper, Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var challengeFaker = ChallengeFaker.Factory.CreateFaker(48392992);

            var challenges = challengeFaker.FakeChallenges(2);

            var mockChallengeQuery = new Mock<IChallengeQuery>();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FetchUserChallengeHistoryAsync(It.IsAny<ChallengeGame>(), It.IsAny<ChallengeState>()))
                .ReturnsAsync(challenges)
                .Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(MapperExtensions.Mapper).Verifiable();

            var controller = new ChallengeHistoryController(mockChallengeQuery.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockChallengeQuery.Verify(
                challengeQuery => challengeQuery.FetchUserChallengeHistoryAsync(It.IsAny<ChallengeGame>(), It.IsAny<ChallengeState>()),
                Times.Once);

            mockChallengeQuery.VerifyGet(challengeQuery => challengeQuery.Mapper, Times.Once);
        }
    }
}
