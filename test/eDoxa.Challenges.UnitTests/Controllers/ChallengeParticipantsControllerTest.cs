// Filename: ChallengeParticipantsControllerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

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
        public ChallengeParticipantsControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator validator) : base(
            testData,
            testMapper,
            validator)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var mockParticipantQuery = new Mock<IParticipantQuery>();

            mockParticipantQuery.Setup(queries => queries.FetchChallengeParticipantsAsync(It.IsAny<ChallengeId>()))
                .ReturnsAsync(Array.Empty<Participant>())
                .Verifiable();

            var controller = new ChallengeParticipantsController(mockParticipantQuery.Object, TestMapper);

            // Act
            var result = await controller.GetAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockParticipantQuery.Verify(participantQuery => participantQuery.FetchChallengeParticipantsAsync(It.IsAny<ChallengeId>()), Times.Once);
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

            var controller = new ChallengeParticipantsController(mockParticipantQuery.Object, TestMapper);

            // Act
            var result = await controller.GetAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockParticipantQuery.Verify(participantQuery => participantQuery.FetchChallengeParticipantsAsync(It.IsAny<ChallengeId>()), Times.Once);
        }
    }
}
