﻿// Filename: GameAuthenticationsControllerTest.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Games.Abstractions.Services;
using eDoxa.Games.Api.Areas.Games.Controllers;
using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.LeagueOfLegends;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Games.TestHelper.Mocks;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Games.UnitTests.Areas.Games.Controllers
{
    public sealed class GameAuthenticationsControllerTest : UnitTest
    {
        public GameAuthenticationsControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        //Todo Test other games too.
        [Fact]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockAuthFactorService = new Mock<IAuthenticationService>();
            var mockCredentialService = new Mock<ICredentialService>();
            var mockMapper = new Mock<IMapper>();

            mockAuthFactorService
                .Setup(authFactorService => authFactorService.GenerateAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<object>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            mockAuthFactorService
                .Setup(
                    authFactorService =>
                        authFactorService.FindAuthenticationAsync<LeagueOfLegendsGameAuthenticationFactor>(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(
                    new GameAuthentication<LeagueOfLegendsGameAuthenticationFactor>(
                        PlayerId.Parse("playerId"),
                        new LeagueOfLegendsGameAuthenticationFactor(
                            1,
                            string.Empty,
                            2,
                            string.Empty)))
                .Verifiable();

            var authFactorController = new GameAuthenticationsController(mockAuthFactorService.Object, mockCredentialService.Object, mockMapper.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            authFactorController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await authFactorController.PostAsync(Game.LeagueOfLegends, "playerId");

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockAuthFactorService.Verify(
                authFactorService => authFactorService.GenerateAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<object>()),
                Times.Once);

            mockAuthFactorService.Verify(
                authFactorService => authFactorService.FindAuthenticationAsync<LeagueOfLegendsGameAuthenticationFactor>(It.IsAny<UserId>(), It.IsAny<Game>()),
                Times.Once);
        }

        [Fact]
        public async Task PostByGameAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockCredentialService = new Mock<ICredentialService>();

            var mockAuthenticationService = new Mock<IAuthenticationService>();

            var mockMapper = new Mock<IMapper>();

            var validation = new ValidationResult();
            validation.Errors.Add(new ValidationFailure("test", "test error"));

            mockCredentialService.Setup(credentialService => credentialService.LinkCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(validation)
                .Verifiable();

            var authFactorController = new GameAuthenticationsController(mockAuthenticationService.Object, mockCredentialService.Object, mockMapper.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            authFactorController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await authFactorController.PutAsync(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            mockCredentialService.Verify(credentialService => credentialService.LinkCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public async Task PuAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockCredentialService = new Mock<ICredentialService>();

            var mockAuthenticationService = new Mock<IAuthenticationService>();

            var mockMapper = new Mock<IMapper>();

            var userId = new UserId();

            var credential = new Credential(
                userId,
                Game.LeagueOfLegends,
                new PlayerId(),
                new UtcNowDateTimeProvider());

            mockCredentialService.Setup(credentialService => credentialService.LinkCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            mockCredentialService.Setup(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(credential)
                .Verifiable();

            var authFactorController = new GameAuthenticationsController(mockAuthenticationService.Object, mockCredentialService.Object, mockMapper.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            authFactorController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await authFactorController.PutAsync(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockCredentialService.Verify(credentialService => credentialService.LinkCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);

            mockCredentialService.Verify(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);
        }
    }
}