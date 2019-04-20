// Filename: ChallengePayoutFactoryTest.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Factories;
using eDoxa.Seedwork.Domain.Common.Enums;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate.Factories
{
    [TestClass]
    public sealed class ChallengePayoutFactoryTest
    {
        [DataRow(ChallengeType.Default)]
        [DataTestMethod]
        public void Create_ImplementedType_ShouldNotBeNull(ChallengeType type)
        {
            // Arrange
            var challenge = new MockChallenge(type);
            var factory = ChallengePayoutFactory.Instance;

            // Act
            var strategy = factory.CreatePayout(challenge.Settings.Type, challenge.Settings.PayoutEntries, challenge.Settings.PrizePool);

            // Assert
            strategy.Payout.Should().NotBeNull();
        }

        [DataRow(ChallengeType.None)]
        [DataRow(ChallengeType.All)]
        [DataTestMethod]
        public void Create_NotImplementedType_ShouldThrowNotImplementedException(ChallengeType type)
        {
            // Arrange
            var challenge = new MockChallenge(type);
            var factory = ChallengePayoutFactory.Instance;

            // Act
            var action = new Action(() =>
                factory.CreatePayout(challenge.Settings.Type, challenge.Settings.PayoutEntries, challenge.Settings.PrizePool));

            // Assert
            action.Should().Throw<NotImplementedException>();
        }

        private sealed class MockChallenge : Challenge
        {
            public MockChallenge(ChallengeType type) : base(Game.LeagueOfLegends, new ChallengeName(nameof(Challenge)))
            {
                Settings.SetPrivateField("_type", type);
            }
        }
    }
}