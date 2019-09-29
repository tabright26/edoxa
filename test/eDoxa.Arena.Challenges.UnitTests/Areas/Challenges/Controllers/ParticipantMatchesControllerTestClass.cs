// Filename: ParticipantMatchesControllerTestClass.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Controllers;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Arena.Challenges.TestHelpers;
using eDoxa.Arena.Challenges.UnitTests.TestHelpers;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Areas.Challenges.Controllers
{
    public sealed class ParticipantMatchesControllerTestClass : UnitTestClass
    {
        public ParticipantMatchesControllerTestClass(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var mockMatchQuery = new Mock<IMatchQuery>();

            mockMatchQuery.Setup(matchQuery => matchQuery.FetchParticipantMatchesAsync(It.IsAny<ParticipantId>()))
                .ReturnsAsync(new Collection<IMatch>())
                .Verifiable();

            mockMatchQuery.SetupGet(matchQuery => matchQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ParticipantMatchesController(mockMatchQuery.Object);

            controller.ControllerContext.ModelState.AddModelError("error", "error");

            // Act
            var result = await controller.GetAsync(new ParticipantId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockMatchQuery.Verify(matchQuery => matchQuery.FetchParticipantMatchesAsync(It.IsAny<ParticipantId>()), Times.Never);

            mockMatchQuery.VerifyGet(matchQuery => matchQuery.Mapper, Times.Never);
        }

        [Fact]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var mockMatchQuery = new Mock<IMatchQuery>();

            mockMatchQuery.Setup(matchQuery => matchQuery.FetchParticipantMatchesAsync(It.IsAny<ParticipantId>()))
                .ReturnsAsync(new Collection<IMatch>())
                .Verifiable();

            mockMatchQuery.SetupGet(matchQuery => matchQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ParticipantMatchesController(mockMatchQuery.Object);

            // Act
            var result = await controller.GetAsync(new ParticipantId());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockMatchQuery.Verify(matchQuery => matchQuery.FetchParticipantMatchesAsync(It.IsAny<ParticipantId>()), Times.Once);

            mockMatchQuery.VerifyGet(matchQuery => matchQuery.Mapper, Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(95632852, null, ChallengeState.InProgress);

            var challenge = challengeFaker.FakeChallenge();

            var participants = challenge.Participants;

            var participant = participants.First();

            var matches = participant.Matches;

            var mockMatchQuery = new Mock<IMatchQuery>();

            mockMatchQuery.Setup(matchQuery => matchQuery.FetchParticipantMatchesAsync(It.IsAny<ParticipantId>())).ReturnsAsync(matches).Verifiable();

            mockMatchQuery.SetupGet(matchQuery => matchQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ParticipantMatchesController(mockMatchQuery.Object);

            // Act
            var result = await controller.GetAsync(new ParticipantId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockMatchQuery.Verify(matchQuery => matchQuery.FetchParticipantMatchesAsync(It.IsAny<ParticipantId>()), Times.Once);

            mockMatchQuery.VerifyGet(matchQuery => matchQuery.Mapper, Times.Once);
        }
    }
}
