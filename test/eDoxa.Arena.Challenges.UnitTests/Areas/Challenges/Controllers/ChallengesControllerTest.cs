﻿// Filename: ChallengesControllerTest.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.ObjectModel;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Controllers;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Arena.Challenges.UnitTests.Helpers.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Areas.Challenges.Controllers
{
    [TestClass]
    public sealed class ChallengesControllerTest
    {
        [TestMethod]
        public async Task GetAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();

            challengeFaker.UseSeed(27852992);

            var challenges = challengeFaker.Generate(2);

            var mockChallengeQuery = new Mock<IChallengeQuery>();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FetchChallengesAsync(It.IsAny<ChallengeGame>(), It.IsAny<ChallengeState>()))
                .ReturnsAsync(challenges)
                .Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(MapperExtensions.Mapper).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockChallengeQuery.Verify(challengeQuery => challengeQuery.FetchChallengesAsync(It.IsAny<ChallengeGame>(), It.IsAny<ChallengeState>()), Times.Once);
        }

        [TestMethod]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var mockChallengeQuery = new Mock<IChallengeQuery>();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FetchChallengesAsync(It.IsAny<ChallengeGame>(), It.IsAny<ChallengeState>()))
                .ReturnsAsync(new Collection<IChallenge>())
                .Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(MapperExtensions.Mapper).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockChallengeQuery.Verify(challengeQuery => challengeQuery.FetchChallengesAsync(It.IsAny<ChallengeGame>(), It.IsAny<ChallengeState>()), Times.Once);
        }

        [TestMethod]
        public async Task GetAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var mockChallengeQuery = new Mock<IChallengeQuery>();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FetchChallengesAsync(It.IsAny<ChallengeGame>(), It.IsAny<ChallengeState>()))
                .ReturnsAsync(new Collection<IChallenge>())
                .Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(MapperExtensions.Mapper).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object);

            controller.ControllerContext.ModelState.AddModelError("error", "error");

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockChallengeQuery.Verify(challengeQuery => challengeQuery.FetchChallengesAsync(It.IsAny<ChallengeGame>(), It.IsAny<ChallengeState>()), Times.Never);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldBeOkObjectResult()
        {
            // Arrange        
            var challengeFaker = new ChallengeFaker();

            challengeFaker.UseSeed(27852992);

            var challenge = challengeFaker.Generate();

            var mockChallengeQuery = new Mock<IChallengeQuery>();
            
            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).ReturnsAsync(challenge).Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(MapperExtensions.Mapper).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object);

            // Act
            var result = await controller.GetByIdAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockChallengeQuery.Verify(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            var mockChallengeQuery = new Mock<IChallengeQuery>();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(MapperExtensions.Mapper).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object);

            // Act
            var result = await controller.GetByIdAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockChallengeQuery.Verify(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var mockChallengeQuery = new Mock<IChallengeQuery>();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(MapperExtensions.Mapper).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object);

            controller.ControllerContext.ModelState.AddModelError("error", "error");

            // Act
            var result = await controller.GetByIdAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockChallengeQuery.Verify(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Never);
        }
    }
}