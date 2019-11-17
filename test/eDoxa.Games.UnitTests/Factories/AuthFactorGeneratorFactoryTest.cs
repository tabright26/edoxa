﻿// Filename: AuthFactorGeneratorFactoryTest.cs
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

using Microsoft.Azure.Storage;

using Moq;

using Xunit;

namespace eDoxa.Games.UnitTests.Factories
{
    public sealed class AuthFactorGeneratorFactoryTest : UnitTest // FRANCIS: comment je peux test si le mock change de type dans le constructeur.
    {
        public AuthFactorGeneratorFactoryTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void CreateInstance_ShouldBeOfTypeGameAdapter()
        {
            // Arrange
            var mockLeagueOfLegendsService = new Mock<ILeagueOfLegendsService>();
            var mockAuthFactorRepository = new Mock<IAuthenticationRepository>();
            var mockCloudStorageAccount = new Mock<CloudStorageAccount>();

            var authFactorGeneratorFactory = new AuthenticationGeneratorFactory(
                new[] {new LeagueOfLegendsAuthenticationGeneratorAdapter(mockLeagueOfLegendsService.Object, mockAuthFactorRepository.Object, mockCloudStorageAccount.Object)});

            // Act
            var result = authFactorGeneratorFactory.CreateInstance(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<LeagueOfLegendsAuthenticationGeneratorAdapter>();
        }

        [Fact]
        public void CreateInstance_ShouldBeOfTypeInvalidOperationException()
        {
            // Arrange
            var mockAuthFactorGeneratorAdapters = new Mock<IDictionary<Game, IAuthFactorGeneratorAdapter>>();

            var mockAuthFactorGeneratorAdapter = new Mock<List<IAuthFactorGeneratorAdapter>>();

            mockAuthFactorGeneratorAdapters.Setup(adapters => adapters.TryGetValue(It.IsAny<Game>(), out It.Ref<IAuthFactorGeneratorAdapter>.IsAny))
                .Returns(false)
                .Verifiable();

            var authFactorGeneratorFactory = new AuthenticationGeneratorFactory(mockAuthFactorGeneratorAdapter.Object);

            try
            {
                // Act
                authFactorGeneratorFactory.CreateInstance(Game.LeagueOfLegends);
            }
            catch (Exception e)
            {
                // Assert
                e.Should().BeOfType<InvalidOperationException>();
            }
        }
    }
}
