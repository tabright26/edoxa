// Filename: MatchStatsTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Api.Application.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class GameStatsTest
    {
        private static IEnumerable<object[]> GameDataSets => ChallengeGame.GetEnumerations().Select(game => new object[] {game});

        [DataTestMethod]
        [DynamicData(nameof(GameDataSets))]
        public void Stats_ShouldBeAssignableToType(ChallengeGame game)
        {
            // Arrange
            var stats = new Faker().Match().Stats(game);

            // Act
            var type = typeof(Dictionary<StatName, StatValue>);

            // Assert
            stats.Should().BeAssignableTo(type);
        }
    }
}
