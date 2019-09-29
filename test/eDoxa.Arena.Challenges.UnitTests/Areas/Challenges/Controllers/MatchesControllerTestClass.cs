// Filename: MatchesControllerTestClass.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Controllers;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Arena.Challenges.UnitTests.TestHelpers;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Areas.Challenges.Controllers
{
    public sealed class MatchesControllerTestClass : UnitTestClass
    {
        public MatchesControllerTestClass(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task GetByIdAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var mockMatchQuery = new Mock<IMatchQuery>();

            mockMatchQuery.Setup(matchQuery => matchQuery.FindMatchAsync(It.IsAny<MatchId>())).Verifiable();

            mockMatchQuery.SetupGet(matchQuery => matchQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new MatchesController(mockMatchQuery.Object);

            controller.ControllerContext.ModelState.AddModelError("error", "error");

            // Act
            var result = await controller.GetByIdAsync(new MatchId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockMatchQuery.Verify(matchQuery => matchQuery.FindMatchAsync(It.IsAny<MatchId>()), Times.Never);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            var mockMatchQuery = new Mock<IMatchQuery>();

            mockMatchQuery.Setup(matchQuery => matchQuery.FindMatchAsync(It.IsAny<MatchId>())).Verifiable();

            mockMatchQuery.SetupGet(matchQuery => matchQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new MatchesController(mockMatchQuery.Object);

            // Act
            var result = await controller.GetByIdAsync(new MatchId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockMatchQuery.Verify(matchQuery => matchQuery.FindMatchAsync(It.IsAny<MatchId>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldBeOkObjectResult()
        {
            // Arrange        
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(89572992, null, ChallengeState.InProgress);

            var challenge = challengeFaker.FakeChallenge();

            var participants = challenge.Participants;

            var participant = participants.First();

            var matches = participant.Matches;

            var mockMatchQuery = new Mock<IMatchQuery>();

            mockMatchQuery.Setup(matchQuery => matchQuery.FindMatchAsync(It.IsAny<MatchId>())).ReturnsAsync(matches.First()).Verifiable();

            mockMatchQuery.SetupGet(matchQuery => matchQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new MatchesController(mockMatchQuery.Object);

            // Act
            var result = await controller.GetByIdAsync(new MatchId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockMatchQuery.Verify(matchQuery => matchQuery.FindMatchAsync(It.IsAny<MatchId>()), Times.Once);
        }
    }
}
