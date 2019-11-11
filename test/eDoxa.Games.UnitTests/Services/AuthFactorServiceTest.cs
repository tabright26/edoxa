// Filename: AuthFactorServiceTest.cs
// Date Created: 2019-11-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Games.Abstractions.Adapter;
using eDoxa.Games.Abstractions.Factories;
using eDoxa.Games.Abstractions.Services;
using eDoxa.Games.Api.Areas.AuthFactor.Controllers;
using eDoxa.Games.Domain.AggregateModels.AuthFactorAggregate;
using eDoxa.Games.Domain.Repositories;
using eDoxa.Games.LeagueOfLegends;
using eDoxa.Games.LeagueOfLegends.Adapter;
using eDoxa.Games.LeagueOfLegends.Services;
using eDoxa.Games.Services;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Games.TestHelper.Mocks;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Moq;

using Xunit;

namespace eDoxa.Games.UnitTests.Services
{
    public sealed class AuthFactorServiceTest : UnitTest
    {
        public AuthFactorServiceTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task FindAuthFactorAsync()
        {
            // Arrange
            var userId = new UserId();

            var authFactor = new AuthFactor(new PlayerId(), userId);

            var mockAuthFactorGenerator = new Mock<IAuthFactorGeneratorFactory>();
            var mockAuthFactorValidator = new Mock<IAuthFactorValidatorFactory>();
            var mockAuthFactorRepository = new Mock<IAuthFactorRepository>();

            mockAuthFactorRepository
                .Setup(repository => repository.GetAuthFactorAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(authFactor)
                .Verifiable();

            var authFactorService = new AuthFactorService(mockAuthFactorGenerator.Object, mockAuthFactorValidator.Object, mockAuthFactorRepository.Object);

            // Act
            await authFactorService.FindAuthFactorAsync(userId, Game.LeagueOfLegends);

            // Assert
            mockAuthFactorRepository.Verify(
                repository => repository.GetAuthFactorAsync(It.IsAny<UserId>(), It.IsAny<Game>()),
                Times.Once);
        }

        [Fact]
        public async Task AuthFactorExistsAsync()
        {
            // Arrange
            var userId = new UserId();

            var mockAuthFactorGenerator = new Mock<IAuthFactorGeneratorFactory>();
            var mockAuthFactorValidator = new Mock<IAuthFactorValidatorFactory>();
            var mockAuthFactorRepository = new Mock<IAuthFactorRepository>();

            mockAuthFactorRepository
                .Setup(repository => repository.AuthFactorExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(true)
                .Verifiable();

            var authFactorService = new AuthFactorService(mockAuthFactorGenerator.Object, mockAuthFactorValidator.Object, mockAuthFactorRepository.Object);

            // Act
            await authFactorService.AuthFactorExistsAsync(userId, Game.LeagueOfLegends);

            // Assert
            mockAuthFactorRepository.Verify(
                repository => repository.AuthFactorExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>()),
                Times.Once);
        }

        [Fact]
        public async Task GenerateAuthFactorAsync()
        {
            // Arrange
            var userId = new UserId();

            var mockAuthFactorGenerator = new Mock<IAuthFactorGeneratorFactory>();
            var mockAuthFactorValidator = new Mock<IAuthFactorValidatorFactory>();
            var mockAuthFactorRepository = new Mock<IAuthFactorRepository>();

            var leagueService = new LeagueOfLegendsService(
                new OptionsWrapper<LeagueOfLegendsOptions>(new LeagueOfLegendsOptions()));

            var leagueAdapter = new LeagueOfLegendsAuthFactorGeneratorAdapter(leagueService, mockAuthFactorRepository.Object);

            mockAuthFactorGenerator
                .Setup(generator => generator.CreateInstance(It.IsAny<Game>()))
                .Returns(leagueAdapter)
                .Verifiable();

            var authFactorService = new AuthFactorService(mockAuthFactorGenerator.Object, mockAuthFactorValidator.Object, mockAuthFactorRepository.Object);

            // Act
            await authFactorService.GenerateAuthFactorAsync(userId, Game.LeagueOfLegends, "playerId");

            // Assert
            mockAuthFactorGenerator.Verify(
                generator => generator.CreateInstance(It.IsAny<Game>()),
                Times.Once);
        }


        [Fact]
        public async Task ValidateAuthFactorAsync()
        {
            // Arrange
            var userId = new UserId();

            var authFactor = new AuthFactor(new PlayerId(), userId);

            var mockAuthFactorGenerator = new Mock<IAuthFactorGeneratorFactory>();
            var mockAuthFactorValidator = new Mock<IAuthFactorValidatorFactory>();
            var mockAuthFactorRepository = new Mock<IAuthFactorRepository>();

            var leagueOptions = new LeagueOfLegendsOptions
            {
                ApiKey = "testKey"
            };

            var leagueService = new LeagueOfLegendsService(
                new OptionsWrapper<LeagueOfLegendsOptions>(leagueOptions));

            var leagueAdapter = new LeagueOfLegendsAuthFactorValidatorAdapter(leagueService, mockAuthFactorRepository.Object);

            mockAuthFactorValidator
                .Setup(validator => validator.CreateInstance(It.IsAny<Game>()))
                .Returns(leagueAdapter)
                .Verifiable();

            var authFactorService = new AuthFactorService(mockAuthFactorGenerator.Object, mockAuthFactorValidator.Object, mockAuthFactorRepository.Object);

            // Act
            await authFactorService.ValidateAuthFactorAsync(userId, Game.LeagueOfLegends, authFactor);

            // Assert
            mockAuthFactorValidator.Verify(
                validator => validator.CreateInstance(It.IsAny<Game>()),
                Times.Once);
        }
    }
}
