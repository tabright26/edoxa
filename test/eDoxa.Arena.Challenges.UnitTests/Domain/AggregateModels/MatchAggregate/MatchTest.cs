// Filename: MatchTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Seedwork.Common.Enumerations;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.MatchAggregate
{
    [TestClass]
    public sealed class MatchTest
    {
        [TestMethod]
        public void SnapshotStats_Stats_ShouldHaveCountOfScoring()
        {
            // Arrange
            var matchReferenceFaker = new MatchReferenceFaker();

            var matchReference = matchReferenceFaker.FakeMatchReference(Game.LeagueOfLegends);

            var scoringFaker = new ScoringFaker();

            var scoring = scoringFaker.FakeMatchStats(Game.LeagueOfLegends);

            var matchStatsFaker = new MatchStatsFaker();

            var stats = matchStatsFaker.FakeMatchStats(Game.LeagueOfLegends);

            // Act
            var match = new Match(matchReference, stats, scoring);
            
            // Assert
            match.Stats.Should().HaveCount(scoring.Count);
        }
    }
}
