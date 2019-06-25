// Filename: ScoringTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Api.Application.Factories;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ScoringTest
    {
        private static IEnumerable<object[]> ChallengeGames => ChallengeGame.GetEnumerations().Select(game => new object[] {game});

        [DataTestMethod]
        [DynamicData(nameof(ChallengeGames))]
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
