// Filename: StatTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
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
    public sealed class StatTest : UnitTest
    {
        public StatTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        public static IEnumerable<object[]> StatPropsDataSets =>
            ChallengeGame.GetEnumerations()
                .SelectMany(
                    game =>
                    {
                        var stats = new Faker().Game().Stats(game);

                        var factory = new ScoringFactory();

                        var strategy = factory.CreateInstance(game);

                        var match = new StatMatch(
                            strategy.Scoring,
                            stats,
                            new GameReference(Guid.NewGuid()),
                            new UtcNowDateTimeProvider());

                        return match.Stats;
                    })
                .Select(stat => new object[] {stat.Name, stat.Value, stat.Weighting})
                .ToList();

        [Theory]
        [MemberData(nameof(StatPropsDataSets))]
        public void Contructor_Tests(StatName name, StatValue value, StatWeighting weighting)
        {
            // Act
            var stat = new Stat(name, value, weighting);

            // Assert
            stat.Name.Should().Be(name);
            stat.Value.Should().Be(value);
            stat.Weighting.Should().Be(weighting);
            stat.Score.Should().NotBeNull();
        }
    }
}
