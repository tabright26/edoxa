// Filename: MatchStatsTest.cs
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
using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Seedwork.Common.Enumerations;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.MatchAggregate
{
    [TestClass]
    public sealed class MatchStatsTest
    {
        [TestMethod]
        public void Stats_ShouldBeAssignableToType()
        {
            // Arrange
            var matchStatsFaker = new MatchStatsFaker();

            var stats = matchStatsFaker.FakeMatchStats(Game.LeagueOfLegends);

            // Act
            var type = typeof(Dictionary<StatName, StatValue>);

            // Assert
            stats.Should().BeAssignableTo(type);
        }
    }
}
