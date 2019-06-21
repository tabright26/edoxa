// Filename: StatScoreTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Services.LeagueOfLegends.Dtos;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.MatchAggregate
{
    [TestClass]
    public sealed class StatScoreTest
    {
        private static IEnumerable<object[]> Data
        {
            get
            {
                yield return new object[]
                {
                    new StatName(nameof(LeagueOfLegendsParticipantStatsDto.Kills)), new StatValue(457000), new StatWeighting(0.00015F), 68.5500032559503M
                };

                yield return new object[]
                {
                    new StatName(nameof(LeagueOfLegendsParticipantStatsDto.Assists)), new StatValue(0.1F), new StatWeighting(1), 0.100000001490116M
                };

                yield return new object[]
                {
                    new StatName(nameof(LeagueOfLegendsParticipantStatsDto.Deaths)), new StatValue(457342424L), new StatWeighting(0.77F), 352153657.756886M
                };

                yield return new object[]
                {
                    new StatName(nameof(LeagueOfLegendsParticipantStatsDto.TotalDamageDealtToChampions)), new StatValue(0.25D), new StatWeighting(100), 25M
                };

                yield return new object[] {new StatName(nameof(LeagueOfLegendsParticipantStatsDto.TotalHeal)), new StatValue(85), new StatWeighting(-3), -255M};
            }
        }

        [DynamicData(nameof(Data))]
        [DataTestMethod]
        public void Constructor_Tests(
            StatName name,
            StatValue value,
            StatWeighting weighting,
            decimal score
        )
        {
            // Arrange
            var stat = new Stat(name, value, weighting);

            // Act
            decimal statScore = new StatScore(stat);

            // Assert
            statScore.Should().Be(score);
        }
    }
}
