﻿// Filename: GameReferencesFactoryTest.cs
// Date Created: 2019-06-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

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
    public sealed class GameReferencesFactoryTest
    {
        private Mock<ILeagueOfLegendsProxy> _mockLeagueOfLegendsProxy;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockLeagueOfLegendsProxy = new Mock<ILeagueOfLegendsProxy>();
        }

        [TestMethod]
        public void CreateInstance_FromDependencyInjection_ShouldBeLeagueOfLegendsGameReferencesAdapter()
        {
            // Arrange
            var leagueOfLegendsGameReferencesAdapter = new LeagueOfLegendsGameReferencesAdapter(_mockLeagueOfLegendsProxy.Object);

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