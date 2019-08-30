﻿// Filename: ChallengeParticipantsControllerTest.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Controllers;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.UnitTests.Helpers.Extensions;
using eDoxa.Arena.Challenges.UnitTests.Helpers.Mocks;
using eDoxa.Seedwork.Domain;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Areas.Challenges.Controllers
{
    [TestClass]
    public sealed class ChallengeParticipantsControllerTest
    {
        [TestMethod]
        public async Task GetAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();

            challengeFaker.UseSeed(25392992);

            var challenge = challengeFaker.Generate();

            var mockParticipantQuery = new Mock<IParticipantQuery>();

            var mockChallengeService = new Mock<IChallengeService>();

            var mockChallengeQuery = new Mock<IChallengeQuery>();
            
            mockParticipantQuery.Setup(participantQuery => participantQuery.FetchChallengeParticipantsAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(challenge.Participants)
                .Verifiable();

            mockParticipantQuery.SetupGet(matchQuery => matchQuery.Mapper).Returns(MapperExtensions.Mapper);

            var controller = new ChallengeParticipantsController(mockParticipantQuery.Object, mockChallengeQuery.Object, mockChallengeService.Object);

            // Act
            var result = await controller.GetAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockParticipantQuery.Verify(participantQuery => participantQuery.FetchChallengeParticipantsAsync(It.IsAny<ChallengeId>()), Times.Once);

            mockParticipantQuery.VerifyGet(challengeQuery => challengeQuery.Mapper, Times.Once);
        }

        [TestMethod]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var mockChallengeService = new Mock<IChallengeService>();

            var mockChallengeQuery = new Mock<IChallengeQuery>();

            var mockParticipantQuery = new Mock<IParticipantQuery>();

            mockParticipantQuery.Setup(queries => queries.FetchChallengeParticipantsAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(Array.Empty<Participant>())
                .Verifiable();

            mockParticipantQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(MapperExtensions.Mapper).Verifiable();

            var controller = new ChallengeParticipantsController(mockParticipantQuery.Object, mockChallengeQuery.Object, mockChallengeService.Object);

            // Act
            var result = await controller.GetAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockParticipantQuery.Verify(participantQuery => participantQuery.FetchChallengeParticipantsAsync(It.IsAny<ChallengeId>()), Times.Once);

            mockParticipantQuery.VerifyGet(challengeQuery => challengeQuery.Mapper, Times.Once);
        }

        [TestMethod]
        public async Task GetAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var mockChallengeService = new Mock<IChallengeService>();

            var mockChallengeQuery = new Mock<IChallengeQuery>();

            var mockParticipantQuery = new Mock<IParticipantQuery>();

            mockParticipantQuery.Setup(queries => queries.FetchChallengeParticipantsAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(Array.Empty<Participant>())
                .Verifiable();

            mockParticipantQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(MapperExtensions.Mapper).Verifiable();

            var controller = new ChallengeParticipantsController(mockParticipantQuery.Object, mockChallengeQuery.Object, mockChallengeService.Object);

            controller.ControllerContext.ModelState.AddModelError("error", "error");

            // Act
            var result = await controller.GetAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockParticipantQuery.Verify(participantQuery => participantQuery.FetchChallengeParticipantsAsync(It.IsAny<ChallengeId>()), Times.Never);

            mockParticipantQuery.VerifyGet(challengeQuery => challengeQuery.Mapper, Times.Never);
        }

        [TestMethod]
        public async Task PostAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(state: ChallengeState.Inscription);

            challengeFaker.UseSeed(1000);

            var challenge = challengeFaker.Generate();

            var mockParticipantQuery = new Mock<IParticipantQuery>();

            var mockChallengeService = new Mock<IChallengeService>();

            var mockChallengeQuery = new Mock<IChallengeQuery>();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).ReturnsAsync(challenge).Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Verifiable();

            mockChallengeService
                .Setup(
                    challengeQuery => challengeQuery.RegisterParticipantAsync(
                        It.IsAny<ChallengeId>(),
                        It.IsAny<UserId>(),
                        It.IsAny<UtcNowDateTimeProvider>(),
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var controller = new ChallengeParticipantsController(mockParticipantQuery.Object, mockChallengeQuery.Object, mockChallengeService.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await controller.PostAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockChallengeQuery.Verify(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);

            mockChallengeQuery.VerifyGet(challengeQuery => challengeQuery.Mapper, Times.Never);

            mockChallengeService.Verify(
                challengeQuery => challengeQuery.RegisterParticipantAsync(
                    It.IsAny<ChallengeId>(),
                    It.IsAny<UserId>(),
                    It.IsAny<UtcNowDateTimeProvider>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [TestMethod]
        public async Task PostAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            var mockParticipantQuery = new Mock<IParticipantQuery>();

            var mockChallengeService = new Mock<IChallengeService>();

            var mockChallengeQuery = new Mock<IChallengeQuery>();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Verifiable();

            mockChallengeService
                .Setup(
                    challengeQuery => challengeQuery.RegisterParticipantAsync(
                        It.IsAny<ChallengeId>(),
                        It.IsAny<UserId>(),
                        It.IsAny<UtcNowDateTimeProvider>(),
                        It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new ChallengeParticipantsController(mockParticipantQuery.Object, mockChallengeQuery.Object, mockChallengeService.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await controller.PostAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockChallengeQuery.Verify(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);

            mockChallengeQuery.VerifyGet(challengeQuery => challengeQuery.Mapper, Times.Never);

            mockChallengeService.Verify(
                challengeQuery => challengeQuery.RegisterParticipantAsync(
                    It.IsAny<ChallengeId>(),
                    It.IsAny<UserId>(),
                    It.IsAny<UtcNowDateTimeProvider>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [TestMethod]
        public async Task PostAsync_ShouldBeNBadRequestObjectResultt()
        {
            // Arrange
            var mockParticipantQuery = new Mock<IParticipantQuery>();

            var mockChallengeService = new Mock<IChallengeService>();

            var mockChallengeQuery = new Mock<IChallengeQuery>();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Verifiable();

            mockChallengeService
                .Setup(
                    challengeQuery => challengeQuery.RegisterParticipantAsync(
                        It.IsAny<ChallengeId>(),
                        It.IsAny<UserId>(),
                        It.IsAny<UtcNowDateTimeProvider>(),
                        It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new ChallengeParticipantsController(mockParticipantQuery.Object, mockChallengeQuery.Object, mockChallengeService.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            controller.ControllerContext.ModelState.AddModelError("error", "error");

            // Act
            var result = await controller.PostAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockChallengeQuery.Verify(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Never);

            mockChallengeQuery.VerifyGet(challengeQuery => challengeQuery.Mapper, Times.Never);

            mockChallengeService.Verify(
                challengeQuery => challengeQuery.RegisterParticipantAsync(
                    It.IsAny<ChallengeId>(),
                    It.IsAny<UserId>(),
                    It.IsAny<UtcNowDateTimeProvider>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}