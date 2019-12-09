// Filename: AuthFactorGeneratorFactoryTest.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Games.Api.Application.Factories;
using eDoxa.Games.Domain.Adapters;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Moq;

using Xunit;

namespace eDoxa.Games.UnitTests.Factories
{
    public sealed class GameAuthenticationGeneratorFactoryTest : UnitTest // FRANCIS: comment je peux test si le mock change de type dans le constructeur.
    {
        public GameAuthenticationGeneratorFactoryTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void CreateInstance_ShouldBeOfTypeGameAdapter()
        {
            // Arrange
            var game = Game.LeagueOfLegends;

            var mockAuthenticationGeneratorAdapter = new Mock<IAuthenticationGeneratorAdapter>();

            mockAuthenticationGeneratorAdapter.SetupGet(authenticationGeneratorAdapter => authenticationGeneratorAdapter.Game).Returns(game);
            
            var authenticationGeneratorFactory = new GameGameAuthenticationGeneratorFactory(new[] {mockAuthenticationGeneratorAdapter.Object});

            // Act
            var adapter = authenticationGeneratorFactory.CreateInstance(game);

            // Assert
            adapter.Should().BeOfType(mockAuthenticationGeneratorAdapter.Object.GetType());
        }

        [Fact]
        public void CreateInstance_ShouldBeOfTypeInvalidOperationException()
        {
            // Arrange
            var mockAuthFactorGeneratorAdapters = new Mock<IDictionary<Game, IAuthenticationGeneratorAdapter>>();

            var mockAuthFactorGeneratorAdapter = new Mock<List<IAuthenticationGeneratorAdapter>>();

            mockAuthFactorGeneratorAdapters.Setup(adapters => adapters.TryGetValue(It.IsAny<Game>(), out It.Ref<IAuthenticationGeneratorAdapter>.IsAny))
                .Returns(false)
                .Verifiable();

            var authFactorGeneratorFactory = new GameGameAuthenticationGeneratorFactory(mockAuthFactorGeneratorAdapter.Object);

            // Act
            var action = new Action(() =>authFactorGeneratorFactory.CreateInstance(Game.LeagueOfLegends) ); 

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }
    }
}
