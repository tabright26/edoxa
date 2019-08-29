// Filename: GameReferencesFactoryTest.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Adapters;
using eDoxa.Arena.Challenges.Api.Areas.Challenges.Factories;
using eDoxa.Arena.Challenges.Domain.Adapters;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Games.LeagueOfLegends.Abstractions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Areas.Challenges.Factories
{
    [TestClass]
    public sealed class GameReferencesFactoryTest
    {
        [TestMethod]
        public void CreateInstance_FromDependencyInjection_ShouldBeLeagueOfLegendsGameReferencesAdapter()
        {
            // Arrange
            var mockLeagueOfLegendsProxy = new Mock<ILeagueOfLegendsProxy>();

            var leagueOfLegendsGameReferencesAdapter = new LeagueOfLegendsGameReferencesAdapter(mockLeagueOfLegendsProxy.Object);

            var gameReferencesAdapters = new List<IGameReferencesAdapter>
            {
                leagueOfLegendsGameReferencesAdapter
            };

            var gameReferencesFactory = new GameReferencesFactory(gameReferencesAdapters);

            // Act
            var gameReferencesAdapter = gameReferencesFactory.CreateInstance(ChallengeGame.LeagueOfLegends);

            // Assert
            gameReferencesAdapter.Should().Be(leagueOfLegendsGameReferencesAdapter);
        }

        [TestMethod]
        public void CreateInstance_WithoutAdapter_ShouldThrowNotSupportedException()
        {
            // Arrange
            var gameReferencesFactory = new GameReferencesFactory(Array.Empty<IGameReferencesAdapter>());

            // Act
            var action = new Action(() => gameReferencesFactory.CreateInstance(ChallengeGame.LeagueOfLegends));

            // Assert
            action.Should().Throw<NotSupportedException>();
        }
    }
}
