// Filename: MatchTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Api.Application.Factories;
using eDoxa.Arena.Challenges.Api.Application.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

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
            var stats = faker.Match().Stats(game);
            var match = new Match(faker.Match().GameId(game), new UtcNowDateTimeProvider());

            // Act
            match.Snapshot(stats, scoring);

            // Assert
            match.Stats.Should().HaveCount(scoring.Count);
        }
    }
}
