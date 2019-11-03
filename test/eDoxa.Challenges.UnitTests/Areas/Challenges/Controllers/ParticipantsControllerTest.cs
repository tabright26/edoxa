// Filename: ParticipantsControllerTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Areas.Challenges.Controllers;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Challenges.UnitTests.Areas.Challenges.Controllers
{
    public sealed class ParticipantsControllerTest : UnitTest
    {
        public ParticipantsControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        //[Fact]
        //public async Task GetByIdAsync_ShouldBeBadRequestObjectResult()
        //{
        //    // Arrange
        //    var mockParticipantQuery = new Mock<IParticipantQuery>();

        //    mockParticipantQuery.Setup(participantQuery => participantQuery.FindParticipantAsync(It.IsAny<ParticipantId>())).Verifiable();

        //    mockParticipantQuery.SetupGet(participantQuery => participantQuery.Mapper).Returns(TestMapper).Verifiable();

        //    var controller = new ParticipantsController(mockParticipantQuery.Object);

        //    controller.ControllerContext.ModelState.AddModelError("error", "error");

        //    // Act
        //    var result = await controller.GetByIdAsync(new ParticipantId());

        //    // Assert
        //    result.Should().BeOfType<BadRequestObjectResult>();

        //    mockParticipantQuery.Verify(participantQuery => participantQuery.FindParticipantAsync(It.IsAny<ParticipantId>()), Times.Never);

        //    mockParticipantQuery.VerifyGet(participantQuery => participantQuery.Mapper, Times.Never);
        //}

        [Fact]
        public async Task GetByIdAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            var mockParticipantQuery = new Mock<IParticipantQuery>();

            mockParticipantQuery.Setup(participantQuery => participantQuery.FindParticipantAsync(It.IsAny<ParticipantId>())).Verifiable();

            mockParticipantQuery.SetupGet(participantQuery => participantQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ParticipantsController(mockParticipantQuery.Object);

            // Act
            var result = await controller.GetByIdAsync(new ParticipantId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockParticipantQuery.Verify(participantQuery => participantQuery.FindParticipantAsync(It.IsAny<ParticipantId>()), Times.Once);

            mockParticipantQuery.VerifyGet(participantQuery => participantQuery.Mapper, Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldBeOkObjectResult()
        {
            // Arrange        
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(95632852, Game.LeagueOfLegends, ChallengeState.InProgress);

            var challenge = challengeFaker.FakeChallenge();

            var participants = challenge.Participants;

            var mockParticipantQuery = new Mock<IParticipantQuery>();

            mockParticipantQuery.Setup(participantQuery => participantQuery.FindParticipantAsync(It.IsAny<ParticipantId>()))
                .ReturnsAsync(participants.First())
                .Verifiable();

            mockParticipantQuery.SetupGet(participantQuery => participantQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ParticipantsController(mockParticipantQuery.Object);

            // Act
            var result = await controller.GetByIdAsync(new ParticipantId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockParticipantQuery.Verify(participantQuery => participantQuery.FindParticipantAsync(It.IsAny<ParticipantId>()), Times.Once);

            mockParticipantQuery.VerifyGet(participantQuery => participantQuery.Mapper, Times.Once);
        }
    }
}
