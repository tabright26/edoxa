﻿// Filename: LeagueOfLegendsScoringStrategyTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Strategies;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.UnitTests.Helpers;
using eDoxa.Arena.Games.LeagueOfLegends.Dtos;

using FluentAssertions;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Areas.Challenges.Strategies
{
    public sealed class LeagueOfLegendsScoringStrategyTest : UnitTest
    {
        public LeagueOfLegendsScoringStrategyTest(ChallengeFakerFixture challengeFaker) : base(challengeFaker)
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
