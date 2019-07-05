// Filename: LeagueOfLegendsScoringStrategyTest.cs
// Date Created: 2019-06-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Api.Application.Strategies;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.LeagueOfLegends.Dtos;

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
        public void Scoring_Strategy_ShouldContain(string key, float value)
        {
            // Act
            var strategy = new LeagueOfLegendsScoringStrategy();

            // Assert
            strategy.Scoring.As<Scoring>().Should().Contain(new StatName(key), new StatWeighting(value));
        }
    }
}
