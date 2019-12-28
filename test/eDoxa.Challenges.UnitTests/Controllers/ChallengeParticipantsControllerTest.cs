﻿// Filename: ChallengeParticipantsControllerTest.cs
// Date Created: 2019-09-29
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Controllers;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Challenges.UnitTests.Controllers
{
    public sealed class ChallengeParticipantsControllerTest : UnitTest
    {
        public ChallengeParticipantsControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator validator) : base(testData, testMapper, validator)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var mockParticipantQuery = new Mock<IParticipantQuery>();

            mockParticipantQuery.Setup(queries => queries.FetchChallengeParticipantsAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(Array.Empty<Participant>())
                .Verifiable();

            mockParticipantQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ChallengeParticipantsController(mockParticipantQuery.Object);

            controller.ControllerContext.ModelState.AddModelError("error", "error");

            // Act
            var result = await controller.GetAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockParticipantQuery.Verify(participantQuery => participantQuery.FetchChallengeParticipantsAsync(It.IsAny<ChallengeId>()), Times.Never);

            mockParticipantQuery.VerifyGet(challengeQuery => challengeQuery.Mapper, Times.Never);
        }

        [Fact]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var mockParticipantQuery = new Mock<IParticipantQuery>();

            mockParticipantQuery.Setup(queries => queries.FetchChallengeParticipantsAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(Array.Empty<Participant>())
                .Verifiable();

            mockParticipantQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ChallengeParticipantsController(mockParticipantQuery.Object);

            // Act
            var result = await controller.GetAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockParticipantQuery.Verify(participantQuery => participantQuery.FetchChallengeParticipantsAsync(It.IsAny<ChallengeId>()), Times.Once);

            mockParticipantQuery.VerifyGet(challengeQuery => challengeQuery.Mapper, Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(25392992, Game.LeagueOfLegends);

            var challenge = challengeFaker.FakeChallenge();

            var mockParticipantQuery = new Mock<IParticipantQuery>();

            mockParticipantQuery.Setup(participantQuery => participantQuery.FetchChallengeParticipantsAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(challenge.Participants)
                .Verifiable();

            mockParticipantQuery.SetupGet(matchQuery => matchQuery.Mapper).Returns(TestMapper);

            var controller = new ChallengeParticipantsController(mockParticipantQuery.Object);

            // Act
            var result = await controller.GetAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockParticipantQuery.Verify(participantQuery => participantQuery.FetchChallengeParticipantsAsync(It.IsAny<ChallengeId>()), Times.Once);

            mockParticipantQuery.VerifyGet(challengeQuery => challengeQuery.Mapper, Times.Once);
        }

        //[Fact]
        //public async Task PostAsync_ShouldBeNotFoundObjectResult()
        //{
        //    // Arrange
        //    var mockParticipantQuery = new Mock<IParticipantQuery>();

        //    var mockChallengeService = new Mock<IChallengeService>();

        //    mockChallengeService.Setup(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).Verifiable();

        //    mockChallengeService.Setup(
        //            challengeQuery => challengeQuery.RegisterChallengeParticipantAsync(
        //                It.IsAny<IChallenge>(),
        //                It.IsAny<UserId>(),
        //                It.IsAny<ParticipantId>(),
        //                It.IsAny<PlayerId>(),
        //                It.IsAny<UtcNowDateTimeProvider>(),
        //                It.IsAny<CancellationToken>()))
        //        .Verifiable();

        //    var controller = new ChallengeParticipantsController(mockParticipantQuery.Object, mockChallengeService.Object);

        //    var mockHttpContextAccessor = new MockHttpContextAccessor();

        //    controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

        //    // Act
        //    var result = await controller.PostAsync(new ChallengeId(), new RegisterChallengeParticipantRequest(new ParticipantId()));

        //    // Assert
        //    result.Should().BeOfType<NotFoundObjectResult>();

        //    mockChallengeService.Verify(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);

        //    mockChallengeService.Verify(
        //        challengeQuery => challengeQuery.RegisterChallengeParticipantAsync(
        //            It.IsAny<IChallenge>(),
        //            It.IsAny<UserId>(),
        //            It.IsAny<ParticipantId>(),
        //            It.IsAny<PlayerId>(),
        //            It.IsAny<UtcNowDateTimeProvider>(),
        //            It.IsAny<CancellationToken>()),
        //        Times.Never);
        //}

        //[Fact]
        //public async Task PostAsync_ShouldBeOkObjectResult()
        //{
        //    // Arrange
        //    var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(1000, Game.LeagueOfLegends);

        //    var challenge = challengeFaker.FakeChallenge();

        //    var mockParticipantQuery = new Mock<IParticipantQuery>();

        //    mockParticipantQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(TestMapper).Verifiable();

        //    var mockChallengeService = new Mock<IChallengeService>();

        //    mockChallengeService.Setup(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).ReturnsAsync(challenge).Verifiable();

        //    mockChallengeService
        //        .Setup(
        //            challengeQuery => challengeQuery.RegisterChallengeParticipantAsync(
        //                It.IsAny<IChallenge>(),
        //                It.IsAny<UserId>(),
        //                It.IsAny<ParticipantId>(),
        //                It.IsAny<PlayerId>(),
        //                It.IsAny<UtcNowDateTimeProvider>(),
        //                It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(new DomainValidationResult())
        //        .Verifiable();

        //    var controller = new ChallengeParticipantsController(mockParticipantQuery.Object, mockChallengeService.Object);

        //    var mockHttpContextAccessor = new MockHttpContextAccessor();

        //    controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

        //    // Act
        //    var result = await controller.PostAsync(new ChallengeId(), new RegisterChallengeParticipantRequest(new ParticipantId()));

        //    // Assert
        //    result.Should().BeOfType<OkObjectResult>();

        //    mockParticipantQuery.VerifyGet(challengeQuery => challengeQuery.Mapper, Times.Once);

        //    mockChallengeService.Verify(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);

        //    mockChallengeService.Verify(
        //        challengeQuery => challengeQuery.RegisterChallengeParticipantAsync(
        //            It.IsAny<IChallenge>(),
        //            It.IsAny<UserId>(),
        //            It.IsAny<ParticipantId>(),
        //            It.IsAny<PlayerId>(),
        //            It.IsAny<UtcNowDateTimeProvider>(),
        //            It.IsAny<CancellationToken>()),
        //        Times.Once);
        //}

        //[Fact]
        //public async Task PostAsync_ShouldBeBadRequestObjectResult()
        //{
        //    // Arrange
        //    var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(1000, Game.LeagueOfLegends);

        //    var challenge = challengeFaker.FakeChallenge();

        //    var mockParticipantQuery = new Mock<IParticipantQuery>();

        //    var mockChallengeService = new Mock<IChallengeService>();
        //    mockChallengeService.Setup(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).ReturnsAsync(challenge).Verifiable();

        //    mockChallengeService
        //        .Setup(
        //            challengeQuery => challengeQuery.RegisterChallengeParticipantAsync(
        //                It.IsAny<IChallenge>(),
        //                It.IsAny<UserId>(),
        //                It.IsAny<ParticipantId>(),
        //                It.IsAny<PlayerId>(),
        //                It.IsAny<UtcNowDateTimeProvider>(),
        //                It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(DomainValidationResult.Failure("test", "test message"))
        //        .Verifiable();

        //    var controller = new ChallengeParticipantsController(mockParticipantQuery.Object, mockChallengeService.Object);
        //    var mockHttpContextAccessor = new MockHttpContextAccessor();
        //    controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

        //    // Act
        //    var result = await controller.PostAsync(new ChallengeId(), new RegisterChallengeParticipantRequest(new ParticipantId()));

        //    // Assert
        //    result.Should().BeOfType<BadRequestObjectResult>();

        //    mockChallengeService.Verify(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);

        //    mockChallengeService.Verify(
        //        challengeQuery => challengeQuery.RegisterChallengeParticipantAsync(
        //            It.IsAny<IChallenge>(),
        //            It.IsAny<UserId>(),
        //            It.IsAny<ParticipantId>(),
        //            It.IsAny<PlayerId>(),
        //            It.IsAny<UtcNowDateTimeProvider>(),
        //            It.IsAny<CancellationToken>()),
        //        Times.Once);
        //}
    }
}