// Filename: ChallengePrizeBreakdownTest.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Challenges.Domain.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ChallengePrizeBreakdownTest
    {
        private static readonly ChallengeAggregateFactory ChallengeAggregateFactory = ChallengeAggregateFactory.Instance;

        [TestMethod]
        public void PrizeBreakdown_ShouldBeAssignableToType()
        {
            // Arrange
            var prizeBreakdown = ChallengeAggregateFactory.CreateChallengePrizeBreakdown();

            // Act
            var type = typeof(Dictionary<string, decimal>);

            // Assert
            prizeBreakdown.Should().BeAssignableTo(type);
        }
    }
}