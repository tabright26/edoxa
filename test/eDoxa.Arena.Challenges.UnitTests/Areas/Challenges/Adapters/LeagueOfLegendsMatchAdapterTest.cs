// Filename: LeagueOfLegendsMatchAdapterTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Adapters;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.TestHelpers;
using eDoxa.Arena.Challenges.TestHelpers.Extensions;
using eDoxa.Arena.Challenges.TestHelpers.Fixtures;
using eDoxa.Arena.Games.LeagueOfLegends.Abstractions;
using eDoxa.Arena.Games.LeagueOfLegends.Dtos;
using eDoxa.Seedwork.Domain;

using FluentAssertions;

using Moq;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Areas.Challenges.Adapters
{
    public sealed class LeagueOfLegendsMatchAdapterTest : UnitTest
    {
        public LeagueOfLegendsMatchAdapterTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
            _mockLeagueOfLegendsProxy = new Mock<ILeagueOfLegendsService>();

            _mockLeagueOfLegendsProxy.Setup(leagueOfLegendsProxy => leagueOfLegendsProxy.GetMatchAsync(It.IsNotNull<string>()))
                .ReturnsAsync(StubMatch)
                .Verifiable();
        }

        private LeagueOfLegendsMatchDto StubMatch =>
            TestData.FileStorage.DeserializeJsonFile<IEnumerable<LeagueOfLegendsMatchDto>>(@"Stubs/LeagueOfLegends/Matches.json").First();

        private readonly Mock<ILeagueOfLegendsService> _mockLeagueOfLegendsProxy;

        [Fact]
        public async Task GetMatchAsync_WhenGameAccountIdIsParticipant_ShouldBeLeagueOfLegends()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(24788394, ChallengeGame.LeagueOfLegends, ChallengeState.InProgress);

            var challenge = challengeFaker.FakeChallenge();

            var synchronizedAt = new UtcNowDateTimeProvider();

            var gameReference = new GameReference(StubMatch.GameId);

            var gameAccountId = StubMatch.ParticipantIdentities
                .Select(participantIdentity => new GameAccountId(participantIdentity.Player.AccountId.ToString()))
                .First();

            var participantId = StubMatch.ParticipantIdentities.Single(participantIdentity => participantIdentity.Player.AccountId == gameAccountId.ToString())
                .ParticipantId;

            var stats = StubMatch.Participants.Single(participant => participant.ParticipantId == participantId).Stats;
            var matchAdapter = new LeagueOfLegendsMatchAdapter(_mockLeagueOfLegendsProxy.Object);

            // Act
            var match = await matchAdapter.GetMatchAsync(
                gameAccountId,
                gameReference,
                challenge.Scoring,
                synchronizedAt);

            // Assert
            var expectedMatch = new StatMatch(
                challenge.Scoring,
                new GameStats(stats),
                gameReference,
                synchronizedAt);

            matchAdapter.Game.Should().Be(ChallengeGame.LeagueOfLegends);
            match.Stats.Should().BeEquivalentTo(expectedMatch.Stats);
            _mockLeagueOfLegendsProxy.Verify(leagueOfLegendsProxy => leagueOfLegendsProxy.GetMatchAsync(It.IsNotNull<string>()), Times.Once);
        }
    }
}
