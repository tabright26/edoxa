// Filename: MatchTest.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Api.Application.Factories;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Providers;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class MatchTest
    {
        private static IEnumerable<object[]> GameDataSets => ChallengeGame.GetEnumerations().Select(game => new object[] {game});

        [DataTestMethod]
        [DynamicData(nameof(GameDataSets))]
        public void Snapshot_Stats_ShouldHaveCountOfScoring(ChallengeGame game)
        {
            // Arrange
            var faker = new Faker();
            var scoring = new ScoringFactory().CreateInstance(game).Scoring;
            var stats = faker.Game().Stats(game);

            // Act
            var match = new StatMatch(scoring, stats, faker.Game().Reference(game), new UtcNowDateTimeProvider());

            // Assert
            match.Stats.Should().HaveCount(scoring.Count);
        }
    }
}
