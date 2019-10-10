// Filename: MatchTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Factories;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.TestHelpers;
using eDoxa.Arena.Challenges.TestHelpers.Fixtures;
using eDoxa.Seedwork.Domain;

using FluentAssertions;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class MatchTest : UnitTest
    {
        public MatchTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        public static IEnumerable<object[]> GameDataSets => ChallengeGame.GetEnumerations().Select(game => new object[] {game}).ToList();

        [Theory]
        [MemberData(nameof(GameDataSets))]
        public void StatMatch_FromGame_ShouldHaveCountOfScoring(ChallengeGame game)
        {
            // Arrange
            var faker = new Faker();
            var scoring = new ScoringFactory().CreateInstance(game).Scoring;
            var stats = faker.Game().Stats(game);

            // Act
            var match = new StatMatch(
                scoring,
                stats,
                faker.Game().Reference(game),
                new UtcNowDateTimeProvider());

            // Assert
            match.Stats.Should().HaveCount(scoring.Count);
        }
    }
}
