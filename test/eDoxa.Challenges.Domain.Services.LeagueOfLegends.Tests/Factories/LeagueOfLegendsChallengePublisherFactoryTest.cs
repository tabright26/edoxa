// Filename: LeagueOfLegendsChallengePublisherFactoryTest.cs
// Date Created: 2019-03-05
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Services.LeagueOfLegends.Tests.Factories
{
    [TestClass]
    public sealed class LeagueOfLegendsChallengePublisherFactoryTest
    {
        [DataRow(ChallengePublisherPeriodicity.Daily)]
        [DataRow(ChallengePublisherPeriodicity.Weekly)]
        [DataRow(ChallengePublisherPeriodicity.Monthly)]
        [DataTestMethod]
        public void Create_ImplementedType_ShouldNotBeNull(ChallengePublisherPeriodicity periodicity)
        {
            // Arrange
            var factory = LeagueOfLegendsChallengePublisherFactory.Instance;

            // Act
            var strategy = factory.Create(periodicity);

            // Assert
            strategy.Challenges.Should().NotBeNull();
        }
    }
}