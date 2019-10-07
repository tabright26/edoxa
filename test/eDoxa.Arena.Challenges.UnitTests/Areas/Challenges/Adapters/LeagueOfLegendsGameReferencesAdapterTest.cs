// Filename: LeagueOfLegendsGameReferencesAdapterTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
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

using FluentAssertions;

using Moq;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Areas.Challenges.Adapters
{
    public sealed class LeagueOfLegendsGameReferencesAdapterTest : UnitTest
    {
        public LeagueOfLegendsGameReferencesAdapterTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        private LeagueOfLegendsMatchReferenceDto[] StubMatchReferences =>
            TestData.FileStorage.DeserializeJsonFile<IEnumerable<LeagueOfLegendsMatchReferenceDto>>(@"Stubs/LeagueOfLegends/MatchReferences.json").ToArray();

        [Fact]
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
