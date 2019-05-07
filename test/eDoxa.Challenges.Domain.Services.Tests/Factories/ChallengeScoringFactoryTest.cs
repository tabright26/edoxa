﻿// Filename: ChallengeScoringFactoryTest.cs
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
using eDoxa.Challenges.Domain.Services.Factories;
using eDoxa.Seedwork.Enumerations;
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
            var factory = ScoringFactory.Instance;

            // Act
            var strategy = factory.CreateScoringStrategy(new MockChallenge(type, game));

            // Assert
            strategy.Scoring.Should().NotBeNull();
        }

        [DataRow(ChallengeType.Default, Game.CSGO)]
        [DataRow(ChallengeType.Default, Game.Fortnite)]
        [DataTestMethod]
        public void Create_NotImplementedType_ShouldThrowNotImplementedException(ChallengeType type, Game game)
        {
            // Arrange
            var factory = ScoringFactory.Instance;

            // Act
            var action = new Action(() => factory.CreateScoringStrategy(new MockChallenge(type, game)));

            // Assert
            action.Should().Throw<NotImplementedException>();
        }

        private sealed class MockChallenge : Challenge
        {
            public MockChallenge(ChallengeType type, Game game) : base(game, new ChallengeName(nameof(Challenge)), new DefaultChallengeSetup())
            {
                Setup.SetPrivateField("_type", type);
            }
        }
    }
}