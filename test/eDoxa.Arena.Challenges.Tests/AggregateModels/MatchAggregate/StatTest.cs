// Filename: StatTest.cs
// Date Created: 2019-05-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Tests.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.Tests.AggregateModels.MatchAggregate
{
    [TestClass]
    public sealed class StatTest
    {
        private static readonly FakeChallengeFactory FakeChallengeFactory = FakeChallengeFactory.Instance;

        [DataRow(FakeChallengeFactory.Kills, 457000, 0.00015F)]
        [DataRow(FakeChallengeFactory.Assists, 0.1F, 1)]
        [DataRow(FakeChallengeFactory.Deaths, 457342424L, 0.77F)]
        [DataRow(FakeChallengeFactory.TotalDamageDealtToChampions, 0.25D, 100)]
        [DataRow(FakeChallengeFactory.TotalHeal, 85, -3)]
        [DataTestMethod]
        public void Stat_Score_ShouldNotBeNull(string name, double value, float weighting)
        {
            // Arrange
            var statValue = new StatValue(value);
            var statWeighting = new StatWeighting(weighting);

            // Act
            var stat = FakeChallengeFactory.CreateStat(name, statValue, statWeighting);

            // Assert
            stat.Name.Should().Be(name);
            stat.Value.Should().Be(statValue);
            stat.Weighting.Should().Be(statWeighting);
            stat.Score.Should().NotBeNull();
        }
    }
}
