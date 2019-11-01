// Filename: MatchTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Bogus;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.TestHelper;
using eDoxa.Arena.Challenges.TestHelper.Fixtures;
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

        [Fact]
        public void StatMatch_FromGame_ShouldHaveCountOfScoring()
        {
            // Arrange
            var faker = new Faker();
            var scoring = new Scoring
            {
                [new StatName("StatName1")] = new StatWeighting(0.00015F),
                [new StatName("StatName2")] = new StatWeighting(1),
                [new StatName("StatName3")] = new StatWeighting(0.77F),
                [new StatName("StatName4")] = new StatWeighting(100),
                [new StatName("StatName5")] = new StatWeighting(-3)
            };
            var stats = faker.Game().Stats();

            // Act
            var match = new StatMatch(
                scoring,
                stats,
                faker.Game().Reference(),
                new UtcNowDateTimeProvider());

            // Assert
            match.Stats.Should().HaveCount(scoring.Count);
        }
    }
}
