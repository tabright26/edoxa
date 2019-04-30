// Filename: MatchTest.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class MatchTest
    {
        private readonly ChallengeAggregateFactory _challengeAggregateFactory = ChallengeAggregateFactory.Instance;

        [TestMethod]
        public void Match_ShouldNotBeNull()
        {
            // Act
            var match = _challengeAggregateFactory.CreateMatch();

            // Assert
            match.Should().NotBeNull();
        }

        [TestMethod]
        public void SnapshotStats_Stats_ShouldHaveCountOfScoring()
        {
            // Arrange
            var scoring = _challengeAggregateFactory.CreateScoring();
            var stats = _challengeAggregateFactory.CreateMatchStats();
            var match = _challengeAggregateFactory.CreateMatch();

            // Act
            match.SnapshotStats(stats, scoring);

            // Assert
            match.Stats.Should().HaveCount(scoring.Count);
        }
    }
}