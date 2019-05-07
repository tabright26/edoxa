// Filename: ChallengeScoreboardFactoryTest.cs
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
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate.Factories;
using eDoxa.Seedwork.Enumerations;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Entities.Tests.AggregateModels.ChallengeAggregate.Factories
{
    [TestClass]
    public sealed class ChallengeScoreboardFactoryTest
    {
        [DataRow(ChallengeType.Default)]
        [DataTestMethod]
        public void Create_ImplementedType_ShouldNotBeNull(ChallengeType type)
        {
            // Arrange
            var factory = ChallengeScoreboardFactory.Instance;

            // Act
            var strategy = factory.CreateScoreboard(new MockChallenge(type));

            // Assert
            strategy.Scoreboard.Should().NotBeNull();
        }

        [DataRow(ChallengeType.None)]
        [DataRow(ChallengeType.All)]
        [DataTestMethod]
        public void Create_NotImplementedType_ShouldThrowArgumentException(ChallengeType type)
        {
            // Arrange
            var factory = ChallengeScoreboardFactory.Instance;

            // Act
            var action = new Action(() => factory.CreateScoreboard(new MockChallenge(type)));

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