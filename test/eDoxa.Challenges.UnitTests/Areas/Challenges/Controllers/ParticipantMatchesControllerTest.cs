// Filename: ParticipantMatchesControllerTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Areas.Challenges.Controllers;
using eDoxa.Challenges.Domain.AggregateModels;
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
    public sealed class ParticipantMatchesControllerTest : UnitTest
    {
        public ParticipantMatchesControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        //[Fact]
        //public async Task GetAsync_ShouldBeBadRequestObjectResult()
        //{
        //    // Arrange
        //    var mockMatchQuery = new Mock<IMatchQuery>();

        //    mockMatchQuery.Setup(matchQuery => matchQuery.FetchParticipantMatchesAsync(It.IsAny<ParticipantId>()))
        //        .ReturnsAsync(new Collection<IMatch>())
        //        .Verifiable();

        //    mockMatchQuery.SetupGet(matchQuery => matchQuery.Mapper).Returns(TestMapper).Verifiable();

        //    var controller = new ParticipantMatchesController(mockMatchQuery.Object);

        //    controller.ControllerContext.ModelState.AddModelError("error", "error");

        //    // Act
        //    var result = await controller.GetAsync(new ParticipantId());

        //    // Assert
        //    result.Should().BeOfType<BadRequestObjectResult>();

        //    mockMatchQuery.Verify(matchQuery => matchQuery.FetchParticipantMatchesAsync(It.IsAny<ParticipantId>()), Times.Never);

        //    mockMatchQuery.VerifyGet(matchQuery => matchQuery.Mapper, Times.Never);
        //}

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
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(95632852, Game.LeagueOfLegends, ChallengeState.InProgress);

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
