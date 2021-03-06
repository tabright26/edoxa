﻿// Filename: MatchesControllerTest.cs
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
    public sealed class MatchesControllerTest : UnitTest
    {
        public MatchesControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator validator) : base(testData, testMapper, validator)
        {
        }

        [Fact]
        public async Task FindMatchAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            TestMock.MatchQuery.Setup(matchQuery => matchQuery.FindMatchAsync(It.IsAny<MatchId>())).Verifiable();

            var controller = new MatchesController(TestMock.MatchQuery.Object, TestMapper);

            // Act
            var result = await controller.FindMatchAsync(new MatchId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            TestMock.MatchQuery.Verify(matchQuery => matchQuery.FindMatchAsync(It.IsAny<MatchId>()), Times.Once);
        }

        [Fact]
        public async Task FindMatchAsync_ShouldBeOkObjectResult()
        {
            // Arrange        
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(89572992, Game.LeagueOfLegends, ChallengeState.InProgress);

            var challenge = challengeFaker.FakeChallenge();

            var participants = challenge.Participants;

            var participant = participants.First();

            var matches = participant.Matches;

            TestMock.MatchQuery.Setup(matchQuery => matchQuery.FindMatchAsync(It.IsAny<MatchId>())).ReturnsAsync(matches.First()).Verifiable();

            var controller = new MatchesController(TestMock.MatchQuery.Object, TestMapper);

            // Act
            var result = await controller.FindMatchAsync(new MatchId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            TestMock.MatchQuery.Verify(matchQuery => matchQuery.FindMatchAsync(It.IsAny<MatchId>()), Times.Once);
        }
    }
}
