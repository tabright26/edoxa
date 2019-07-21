// Filename: MatchFactoryTest.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Arena.Challenges.Api.Application.Adapters;
using eDoxa.Arena.Challenges.Api.Application.Factories;
using eDoxa.Arena.Challenges.Domain.Adapters;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.LeagueOfLegends.Abstractions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Factories
{
    [TestClass]
    public sealed class MatchFactoryTest
    {
        private Mock<ILeagueOfLegendsProxy> _mockLeagueOfLegendsProxy;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockLeagueOfLegendsProxy = new Mock<ILeagueOfLegendsProxy>();
        }

        [TestMethod]
        public void CreateInstance_FromDependencyInjection_ShouldBeLeagueOfLegendsMatchAdapter()
        {
            // Arrange
            var leagueOfLegendsMatchAdapter = new LeagueOfLegendsMatchAdapter(_mockLeagueOfLegendsProxy.Object);

            var matchAdapters = new List<IMatchAdapter>
            {
                leagueOfLegendsMatchAdapter
            };

            var matchFactory = new MatchFactory(matchAdapters);

            // Act
            var matchAdapter = matchFactory.CreateInstance(ChallengeGame.LeagueOfLegends);

            // Assert
            matchAdapter.Should().Be(leagueOfLegendsMatchAdapter);
        }

        [TestMethod]
        public void CreateInstance_WithoutAdapter_ShouldThrowNotSupportedException()
        {
            // Arrange
            var matchFactory = new MatchFactory(Array.Empty<IMatchAdapter>());

            // Act
            var action = new Action(() => matchFactory.CreateInstance(ChallengeGame.LeagueOfLegends));

            // Assert
            action.Should().Throw<NotSupportedException>();
        }
    }
}
