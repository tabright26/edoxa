// Filename: LeagueOfLegendsChallengeScoringFactoryTest.cs
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
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.Factories;
using eDoxa.Seedwork.Domain.Common.Enums;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.Tests.Factories
{
    [TestClass]
    public sealed class LeagueOfLegendsChallengeScoringFactoryTest
    {
        [DataRow(ChallengeType.Default)]
        [DataTestMethod]
        public void Create_ImplementedType_ShouldNotBeNull(ChallengeType type)
        {
            // Arrange
            var factory = LeagueOfLegendsChallengeScoringFactory.Instance;

            // Act
            var strategy = factory.Create(new MockChallenge(type));

            // Assert
            strategy.Scoring.Should().NotBeNull();
        }

        [DataRow(ChallengeType.None)]
        [DataRow(ChallengeType.All)]
        [DataTestMethod]
        public void Create_NotImplementedType_ShouldThrowNotImplementedException(ChallengeType type)
        {
            // Arrange
            var factory = LeagueOfLegendsChallengeScoringFactory.Instance;

            // Act
            var action = new Action(() => factory.Create(new MockChallenge(type)));

            // Assert
            action.Should().Throw<NotImplementedException>();
        }

        private sealed class MockChallenge : Challenge
        {
            public MockChallenge(ChallengeType type) : base(Game.LeagueOfLegends, new ChallengeName(nameof(Challenge)), new DefaultChallengeSetup(), new ChallengeTimeline())
            {
                Setup.SetPrivateField("_type", type);
            }
        }
    }
}