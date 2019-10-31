// Filename: MatchTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Bogus;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Factories;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.TestHelpers;
using eDoxa.Arena.Challenges.TestHelpers.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class MatchTest : UnitTest
    {
        public MatchTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        public static TheoryData<Game> GameDataSets =>
            new TheoryData<Game>
            {
                Game.LeagueOfLegends
            };

        [Theory]
        [MemberData(nameof(GameDataSets))]
        public void StatMatch_FromGame_ShouldHaveCountOfScoring(Game game)
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
