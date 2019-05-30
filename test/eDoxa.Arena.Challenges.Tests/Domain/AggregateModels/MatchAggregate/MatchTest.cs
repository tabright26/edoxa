// Filename: MatchTest.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Tests.Utilities.Fakes;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.Tests.Domain.AggregateModels.MatchAggregate
{
    [TestClass]
    public sealed class MatchTest
    {
        private static readonly FakeChallengeFactory FakeChallengeFactory = FakeChallengeFactory.Instance;

        [TestMethod]
        public void Match_ShouldNotBeNull()
        {
            // Act
            var match = FakeChallengeFactory.CreateMatch();

            // Assert
            match.Should().NotBeNull();
        }

        [TestMethod]
        public void SnapshotStats_Stats_ShouldHaveCountOfScoring()
        {
            // Arrange
            var scoring = FakeChallengeFactory.CreateScoring();
            var stats = FakeChallengeFactory.CreateMatchStats();
            var match = FakeChallengeFactory.CreateMatch();

            // Act
            match.SnapshotStats(stats, scoring);

            // Assert
            match.Stats.Should().HaveCount(scoring.Count);
        }
    }
}
