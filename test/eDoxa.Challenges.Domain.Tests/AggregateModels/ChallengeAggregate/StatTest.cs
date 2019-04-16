// Filename: StatTest.cs
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
    public sealed class StatTest
    {
        private static readonly ChallengeAggregateFactory _factory = ChallengeAggregateFactory.Instance;

        [DataRow(ChallengeAggregateFactory.Kills, 457000, 0.00015F)]
        [DataRow(ChallengeAggregateFactory.Assists, 0.1F, 1)]
        [DataRow(ChallengeAggregateFactory.Deaths, 457342424L, 0.77F)]
        [DataRow(ChallengeAggregateFactory.TotalDamageDealtToChampions, 0.25D, 100)]
        [DataRow(ChallengeAggregateFactory.TotalHeal, 85, -3)]
        [DataTestMethod]
        public void Stat_Score_ShouldNotBeNull(string name, double value, float weighting)
        {
            // Act
            var stat = _factory.CreateStat(name, value, weighting);

            // Assert
            stat.Name.Should().Be(name);
            stat.Value.Should().Be(value);
            stat.Weighting.Should().Be(weighting);
            stat.Score.Should().NotBeNull();
        }
    }
}