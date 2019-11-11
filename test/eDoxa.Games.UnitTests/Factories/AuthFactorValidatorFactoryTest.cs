// Filename: AuthFactorValidatorFactoryTest.cs
// Date Created: 2019-11-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Games.Abstractions.Adapter;
using eDoxa.Games.Factories;
using eDoxa.Games.LeagueOfLegends.Adapter;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Moq;

using Xunit;

namespace eDoxa.Games.UnitTests.Factories
{
    public sealed class AuthFactorValidatorFactoryTest : UnitTest // FRANCIS: Meme chose ici
    {
        public AuthFactorValidatorFactoryTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void CreateInstance_ShouldBeOfTypeGameAdapter()
        {
            // Arrange
            var mockAuthFactorValidatorAdapters = new Mock<IDictionary<Game, IAuthFactorValidatorAdapter>>();

            var mockAuthFactorValidatorAdapter = new Mock<List<IAuthFactorValidatorAdapter>>();

            mockAuthFactorValidatorAdapters
                .Setup(adapters => adapters.TryGetValue(It.IsAny<Game>(), out It.Ref<IAuthFactorValidatorAdapter>.IsAny))
                .Returns(true)
                .Verifiable();

            var authFactorValidatorFactory = new AuthFactorValidatorFactory(mockAuthFactorValidatorAdapter.Object);

            // Act
            var result = authFactorValidatorFactory.CreateInstance(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<LeagueOfLegendsAuthFactorValidatorAdapter>();
        }

        [Fact]
        public void CreateInstance_ShouldBeOfTypeInvalidOperationException()
        {
            // Arrange
            var mockAuthFactorValidatorAdapters = new Mock<IDictionary<Game, IAuthFactorValidatorAdapter>>();

            var mockAuthFactorValidatorAdapter = new Mock<List<IAuthFactorValidatorAdapter>>();

            mockAuthFactorValidatorAdapters
                .Setup(adapters => adapters.TryGetValue(It.IsAny<Game>(), out It.Ref<IAuthFactorValidatorAdapter>.IsAny))
                .Returns(false)
                .Verifiable();

            var authFactorValidatorFactory = new AuthFactorValidatorFactory(mockAuthFactorValidatorAdapter.Object);

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
