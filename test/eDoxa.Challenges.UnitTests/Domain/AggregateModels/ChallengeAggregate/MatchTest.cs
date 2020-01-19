// Filename: MatchTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

using Bogus;

using eDoxa.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;

using FluentAssertions;

using Xunit;

namespace eDoxa.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class MatchTest : UnitTest
    {
        public MatchTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator validator) : base(testData, testMapper, validator)
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
            var match = new Match(
                faker.Game().Uuid(),
                new UtcNowDateTimeProvider(),
                TimeSpan.FromSeconds(3600),
                scoring.Map(stats),
                new UtcNowDateTimeProvider());

            // Assert
            match.Stats.Should().HaveCount(scoring.Count);
        }
    }
}
