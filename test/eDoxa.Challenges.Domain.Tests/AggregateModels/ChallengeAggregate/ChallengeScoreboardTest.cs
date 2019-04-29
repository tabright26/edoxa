// Filename: ChallengeScoreboardTest.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.Challenges.Domain.Factories;
using eDoxa.Functional.Maybe;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ChallengeScoreboardTest
    {
        private static readonly ChallengeAggregateFactory ChallengeAggregateFactory = ChallengeAggregateFactory.Instance;

        [TestMethod]
        public void Scoreboard_ShouldBeAssignableToType()
        {
            // Arrange
            var scoreboard = ChallengeAggregateFactory.CreateChallengeScoreboard();

            // Act
            var type = typeof(Dictionary<UserId, Option<Score>>);

            // Assert
            scoreboard.Should().BeAssignableTo(type);
        }
    }
}