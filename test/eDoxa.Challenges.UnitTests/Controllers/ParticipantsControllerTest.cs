// Filename: ParticipantsControllerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Controllers;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Challenges.UnitTests.Controllers
{
    public sealed class ParticipantsControllerTest : UnitTest
    {
        public ParticipantsControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator validator) : base(
            testData,
            testMapper,
            validator)
        {
        }

        [Fact]
        public async Task FindParticipantAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            TestMock.ParticipantQuery.Setup(participantQuery => participantQuery.FindParticipantAsync(It.IsAny<ParticipantId>())).Verifiable();

            var controller = new ParticipantsController(TestMock.ParticipantQuery.Object, TestMapper);

            // Act
            var result = await controller.FindParticipantAsync(new ParticipantId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            TestMock.ParticipantQuery.Verify(participantQuery => participantQuery.FindParticipantAsync(It.IsAny<ParticipantId>()), Times.Once);
        }

        [Fact]
        public async Task FindParticipantAsync_ShouldBeOkObjectResult()
        {
            // Arrange        
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(95632852, Game.LeagueOfLegends, ChallengeState.InProgress);

            var challenge = challengeFaker.FakeChallenge();

            var participants = challenge.Participants;

            TestMock.ParticipantQuery.Setup(participantQuery => participantQuery.FindParticipantAsync(It.IsAny<ParticipantId>()))
                .ReturnsAsync(participants.First())
                .Verifiable();

            var controller = new ParticipantsController(TestMock.ParticipantQuery.Object, TestMapper);

            // Act
            var result = await controller.FindParticipantAsync(new ParticipantId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            TestMock.ParticipantQuery.Verify(participantQuery => participantQuery.FindParticipantAsync(It.IsAny<ParticipantId>()), Times.Once);
        }
    }
}
