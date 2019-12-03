// Filename: ChallengesControllerTest.cs
// Date Created: 2019-09-29
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Areas.Challenges.Controllers;
using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.Domain.Services;
using eDoxa.Challenges.Requests;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Challenges.UnitTests.Areas.Challenges.Controllers
{
    public sealed class ChallengesControllerTest : UnitTest
    {
        public ChallengesControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator validator) : base(testData, testMapper, validator)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var mockChallengeQuery = new Mock<IChallengeQuery>();
            var mockChallengeService = new Mock<IChallengeService>();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FetchChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()))
                .ReturnsAsync(new Collection<IChallenge>())
                .Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object, mockChallengeService.Object);

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
            var mockChallengeService = new Mock<IChallengeService>();

            var challenges = challengeFaker.FakeChallenges(2);

            var mockChallengeQuery = new Mock<IChallengeQuery>();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FetchChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>()))
                .ReturnsAsync(challenges)
                .Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object, mockChallengeService.Object);

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
            var mockChallengeService = new Mock<IChallengeService>();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object, mockChallengeService.Object);

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
            var mockChallengeService = new Mock<IChallengeService>();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).ReturnsAsync(challenge).Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object, mockChallengeService.Object);

            // Act
            var result = await controller.GetByIdAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockChallengeQuery.Verify(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);
        }

        [Fact]
        public async Task PostByIdAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(27852992, Game.LeagueOfLegends);

            var challenge = challengeFaker.FakeChallenge();

            var mockChallengeQuery = new Mock<IChallengeQuery>();

            var mockChallengeService = new Mock<IChallengeService>();

            mockChallengeService.Setup(challengeService => challengeService.FindChallengeAsync(It.IsAny<ChallengeId>())).ReturnsAsync(challenge).Verifiable();

            mockChallengeService.Setup(challengeService => challengeService.SynchronizeChallengeAsync(
                It.IsAny<Challenge>(),
                It.IsAny<IDateTimeProvider>(),
                It.IsAny<CancellationToken>()
                )).ReturnsAsync(new DomainValidationResult()).Verifiable();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).ReturnsAsync(challenge).Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object, mockChallengeService.Object);

            // Act
            var result = await controller.PostByIdAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockChallengeQuery.VerifyGet(challengeQuery => challengeQuery.Mapper, Times.Once);
            mockChallengeQuery.Verify(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);
            mockChallengeService.Verify(challengeService => challengeService.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);
            mockChallengeService.Verify(challengeService => challengeService.SynchronizeChallengeAsync(
                It.IsAny<Challenge>(),
                It.IsAny<IDateTimeProvider>(),
                It.IsAny<CancellationToken>()
            ), Times.Once);

        }

        [Fact]
        public async Task PostByIdAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            var mockChallengeQuery = new Mock<IChallengeQuery>();
            var mockChallengeService = new Mock<IChallengeService>();

            mockChallengeService.Setup(challengeService => challengeService.FindChallengeAsync(It.IsAny<ChallengeId>())).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object, mockChallengeService.Object);

            // Act
            var result = await controller.PostByIdAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockChallengeService.Verify(challengeService => challengeService.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);

        }

        [Fact]
        public async Task PostByIdAsync_ShouldBeBadRequestsObjectResult()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(27852992, Game.LeagueOfLegends);
            var challenge = challengeFaker.FakeChallenge();
            var mockChallengeQuery = new Mock<IChallengeQuery>();
            var mockChallengeService = new Mock<IChallengeService>();

            mockChallengeService.Setup(challengeService => challengeService.FindChallengeAsync(It.IsAny<ChallengeId>())).ReturnsAsync(challenge).Verifiable();
            mockChallengeService.Setup(challengeService => challengeService.SynchronizeChallengeAsync(
                It.IsAny<Challenge>(),
                It.IsAny<IDateTimeProvider>(),
                It.IsAny<CancellationToken>()
                )).ReturnsAsync(DomainValidationResult.Failure("test", "test message")).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object, mockChallengeService.Object);

            // Act
            var result = await controller.PostByIdAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            mockChallengeService.Verify(challengeService => challengeService.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);
            mockChallengeService.Verify(challengeService => challengeService.SynchronizeChallengeAsync(
                It.IsAny<Challenge>(),
                It.IsAny<IDateTimeProvider>(),
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(27852992, Game.LeagueOfLegends);
            var challenge = challengeFaker.FakeChallenge();
            var mockChallengeQuery = new Mock<IChallengeQuery>();
            var mockChallengeService = new Mock<IChallengeService>();

            mockChallengeService.Setup(challengeService => challengeService.CreateChallengeAsync(
                It.IsAny<ChallengeId>(),
                It.IsAny<ChallengeName>(),
                It.IsAny<Game>(),
                It.IsAny<BestOf>(),
                It.IsAny<Entries>(),
                It.IsAny<ChallengeDuration>(),
                It.IsAny<IDateTimeProvider>(),
                It.IsAny<CancellationToken>()
                )).ReturnsAsync(new DomainValidationResult()).Verifiable();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).ReturnsAsync(challenge).Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object, mockChallengeService.Object);

            // Act
            var result = await controller.PostAsync(new CreateChallengeRequest(new Guid(), "test", "League of legends", 5, 50, 1));

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockChallengeQuery.VerifyGet(challengeQuery => challengeQuery.Mapper, Times.Once);
            mockChallengeQuery.Verify(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once); ;
            mockChallengeService.Verify(challengeService => challengeService.CreateChallengeAsync(
                It.IsAny<ChallengeId>(),
                It.IsAny<ChallengeName>(),
                It.IsAny<Game>(),
                It.IsAny<BestOf>(),
                It.IsAny<Entries>(),
                It.IsAny<ChallengeDuration>(),
                It.IsAny<IDateTimeProvider>(),
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var mockChallengeQuery = new Mock<IChallengeQuery>();
            var mockChallengeService = new Mock<IChallengeService>();

            mockChallengeService.Setup(challengeService => challengeService.CreateChallengeAsync(
                It.IsAny<ChallengeId>(),
                It.IsAny<ChallengeName>(),
                It.IsAny<Game>(),
                It.IsAny<BestOf>(),
                It.IsAny<Entries>(),
                It.IsAny<ChallengeDuration>(),
                It.IsAny<IDateTimeProvider>(),
                It.IsAny<CancellationToken>()
                )).ReturnsAsync(DomainValidationResult.Failure("test", "test message")).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object, mockChallengeService.Object);

            // Act
            var result = await controller.PostAsync(new CreateChallengeRequest(new Guid(), "test", "League of legends", 5, 50, 1));

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            mockChallengeService.Verify(challengeService => challengeService.CreateChallengeAsync(
                It.IsAny<ChallengeId>(),
                It.IsAny<ChallengeName>(),
                It.IsAny<Game>(),
                It.IsAny<BestOf>(),
                It.IsAny<Entries>(),
                It.IsAny<ChallengeDuration>(),
                It.IsAny<IDateTimeProvider>(),
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }
    }
}
