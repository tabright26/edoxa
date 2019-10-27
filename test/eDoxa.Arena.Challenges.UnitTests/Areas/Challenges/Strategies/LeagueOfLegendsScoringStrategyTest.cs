// Filename: LeagueOfLegendsScoringStrategyTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Strategies;
using eDoxa.Arena.Challenges.Api.Temp.LeagueOfLegends.Dtos;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.TestHelpers;
using eDoxa.Arena.Challenges.TestHelpers.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Areas.Challenges.Strategies
{
    public sealed class LeagueOfLegendsScoringStrategyTest : UnitTest
    {
        public LeagueOfLegendsScoringStrategyTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Theory]
        [InlineData(nameof(LeagueOfLegendsParticipantStatsDto.Kills), 4)]
        [InlineData(nameof(LeagueOfLegendsParticipantStatsDto.Deaths), -3)]
        [InlineData(nameof(LeagueOfLegendsParticipantStatsDto.Assists), 3)]
        [InlineData(nameof(LeagueOfLegendsParticipantStatsDto.TotalDamageDealtToChampions), 0.0008F)]
        [InlineData(nameof(LeagueOfLegendsParticipantStatsDto.TotalHeal), 0.0015F)]
        public void LeagueOfLegendsScoringStrategy_ShouldContain(string key, float value)
        {
            // Act
            var strategy = new LeagueOfLegendsScoringStrategy();

            // Assert
            strategy.Scoring.As<Scoring>().Should().Contain(new StatName(key), new StatWeighting(value));
        }
    }
}
