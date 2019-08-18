// Filename: StatTest.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Api.Application.Factories;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class StatTest
    {
        private static IEnumerable<object[]> StatPropsDataSets =>
            ChallengeGame.GetEnumerations()
                .SelectMany(
                    game =>
                    {
                        var stats = new Faker().Game().Stats(game);

                        var factory = new ScoringFactory();

                        var strategy = factory.CreateInstance(game);

                        var match = new StatMatch(strategy.Scoring, stats, new GameReference(Guid.NewGuid()), new UtcNowDateTimeProvider());

                        return match.Stats;
                    }
                )
                .Select(stat => new object[] {stat.Name, stat.Value, stat.Weighting})
                .ToList();

        [DataTestMethod]
        [DynamicData(nameof(StatPropsDataSets))]
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
