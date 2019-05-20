// Filename: StatScoreTest.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Challenges.Domain.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.MatchAggregate
{
    [TestClass]
    public sealed class StatScoreTest
    {
        private static readonly FakeDefaultChallengeFactory FakeDefaultChallengeFactory = FakeDefaultChallengeFactory.Instance;

        [DataRow(FakeDefaultChallengeFactory.Kills, 457000, 0.00015F, 68.55D)]
        [DataRow(FakeDefaultChallengeFactory.Assists, 0.1F, 1, 0.1D)]
        [DataRow(FakeDefaultChallengeFactory.Deaths, 457342424L, 0.77F, 352153666.48D)]
        [DataRow(FakeDefaultChallengeFactory.TotalDamageDealtToChampions, 0.25D, 100, 25D)]
        [DataRow(FakeDefaultChallengeFactory.TotalHeal, 85, -3, -255D)]
        [DataTestMethod]
        public void Constructor_Tests(string name, double value, float weighting, double result)
        {
            // Arrange
            var stat = FakeDefaultChallengeFactory.CreateStat(name, new StatValue(value), new StatWeighting(weighting));

            // Act
            decimal score = new StatScore(stat);

            // Assert
            score.Should().Be(new decimal(result));
        }
    }
}