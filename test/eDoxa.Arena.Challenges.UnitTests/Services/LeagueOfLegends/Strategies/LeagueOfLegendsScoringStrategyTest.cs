// Filename: LeagueOfLegendsScoringStrategyTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Strategies;
using eDoxa.Arena.Services.LeagueOfLegends.Dtos;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Services.LeagueOfLegends.Strategies
{
    [TestClass]
    public sealed class LeagueOfLegendsScoringStrategyTest
    {
        [DataRow(nameof(LeagueOfLegendsParticipantStatsDto.Kills), 4)]
        [DataRow(nameof(LeagueOfLegendsParticipantStatsDto.Deaths), -3)]
        [DataRow(nameof(LeagueOfLegendsParticipantStatsDto.Assists), 3)]
        [DataRow(nameof(LeagueOfLegendsParticipantStatsDto.TotalDamageDealtToChampions), 0.0008F)]
        [DataRow(nameof(LeagueOfLegendsParticipantStatsDto.TotalHeal), 0.0015F)]
        [DataTestMethod]
        public void Scoring_Default_ShouldContain(string key, float value)
        {
            // Act
            var strategy = new LeagueOfLegendsScoringStrategy();

            // Assert
            strategy.Scoring.As<Scoring>().Should().Contain(new StatName(key), new StatWeighting(value));
        }
    }
}
