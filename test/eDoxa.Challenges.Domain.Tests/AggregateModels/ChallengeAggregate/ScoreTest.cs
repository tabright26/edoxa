// Filename: ScoreTest.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ScoreTest
    {
        private static readonly ChallengeAggregateFactory _factory = ChallengeAggregateFactory.Instance;

        [DataRow(68.55D)]
        [DataRow(0.1D)]
        [DataRow(352153666.48D)]
        [DataRow(25D)]
        [DataRow(-255D)]
        [DataTestMethod]
        public void FromDouble_WithValidInput_ShouldNotBeNull(double input)
        {
            // Act
            var score = Score.FromDouble(input);

            // Assert
            score.Should().NotBeNull();
        }

        [DataRow(ChallengeAggregateFactory.Kills, 457000, 0.00015F, 68.55D)]
        [DataRow(ChallengeAggregateFactory.Assists, 0.1F, 1, 0.1D)]
        [DataRow(ChallengeAggregateFactory.Deaths, 457342424L, 0.77F, 352153666.48D)]
        [DataRow(ChallengeAggregateFactory.TotalDamageDealtToChampions, 0.25D, 100, 25D)]
        [DataRow(ChallengeAggregateFactory.TotalHeal, 85, -3, -255D)]
        [DataTestMethod]
        public void FromStat_WithValidArguments_ShouldBeResult(string name, double value, float weighting, double result)
        {
            // Arrange
            var stat = _factory.CreateStat(name, value, weighting);

            // Act
            var score = Score.FromStat(stat);

            // Assert
            score.Should().Be(Score.FromDouble(result));
        }
    }
}