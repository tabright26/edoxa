// Filename: ChallengePubliserFactoryTest.cs
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

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Services.Tests.Factories
{
    [TestClass]
    public sealed class ChallengePubliserFactoryTest
    {
        [DataRow(ChallengePublisherPeriodicity.Daily, Game.LeagueOfLegends)]
        [DataRow(ChallengePublisherPeriodicity.Weekly, Game.LeagueOfLegends)]
        [DataRow(ChallengePublisherPeriodicity.Monthly, Game.LeagueOfLegends)]
        [DataTestMethod]
        public void Create_ImplementedType_ShouldNotBeNull(ChallengePublisherPeriodicity periodicity, Game game)
        {
            // Arrange
            var factory = ChallengePubliserFactory.Instance;

            // Act
            var strategy = factory.Create(periodicity, game);

            // Assert
            strategy.Challenges.Should().NotBeNull();
        }

        [DataRow(ChallengePublisherPeriodicity.Daily, Game.CSGO)]
        [DataRow(ChallengePublisherPeriodicity.Weekly, Game.CSGO)]
        [DataRow(ChallengePublisherPeriodicity.Monthly, Game.CSGO)]
        [DataRow(ChallengePublisherPeriodicity.Daily, Game.Fortnite)]
        [DataRow(ChallengePublisherPeriodicity.Weekly, Game.Fortnite)]
        [DataRow(ChallengePublisherPeriodicity.Monthly, Game.Fortnite)]
        [DataTestMethod]
        public void Create_NotImplementedType_ShouldThrowNotImplementedException(ChallengePublisherPeriodicity periodicity, Game game)
        {
            // Arrange
            var factory = ChallengePubliserFactory.Instance;

            // Act
            var action = new Action(() => factory.Create(periodicity, game));

            // Assert
            action.Should().Throw<NotImplementedException>();
        }
    }
}