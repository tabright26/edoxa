// Filename: GameAuthenticationValidatorFactoryTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Games.Api.Application.Factories;
using eDoxa.Games.Domain.Adapters;
using eDoxa.Games.LeagueOfLegends.Adapter;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Xunit;

namespace eDoxa.Games.UnitTests.Application.Factories
{
    public sealed class GameAuthenticationValidatorFactoryTest : UnitTest
    {
        public GameAuthenticationValidatorFactoryTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void CreateInstance_ShouldBeOfTypeGameAdapter()
        {
            // Arrange
            var factory = new GameGameAuthenticationValidatorFactory(
                new[]
                {
                    new LeagueOfLegendsAuthenticationValidatorAdapter(TestMock.LeagueOfLegendsService.Object, TestMock.GameAuthenticationRepository.Object)
                });

            // Act
            var result = factory.CreateInstance(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<LeagueOfLegendsAuthenticationValidatorAdapter>();
        }

        [Fact]
        public void CreateInstance_ShouldBeOfTypeInvalidOperationException()
        {
            // Arrange
            var authFactorValidatorFactory = new GameGameAuthenticationValidatorFactory(new List<IAuthenticationValidatorAdapter>());

            // Act
            var action = new Action(() => authFactorValidatorFactory.CreateInstance(Game.LeagueOfLegends));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }
    }
}
