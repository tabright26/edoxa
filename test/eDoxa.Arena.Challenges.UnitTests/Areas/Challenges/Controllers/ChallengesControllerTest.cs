// Filename: ChallengesControllerTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.ObjectModel;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Controllers;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Arena.Challenges.TestHelper;
using eDoxa.Arena.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Areas.Challenges.Controllers
{
    public sealed class ChallengesControllerTest : UnitTest
    {
        public ChallengesControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        //[Fact]
        //public async Task GetAsync_ShouldBeBadRequestObjectResult()
        //{
        //    // Arrange
        //    var mockChallengeQuery = new Mock<IChallengeQuery>();

        //    mockChallengeQuery.Setup(challengeQuery => challengeQuery.FetchChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()))
        //        .ReturnsAsync(new Collection<IChallenge>())
        //        .Verifiable();

        //    mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(TestMapper).Verifiable();

        //    var controller = new ChallengesController(mockChallengeQuery.Object);

        //    controller.ControllerContext.ModelState.AddModelError("error", "error");

        //    // Act
        //    var result = await controller.GetAsync();

        //    // Assert
        //    result.Should().BeOfType<BadRequestObjectResult>();

        //    mockChallengeQuery.Verify(
        //        challengeQuery => challengeQuery.FetchChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()),
        //        Times.Never);
        //}

        [Fact]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var mockChallengeQuery = new Mock<IChallengeQuery>();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FetchChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()))
                .ReturnsAsync(new Collection<IChallenge>())
                .Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockChallengeQuery.Verify(challengeQuery => challengeQuery.FetchChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(27852992, Game.LeagueOfLegends);

            var challenges = challengeFaker.FakeChallenges(2);

            var mockChallengeQuery = new Mock<IChallengeQuery>();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FetchChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()))
                .ReturnsAsync(challenges)
                .Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockChallengeQuery.Verify(challengeQuery => challengeQuery.FetchChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            var mockChallengeQuery = new Mock<IChallengeQuery>();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object);

            // Act
            var result = await controller.GetByIdAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockChallengeQuery.Verify(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldBeOkObjectResult()
        {
            // Arrange        
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(27852992, Game.LeagueOfLegends);

            var challenge = challengeFaker.FakeChallenge();

            var mockChallengeQuery = new Mock<IChallengeQuery>();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).ReturnsAsync(challenge).Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object);

            // Act
            var result = await controller.GetByIdAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockChallengeQuery.Verify(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);
        }
    }
}
