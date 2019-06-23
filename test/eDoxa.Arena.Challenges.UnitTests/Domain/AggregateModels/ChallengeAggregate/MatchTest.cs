// Filename: MatchTest.cs
// Date Created: 2019-06-01
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

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Arena.Challenges.Domain.Fakers.Extensions;
using eDoxa.Seedwork.Common;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class MatchTest
    {
        private static readonly Faker Faker = new Faker();

        private static IEnumerable<object[]> ChallengeGames => ChallengeGame.GetEnumerations().Select(game => new object[] { game });

        [DataTestMethod]
        [DynamicData(nameof(ChallengeGames))]
        public void SnapshotStats_Stats_ShouldHaveCountOfScoring(ChallengeGame game)
        {
            // Arrange
            var scoring = ScoringFactory.Instance.CreateStrategy(game).Scoring;

            var stats = Faker.Match().Stats(game);

            // Act
            var match = new Match(Faker.Match().GameId(game), new UtcNowDateTimeProvider());

            match.SnapshotStats(scoring, stats);

            // Assert
            match.Stats.Should().HaveCount(scoring.Count);
        }
    }
}
