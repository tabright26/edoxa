// Filename: MatchTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Arena.Challenges.UnitTests.Utilities.Fakes;
using eDoxa.Seedwork.Common.Enumerations;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.MatchAggregate
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
            var scoringFaker = new ScoringFaker();

            var scoring = scoringFaker.FakeScoring(Game.LeagueOfLegends);

            var matchStatsFaker = new MatchStatsFaker();

            var stats = matchStatsFaker.FakeMatchStats(Game.LeagueOfLegends);

            var match = FakeChallengeFactory.CreateMatch();

            // Act
            match.SnapshotStats(stats, scoring);

            // Assert
            match.Stats.Should().HaveCount(scoring.Count);
        }
    }
}
