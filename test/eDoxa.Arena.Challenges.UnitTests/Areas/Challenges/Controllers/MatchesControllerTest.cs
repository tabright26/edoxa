// Filename: MatchesControllerTest.cs
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
    public sealed class MatchesControllerTest
    {
        [TestMethod]
        public async Task GetByIdAsync_ShouldBeOkObjectResult()
        {
            // Arrange        
            var challengeFaker = new ChallengeFaker(state: ChallengeState.InProgress);

            challengeFaker.UseSeed(89572992);

            var challenge = challengeFaker.Generate();

            var participants = challenge.Participants;

            var participant = participants.First();

            var matches = participant.Matches;

            var mockMatchQuery = new Mock<IMatchQuery>();

            mockMatchQuery.Setup(matchQuery => matchQuery.FindMatchAsync(It.IsAny<MatchId>())).ReturnsAsync(matches.First()).Verifiable();

            mockMatchQuery.SetupGet(matchQuery => matchQuery.Mapper).Returns(MapperExtensions.Mapper).Verifiable();

            var controller = new MatchesController(mockMatchQuery.Object);

            // Act
            var result = await controller.GetByIdAsync(new MatchId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockMatchQuery.Verify(matchQuery => matchQuery.FindMatchAsync(It.IsAny<MatchId>()), Times.Once);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            var mockMatchQuery = new Mock<IMatchQuery>();

            mockMatchQuery.Setup(matchQuery => matchQuery.FindMatchAsync(It.IsAny<MatchId>())).Verifiable();

            mockMatchQuery.SetupGet(matchQuery => matchQuery.Mapper).Returns(MapperExtensions.Mapper).Verifiable();

            var controller = new MatchesController(mockMatchQuery.Object);

            // Act
            var result = await controller.GetByIdAsync(new MatchId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockMatchQuery.Verify(matchQuery => matchQuery.FindMatchAsync(It.IsAny<MatchId>()), Times.Once);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var mockMatchQuery = new Mock<IMatchQuery>();

            mockMatchQuery.Setup(matchQuery => matchQuery.FindMatchAsync(It.IsAny<MatchId>())).Verifiable();

            mockMatchQuery.SetupGet(matchQuery => matchQuery.Mapper).Returns(MapperExtensions.Mapper).Verifiable();

            var controller = new MatchesController(mockMatchQuery.Object);

            controller.ControllerContext.ModelState.AddModelError("error", "error");

            // Act
            var result = await controller.GetByIdAsync(new MatchId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockMatchQuery.Verify(matchQuery => matchQuery.FindMatchAsync(It.IsAny<MatchId>()), Times.Never);
        }
    }
}
