// Filename: StatScoreTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.UnitTests.Utilities.Fakes;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.MatchAggregate
{
    [TestClass]
    public sealed class StatScoreTest
    {
        private static IEnumerable<object[]> Data
        {
            get
            {
                yield return new object[] {new StatName(FakeChallengeFactory.Kills), new StatValue(457000), new StatWeighting(0.00015F), new decimal(68.55D)};
                yield return new object[] {new StatName(FakeChallengeFactory.Assists), new StatValue(0.1F), new StatWeighting(1), new decimal(0.1D)};

                yield return new object[]
                {
                    new StatName(FakeChallengeFactory.Deaths), new StatValue(457342424L), new StatWeighting(0.77F), new decimal(352153666.48D)
                };

                yield return new object[]
                {
                    new StatName(FakeChallengeFactory.TotalDamageDealtToChampions), new StatValue(0.25D), new StatWeighting(100), new decimal(25D)
                };

                yield return new object[] {new StatName(FakeChallengeFactory.TotalHeal), new StatValue(85), new StatWeighting(-3), new decimal(-255D)};
            }
        }

        [DynamicData(nameof(Data))]
        [DataTestMethod]
        public void Constructor_Tests(
            StatName name,
            StatValue value,
            StatWeighting weighting,
            decimal score
        )
        {
            // Arrange
            var stat = new Stat(name, value, weighting);

            // Act
            decimal statScore = new StatScore(stat);

            // Assert
            statScore.Should().Be(score);
        }
    }
}
