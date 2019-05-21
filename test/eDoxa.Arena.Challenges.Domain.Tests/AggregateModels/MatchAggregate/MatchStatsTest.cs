// Filename: MatchStatsTest.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Tests.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.Domain.Tests.AggregateModels.MatchAggregate
{
    [TestClass]
    public sealed class MatchStatsTest
    {
        private static readonly FakeChallengeFactory FakeChallengeFactory = FakeChallengeFactory.Instance;

        [TestMethod]
        public void Stats_ShouldBeAssignableToType()
        {
            // Arrange
            var stats = FakeChallengeFactory.CreateMatchStats();

            // Act
            var type = typeof(Dictionary<StatName, StatValue>);

            // Assert
            stats.Should().BeAssignableTo(type);
        }
    }
}