﻿// Filename: GameReferencesFactoryTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Adapters;
using eDoxa.Arena.Challenges.Api.Areas.Challenges.Factories;
using eDoxa.Arena.Challenges.Api.Temp.LeagueOfLegends.Abstractions;
using eDoxa.Arena.Challenges.Domain.Adapters;
using eDoxa.Arena.Challenges.TestHelpers;
using eDoxa.Arena.Challenges.TestHelpers.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Moq;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Areas.Challenges.Factories
{
    public sealed class GameReferencesFactoryTest : UnitTest
    {
        public GameReferencesFactoryTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void CreateInstance_FromDependencyInjection_ShouldBeLeagueOfLegendsGameReferencesAdapter()
        {
            // Arrange
            var mockLeagueOfLegendsProxy = new Mock<ILeagueOfLegendsService>();

            var leagueOfLegendsGameReferencesAdapter = new LeagueOfLegendsGameReferencesAdapter(mockLeagueOfLegendsProxy.Object);

            var gameReferencesAdapters = new List<IGameReferencesAdapter>
            {
                leagueOfLegendsGameReferencesAdapter
            };

            var gameReferencesFactory = new GameReferencesFactory(gameReferencesAdapters);

            // Act
            var gameReferencesAdapter = gameReferencesFactory.CreateInstance(Game.LeagueOfLegends);

            // Assert
            gameReferencesAdapter.Should().Be(leagueOfLegendsGameReferencesAdapter);
        }

        [Fact]
        public void CreateInstance_WithoutAdapter_ShouldThrowNotSupportedException()
        {
            // Arrange
            var gameReferencesFactory = new GameReferencesFactory(Array.Empty<IGameReferencesAdapter>());

            // Act
            var action = new Action(() => gameReferencesFactory.CreateInstance(Game.LeagueOfLegends));

            // Assert
            action.Should().Throw<NotSupportedException>();
        }
    }
}
