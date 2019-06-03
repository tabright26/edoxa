﻿// Filename: LeagueOfLegendsScoringStrategyTest.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Services.LeagueOfLegends.Strategies;
using eDoxa.Arena.Services.LeagueOfLegends.DTO;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.Tests.Services.LeagueOfLegends.Strategies
{
    [TestClass]
    public sealed class LeagueOfLegendsScoringStrategyTest
    {
        [DataRow(nameof(LeagueOfLegendsParticipantStatsDTO.Kills), 4)]
        [DataRow(nameof(LeagueOfLegendsParticipantStatsDTO.Deaths), -3)]
        [DataRow(nameof(LeagueOfLegendsParticipantStatsDTO.Assists), 3)]
        [DataRow(nameof(LeagueOfLegendsParticipantStatsDTO.TotalDamageDealtToChampions), 0.0008F)]
        [DataRow(nameof(LeagueOfLegendsParticipantStatsDTO.TotalHeal), 0.0015F)]
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
