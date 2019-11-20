// Filename: AuthFactorValidatorFactoryTest.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Games.Abstractions.Adapter;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Games.Factories;
using eDoxa.Games.LeagueOfLegends.Abstactions;
using eDoxa.Games.LeagueOfLegends.Adapter;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Moq;

using Xunit;

namespace eDoxa.Games.UnitTests.Factories
{
    public sealed class GameAuthenticationValidatorFactoryTest : UnitTest // FRANCIS: Meme chose ici
    {
        public GameAuthenticationValidatorFactoryTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void CreateInstance_ShouldBeOfTypeGameAdapter()
        {
            // Arrange
            var mockLeagueOfLegendsService = new Mock<ILeagueOfLegendsService>();
            var mockAuthFactorRepository = new Mock<IGameAuthenticationRepository>();

            var factory = new GameGameAuthenticationValidatorFactory(
                new[] {new LeagueOfLegendsAuthenticationValidatorAdapter(mockLeagueOfLegendsService.Object, mockAuthFactorRepository.Object)});

            // Act
            var result = factory.CreateInstance(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<LeagueOfLegendsAuthenticationValidatorAdapter>();
        }

        [Fact]
        public void CreateInstance_ShouldBeOfTypeInvalidOperationException()
        {
            // Arrange
            var mockAuthFactorValidatorAdapters = new Mock<IDictionary<Game, IAuthenticationValidatorAdapter>>();

            var mockAuthFactorValidatorAdapter = new Mock<List<IAuthenticationValidatorAdapter>>();

            mockAuthFactorValidatorAdapters.Setup(adapters => adapters.TryGetValue(It.IsAny<Game>(), out It.Ref<IAuthenticationValidatorAdapter>.IsAny))
                .Returns(false)
                .Verifiable();

            var authFactorValidatorFactory = new GameGameAuthenticationValidatorFactory(mockAuthFactorValidatorAdapter.Object);

            try
            {
                // Act
                authFactorValidatorFactory.CreateInstance(Game.LeagueOfLegends);
            }
            catch (Exception e)
            {
                // Assert
                e.Should().BeOfType<InvalidOperationException>();
            }
        }
    }
}
