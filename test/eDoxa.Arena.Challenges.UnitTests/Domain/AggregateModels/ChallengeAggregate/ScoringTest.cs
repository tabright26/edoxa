// Filename: ScoringTest.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Factories;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ScoringTest
    {
        private static IEnumerable<object[]> GameDataSets => ChallengeGame.GetEnumerations().Select(game => new object[] {game});

        [DataTestMethod]
        [DynamicData(nameof(GameDataSets))]
        public void Scoring_ShouldBeAssignableToType(ChallengeGame game)
        {
            // Arrange
            var scoring = new ScoringFactory().CreateInstance(game).Scoring;

            // Act
            var type = typeof(Dictionary<StatName, StatWeighting>);

            // Assert
            scoring.Should().BeAssignableTo(type);
        }
    }
}
