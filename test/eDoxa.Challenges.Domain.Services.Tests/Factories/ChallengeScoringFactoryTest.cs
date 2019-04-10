// Filename: ChallengeScoringFactoryTest.cs
// Date Created: 2019-03-05
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Services.Factories;
using eDoxa.Seedwork.Domain.Common.Enums;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Services.Tests.Factories
{
    [TestClass]
    public sealed class ChallengeScoringFactoryTest
    {
        [DataRow(ChallengeType.Default, Game.LeagueOfLegends)]
        [DataTestMethod]
        public void Create_ImplementedType_ShouldNotBeNull(ChallengeType type, Game game)
        {
            // Arrange
            var factory = ChallengeScoringFactory.Instance;

            // Act
            var strategy = factory.Create(new MockChallenge(type, game));

            // Assert
            strategy.Scoring.Should().NotBeNull();
        }

        [DataRow(ChallengeType.Default, Game.CSGO)]
        [DataRow(ChallengeType.Default, Game.Fortnite)]
        [DataTestMethod]
        public void Create_NotImplementedType_ShouldThrowNotImplementedException(ChallengeType type, Game game)
        {
            // Arrange
            var factory = ChallengeScoringFactory.Instance;

            // Act
            var action = new Action(() => factory.Create(new MockChallenge(type, game)));

            // Assert
            action.Should().Throw<NotImplementedException>();
        }

        private sealed class MockChallenge : Challenge
        {
            public MockChallenge(ChallengeType type, Game game) : base(game, new ChallengeName(nameof(Challenge)))
            {
                Settings.SetPrivateField("_type", type);
            }
        }
    }
}