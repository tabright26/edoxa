// Filename: StatScoreTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.TestHelpers;
using eDoxa.Arena.Challenges.TestHelpers.Fixtures;
using eDoxa.Arena.Games.LeagueOfLegends.Dtos;

using FluentAssertions;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class StatScoreTest : UnitTest
    {
        public StatScoreTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        public static IEnumerable<object[]> StatScoreDataSets
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

        [Theory]
        [MemberData(nameof(StatScoreDataSets))]
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
            var statScore = new StatScore(stat);

            // Assert
            score.Should().Be(statScore.ToDecimal());
        }
    }
}
