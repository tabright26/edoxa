// Filename: LeagueOfLegendsScoringStrategyTest.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Challenges.Api.Application.Strategies;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Games.LeagueOfLegends.Dtos;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Strategies
{
    [TestClass]
    public sealed class LeagueOfLegendsScoringStrategyTest
    {
        [DataTestMethod]
        [DataRow(nameof(LeagueOfLegendsParticipantStatsDto.Kills), 4)]
        [DataRow(nameof(LeagueOfLegendsParticipantStatsDto.Deaths), -3)]
        [DataRow(nameof(LeagueOfLegendsParticipantStatsDto.Assists), 3)]
        [DataRow(nameof(LeagueOfLegendsParticipantStatsDto.TotalDamageDealtToChampions), 0.0008F)]
        [DataRow(nameof(LeagueOfLegendsParticipantStatsDto.TotalHeal), 0.0015F)]
        public void LeagueOfLegendsScoringStrategy_ShouldContain(string key, float value)
        {
            // Act
            var strategy = new LeagueOfLegendsScoringStrategy();

            // Assert
            strategy.Scoring.As<Scoring>().Should().Contain(new StatName(key), new StatWeighting(value));
        }
    }
}
