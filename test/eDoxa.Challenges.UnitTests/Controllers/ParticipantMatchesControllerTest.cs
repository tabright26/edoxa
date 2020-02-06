// Filename: ParticipantMatchesControllerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Controllers;
using eDoxa.Challenges.Domain.AggregateModels;
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
    public sealed class ParticipantMatchesControllerTest : UnitTest
    {
        public ParticipantMatchesControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator validator) : base(
            testData,
            testMapper,
            validator)
        {
        }

        [Fact]
        public async Task FetchParticipantMatchesAsync_ShouldBeNoContentResult()
        {
            // Arrange
            TestMock.MatchQuery.Setup(matchQuery => matchQuery.FetchParticipantMatchesAsync(It.IsAny<ParticipantId>()))
                .ReturnsAsync(new Collection<IMatch>())
                .Verifiable();

            var controller = new ParticipantMatchesController(TestMock.MatchQuery.Object, TestMapper);

            // Act
            var result = await controller.FetchParticipantMatchesAsync(new ParticipantId());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            TestMock.MatchQuery.Verify(matchQuery => matchQuery.FetchParticipantMatchesAsync(It.IsAny<ParticipantId>()), Times.Once);
        }

        [Fact]
        public async Task FetchParticipantMatchesAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(95632852, Game.LeagueOfLegends, ChallengeState.InProgress);

            var challenge = challengeFaker.FakeChallenge();

            var participants = challenge.Participants;

            var participant = participants.First();

            var matches = participant.Matches;

            TestMock.MatchQuery.Setup(matchQuery => matchQuery.FetchParticipantMatchesAsync(It.IsAny<ParticipantId>())).ReturnsAsync(matches).Verifiable();

            var controller = new ParticipantMatchesController(TestMock.MatchQuery.Object, TestMapper);

            // Act
            var result = await controller.FetchParticipantMatchesAsync(new ParticipantId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            TestMock.MatchQuery.Verify(matchQuery => matchQuery.FetchParticipantMatchesAsync(It.IsAny<ParticipantId>()), Times.Once);
        }
    }
}
