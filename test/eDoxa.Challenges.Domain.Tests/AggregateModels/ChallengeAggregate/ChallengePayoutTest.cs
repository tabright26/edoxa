// Filename: ChallengePayoutTest.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Challenges.Domain.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ChallengePayoutTest
    {
        private static readonly ChallengeAggregateFactory ChallengeAggregateFactory = ChallengeAggregateFactory.Instance;

        [TestMethod]
        public void Payout_ShouldBeAssignableToType()
        {
            // Arrange
            var payout = ChallengeAggregateFactory.CreateChallengePayout();

            // Act
            var type = typeof(Dictionary<string, decimal>);

            // Assert
            payout.Should().BeAssignableTo(type);
        }
    }
}