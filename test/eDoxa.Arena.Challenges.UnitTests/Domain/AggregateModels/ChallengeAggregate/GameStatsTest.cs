// Filename: GameStatsTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.UnitTests.Helpers;

using FluentAssertions;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class GameStatsTest : UnitTest
    {
        public GameStatsTest(ChallengeFakerFixture challengeFaker) : base(challengeFaker)
        {
        }

        public static IEnumerable<object[]> GameDataSets => ChallengeGame.GetEnumerations().Select(game => new object[] {game}).ToList();

        [Theory]
        [MemberData(nameof(GameDataSets))]
        public void Stats_FromGame_ShouldBeAssignableToType(ChallengeGame game)
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
