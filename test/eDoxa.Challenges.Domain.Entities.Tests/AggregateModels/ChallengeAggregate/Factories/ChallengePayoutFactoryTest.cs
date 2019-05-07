// Filename: ChallengePayoutFactoryTest.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Entities.Default;
using eDoxa.Challenges.Domain.Services.Factories;
using eDoxa.Seedwork.Enumerations;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Entities.Tests.AggregateModels.ChallengeAggregate.Factories
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
            var factory = PayoutFactory.Instance;

            // Act
            var strategy = factory.CreatePayout(challenge.Setup.Type, challenge.Setup.PayoutEntries, challenge.Setup.PrizePool,
                challenge.Setup.EntryFee);

            // Assert
            strategy.Payout.Should().NotBeNull();
        }

        [DataRow(ChallengeType.None)]
        [DataRow(ChallengeType.All)]
        [DataTestMethod]
        public void Create_NotImplementedType_ShouldThrowArgumentException(ChallengeType type)
        {
            // Arrange
            var challenge = new MockChallenge(type);
            var factory = PayoutFactory.Instance;

            // Act
            var action = new Action(() =>
                factory.CreatePayout(challenge.Setup.Type, challenge.Setup.PayoutEntries, challenge.Setup.PrizePool, challenge.Setup.EntryFee));

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        private sealed class MockChallenge : Challenge
        {
            public MockChallenge(ChallengeType type) : base(Game.LeagueOfLegends, new ChallengeName(nameof(Challenge)), new DefaultChallengeSetup())
            {
                Setup.SetPrivateField("_type", type);
            }
        }
    }
}