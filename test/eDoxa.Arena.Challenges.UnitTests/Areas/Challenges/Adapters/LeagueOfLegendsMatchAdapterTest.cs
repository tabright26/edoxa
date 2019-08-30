﻿// Filename: LeagueOfLegendsMatchAdapterTest.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Adapters;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.UnitTests.Helpers;
using eDoxa.Arena.Games.LeagueOfLegends.Abstractions;
using eDoxa.Arena.Games.LeagueOfLegends.Dtos;
using eDoxa.Seedwork.Domain;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Areas.Challenges.Adapters
{
    [TestClass]
    public sealed class LeagueOfLegendsMatchAdapterTest
    {
        private Mock<ILeagueOfLegendsProxy> _mockLeagueOfLegendsProxy;

        private static LeagueOfLegendsMatchDto StubMatch =>
            JsonFileConvert.DeserializeObject<IEnumerable<LeagueOfLegendsMatchDto>>(@"Helpers/Stubs/LeagueOfLegends/Matches.json").First();

        [TestInitialize]
        public void TestInitialize()
        {
            _mockLeagueOfLegendsProxy = new Mock<ILeagueOfLegendsProxy>();

            _mockLeagueOfLegendsProxy.Setup(leagueOfLegendsProxy => leagueOfLegendsProxy.GetMatchAsync(It.IsNotNull<string>()))
                .ReturnsAsync(StubMatch)
                .Verifiable();
        }

        [TestMethod]
        public async Task GetMatchAsync_WhenGameAccountIdIsParticipant_ShouldBeLeagueOfLegends()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(ChallengeGame.LeagueOfLegends, ChallengeState.InProgress);
            challengeFaker.UseSeed(24788394);
            var challenge = challengeFaker.Generate();

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