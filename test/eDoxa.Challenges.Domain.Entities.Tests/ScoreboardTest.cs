// Filename: ScoreboardTest.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.Factories;
using eDoxa.Functional;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Entities.Tests
{
    [TestClass]
    public sealed class ScoreboardTest
    {
        private static readonly FakeDefaultChallengeFactory FakeDefaultChallengeFactory = FakeDefaultChallengeFactory.Instance;

        [TestMethod]
        public void Scoreboard_ShouldBeAssignableToType()
        {
            // Arrange
            var scoreboard = FakeDefaultChallengeFactory.CreateScoreboard();

            // Act
            var type = typeof(Dictionary<UserId, Option<Score>>);

            // Assert
            scoreboard.Should().BeAssignableTo(type);
        }
    }
}