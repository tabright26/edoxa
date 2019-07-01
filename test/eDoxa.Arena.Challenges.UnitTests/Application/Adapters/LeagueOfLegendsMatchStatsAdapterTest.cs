// Filename: LeagueOfLegendsMatchStatsAdapterTest.cs
// Date Created: 2019-06-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Application.Adapters;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.LeagueOfLegends.Abstractions;
using eDoxa.Arena.LeagueOfLegends.Dtos;
using eDoxa.Arena.UnitTests.Utilities.Stubs;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Adapters
{
    [TestClass]
    public sealed class LeagueOfLegendsMatchStatsAdapterTest
    {
        private Mock<ILeagueOfLegendsProxy> _mockLeagueOfLegendsProxy;

        private static LeagueOfLegendsMatchDto StubMatch =>
            JsonFileConvert.DeserializeObject<IEnumerable<LeagueOfLegendsMatchDto>>(@"Utilities/Stubs/LeagueOfLegends/Matches.json").First();

        [TestInitialize]
        public void TestInitialize()
        {
            _mockLeagueOfLegendsProxy = new Mock<ILeagueOfLegendsProxy>();

            _mockLeagueOfLegendsProxy.Setup(leagueOfLegendsProxy => leagueOfLegendsProxy.GetMatchAsync(It.IsNotNull<string>()))
                .ReturnsAsync(StubMatch)
                .Verifiable();
        }

        [TestMethod]
        public async Task GetMatchStatsAsync()
        {
            // Arrange
            var gameReference = new GameReference(StubMatch.GameId);

            var gameAccountId = StubMatch.ParticipantIdentities
                .Select(participantIdentity => new GameAccountId(participantIdentity.Player.AccountId.ToString()))
                .First();

            var participantId = StubMatch.ParticipantIdentities.Single(participantIdentity => participantIdentity.Player.AccountId == gameAccountId.ToString())
                .ParticipantId;

            var stats = StubMatch.Participants.Single(participant => participant.ParticipantId == participantId).Stats;
            var matchStatsAdapter = new LeagueOfLegendsMatchStatsAdapter(_mockLeagueOfLegendsProxy.Object);

            // Act
            var matchStats = await matchStatsAdapter.GetMatchStatsAsync(gameAccountId, gameReference);

            // Assert
            matchStatsAdapter.Game.Should().Be(ChallengeGame.LeagueOfLegends);
            matchStats.Should().BeEquivalentTo(new MatchStats(stats));
            _mockLeagueOfLegendsProxy.Verify(leagueOfLegendsProxy => leagueOfLegendsProxy.GetMatchAsync(It.IsNotNull<string>()), Times.Once);
        }
    }
}
