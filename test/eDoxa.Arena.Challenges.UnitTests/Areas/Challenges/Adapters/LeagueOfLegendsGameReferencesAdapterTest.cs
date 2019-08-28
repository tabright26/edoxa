// Filename: LeagueOfLegendsGameReferencesAdapterTest.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Adapters;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.UnitTests.Helpers;
using eDoxa.Arena.Games.LeagueOfLegends.Abstractions;
using eDoxa.Arena.Games.LeagueOfLegends.Dtos;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Areas.Challenges.Adapters
{
    [TestClass]
    public sealed class LeagueOfLegendsGameReferencesAdapterTest
    {
        private static LeagueOfLegendsMatchReferenceDto[] StubMatchReferences =>
            JsonFileConvert.DeserializeObject<IEnumerable<LeagueOfLegendsMatchReferenceDto>>(@"Helpers/Stubs/LeagueOfLegends/MatchReferences.json").ToArray();

        [TestMethod]
        public async Task GetGameReferencesAsync_WhenMatchReferenceTimestampIsBetweenRange_ShouldBeLeagueOfLegends()
        {
            // Arrange
            var mockLeagueOfLegendsProxy = new Mock<ILeagueOfLegendsProxy>();

            mockLeagueOfLegendsProxy
                .Setup(leagueOfLegendsProxy => leagueOfLegendsProxy.GetMatchReferencesAsync(It.IsNotNull<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(StubMatchReferences)
                .Verifiable();

            var gameAccountId = new GameAccountId("NzH50JS-LCAu0UEY4EMjuS710F_U_8pLfEpNib9X06dD4w");
            var timestamps = StubMatchReferences.Select(matchReference => DateTimeOffset.FromUnixTimeMilliseconds(matchReference.Timestamp)).ToList();
            var startedAt = timestamps.Min(matchReference => matchReference.DateTime);
            var endedAt = timestamps.Max(matchReference => matchReference.DateTime);
            var matchReferencesAdapter = new LeagueOfLegendsGameReferencesAdapter(mockLeagueOfLegendsProxy.Object);

            // Act
            var matchReferences = await matchReferencesAdapter.GetGameReferencesAsync(gameAccountId, startedAt, endedAt);

            // Assert
            matchReferencesAdapter.Game.Should().Be(ChallengeGame.LeagueOfLegends);
            matchReferences.Should().BeEquivalentTo(StubMatchReferences.Select(matchReference => new GameReference(matchReference.GameId)));

            mockLeagueOfLegendsProxy.Verify(
                leagueOfLegendsProxy => leagueOfLegendsProxy.GetMatchReferencesAsync(It.IsNotNull<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()),
                Times.Once);
        }
    }
}
