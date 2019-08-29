// Filename: ParticipantsControllerTest.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Controllers;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
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
    public sealed class ParticipantsControllerTest
    {
        [TestMethod]
        public async Task GetByIdAsync_ShouldBeOkObjectResult()
        {
            // Arrange        
            var challengeFaker = new ChallengeFaker(state: ChallengeState.InProgress);

            challengeFaker.UseSeed(95632852);

            var challenge = challengeFaker.Generate();

            var participants = challenge.Participants;

            var mockParticipantQuery = new Mock<IParticipantQuery>();
            
            mockParticipantQuery.Setup(participantQuery => participantQuery.FindParticipantAsync(It.IsAny<ParticipantId>())).ReturnsAsync(participants.First()).Verifiable();

            mockParticipantQuery.SetupGet(participantQuery => participantQuery.Mapper).Returns(MapperExtensions.Mapper).Verifiable();

            var controller = new ParticipantsController(mockParticipantQuery.Object);

            // Act
            var result = await controller.GetByIdAsync(new ParticipantId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockParticipantQuery.Verify(participantQuery => participantQuery.FindParticipantAsync(It.IsAny<ParticipantId>()), Times.Once);

            mockParticipantQuery.VerifyGet(participantQuery => participantQuery.Mapper, Times.Once);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            var mockParticipantQuery = new Mock<IParticipantQuery>();

            mockParticipantQuery.Setup(participantQuery => participantQuery.FindParticipantAsync(It.IsAny<ParticipantId>())).Verifiable();

            mockParticipantQuery.SetupGet(participantQuery => participantQuery.Mapper).Returns(MapperExtensions.Mapper).Verifiable();

            var controller = new ParticipantsController(mockParticipantQuery.Object);

            // Act
            var result = await controller.GetByIdAsync(new ParticipantId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockParticipantQuery.Verify(participantQuery => participantQuery.FindParticipantAsync(It.IsAny<ParticipantId>()), Times.Once);

            mockParticipantQuery.VerifyGet(participantQuery => participantQuery.Mapper, Times.Once);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var mockParticipantQuery = new Mock<IParticipantQuery>();

            mockParticipantQuery.Setup(participantQuery => participantQuery.FindParticipantAsync(It.IsAny<ParticipantId>())).Verifiable();

            mockParticipantQuery.SetupGet(participantQuery => participantQuery.Mapper).Returns(MapperExtensions.Mapper).Verifiable();

            var controller = new ParticipantsController(mockParticipantQuery.Object);

            controller.ControllerContext.ModelState.AddModelError("error", "error");

            // Act
            var result = await controller.GetByIdAsync(new ParticipantId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockParticipantQuery.Verify(participantQuery => participantQuery.FindParticipantAsync(It.IsAny<ParticipantId>()), Times.Never);

            mockParticipantQuery.VerifyGet(participantQuery => participantQuery.Mapper, Times.Never);
        }
    }
}
