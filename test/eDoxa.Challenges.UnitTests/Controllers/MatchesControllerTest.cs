﻿// Filename: MatchesControllerTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
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
    public sealed class MatchesControllerTest : UnitTest
    {
        public MatchesControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator validator) : base(testData, testMapper, validator)
        {
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
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(89572992, Game.LeagueOfLegends, ChallengeState.InProgress);

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