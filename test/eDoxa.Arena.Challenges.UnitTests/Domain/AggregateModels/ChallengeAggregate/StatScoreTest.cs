// Filename: StatScoreTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Challenges.Api.Temp.LeagueOfLegends.Dtos;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.TestHelper;
using eDoxa.Arena.Challenges.TestHelper.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class StatScoreTest : UnitTest
    {
        public StatScoreTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        public static TheoryData<StatName, StatValue, StatWeighting, decimal> StatScoreDataSets =>
            new TheoryData<StatName, StatValue, StatWeighting, decimal>
            {
                {new StatName(nameof(LeagueOfLegendsParticipantStatsDto.Kills)), new StatValue(457000), new StatWeighting(0.00015F), 68.5500032559503M},
                {new StatName(nameof(LeagueOfLegendsParticipantStatsDto.Assists)), new StatValue(0.1F), new StatWeighting(1), 0.100000001490116M},
                {new StatName(nameof(LeagueOfLegendsParticipantStatsDto.Deaths)), new StatValue(457342424L), new StatWeighting(0.77F), 352153657.756886M},
                {new StatName(nameof(LeagueOfLegendsParticipantStatsDto.TotalDamageDealtToChampions)), new StatValue(0.25D), new StatWeighting(100), 25M},
                {new StatName(nameof(LeagueOfLegendsParticipantStatsDto.TotalHeal)), new StatValue(85), new StatWeighting(-3), -255M}
            };

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
