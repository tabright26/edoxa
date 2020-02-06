// Filename: GameAuthenticationGeneratorFactoryTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Games.Api.Application.Factories;
using eDoxa.Games.Domain.Adapters;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Xunit;

namespace eDoxa.Games.UnitTests.Application.Factories
{
    public sealed class GameAuthenticationGeneratorFactoryTest : UnitTest
    {
        public GameAuthenticationGeneratorFactoryTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void CreateInstance_ShouldBeOfTypeGameAdapter()
        {
            // Arrange
            var game = Game.LeagueOfLegends;

            TestMock.AuthenticationGeneratorAdapter.SetupGet(authenticationGeneratorAdapter => authenticationGeneratorAdapter.Game).Returns(game);

            var authenticationGeneratorFactory = new GameGameAuthenticationGeneratorFactory(new[] {TestMock.AuthenticationGeneratorAdapter.Object});

            // Act
            var adapter = authenticationGeneratorFactory.CreateInstance(game);

            // Assert
            adapter.Should().BeOfType(TestMock.AuthenticationGeneratorAdapter.Object.GetType());
        }

        [Fact]
        public void CreateInstance_ShouldBeOfTypeInvalidOperationException()
        {
            // Arrange
            var authFactorGeneratorFactory = new GameGameAuthenticationGeneratorFactory(new List<IAuthenticationGeneratorAdapter>());

            // Act
            var action = new Action(() => authFactorGeneratorFactory.CreateInstance(Game.LeagueOfLegends));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }
    }
}
