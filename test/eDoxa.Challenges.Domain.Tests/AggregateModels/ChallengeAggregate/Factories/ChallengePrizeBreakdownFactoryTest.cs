// Filename: ChallengePrizeBreakdownFactoryTest.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
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
    public sealed class ChallengePrizeBreakdownFactoryTest
    {
        [DataRow(ChallengeType.Default)]
        [DataTestMethod]
        public void Create_ImplementedType_ShouldNotBeNull(ChallengeType type)
        {
            // Arrange
            var challenge = new MockChallenge(type);
            var factory = ChallengePrizeBreakdownFactory.Instance;

            // Act
            var strategy = factory.Create(challenge.Settings.Type, challenge.Settings.PayoutEntries.ToInt32(), challenge.Settings.PrizePool.ToDecimal());

            // Assert
            strategy.PrizeBreakdown.Should().NotBeNull();
        }

        [DataRow(ChallengeType.None)]
        [DataRow(ChallengeType.All)]
        [DataTestMethod]
        public void Create_NotImplementedType_ShouldThrowNotImplementedException(ChallengeType type)
        {
            // Arrange
            var challenge = new MockChallenge(type);
            var factory = ChallengePrizeBreakdownFactory.Instance;

            // Act
            var action = new Action(() => factory.Create(challenge.Settings.Type, challenge.Settings.PayoutEntries.ToInt32(), challenge.Settings.PrizePool.ToDecimal()));

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