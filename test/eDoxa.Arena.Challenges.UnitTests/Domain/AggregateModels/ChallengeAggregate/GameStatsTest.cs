// Filename: GameStatsTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using Bogus;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.TestHelper;
using eDoxa.Arena.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;
using FluentAssertions;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class GameStatsTest : UnitTest
    {
        public GameStatsTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        public static TheoryData<Game> GameDataSets =>
            new TheoryData<Game>
            {
                Game.LeagueOfLegends
            };

        [Theory]
        [MemberData(nameof(GameDataSets))]
        public void Stats_FromGame_ShouldBeAssignableToType(Game game)
        {
            // Arrange
            var stats = new Faker().Game().Stats(game);

            // Act
            var type = typeof(Dictionary<StatName, StatValue>);

            // Assert
            stats.Should().BeAssignableTo(type);
        }
    }
}
