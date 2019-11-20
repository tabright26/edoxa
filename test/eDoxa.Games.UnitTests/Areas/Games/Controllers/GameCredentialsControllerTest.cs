﻿// Filename: GameCredentialControllerTest.cs
// Date Created: 2019-11-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Games.Abstractions.Services;
using eDoxa.Games.Api.Areas.Games.Controllers;
using eDoxa.Games.Domain.AggregateModels.GameAggregate;
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
    public sealed class GameCredentialsControllerTest : UnitTest
    {
        public GameCredentialsControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task DeleteByGameAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockCredentialService = new Mock<ICredentialService>();

            var mockMapper = new Mock<IMapper>();

            var userId = new UserId();

            var credential = new Credential(
                userId,
                Game.LeagueOfLegends,
                new PlayerId(),
                new UtcNowDateTimeProvider());

            mockCredentialService
                .Setup(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(credential)
                .Verifiable();

            mockCredentialService
                .Setup(credentialService => credentialService.UnlinkCredentialAsync(It.IsAny<Credential>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var authFactorController = new GameCredentialsController(mockCredentialService.Object, mockMapper.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            authFactorController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await authFactorController.DeleteAsync(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockCredentialService.Verify(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);

            mockCredentialService.Verify(credentialService => credentialService.UnlinkCredentialAsync(It.IsAny<Credential>()), Times.Once);
        }

        [Fact]
        public async Task DeleteByGameAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockCredentialService = new Mock<ICredentialService>();

            var mockMapper = new Mock<IMapper>();

            mockCredentialService
                .Setup(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .Verifiable();

            var authFactorController = new GameCredentialsController(mockCredentialService.Object, mockMapper.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            authFactorController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await authFactorController.DeleteAsync(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockCredentialService.Verify(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public async Task DeleteByGameAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockCredentialService = new Mock<ICredentialService>();

            var mockMapper = new Mock<IMapper>();

            var userId = new UserId();

            var credential = new Credential(
                userId,
                Game.LeagueOfLegends,
                new PlayerId(),
                new UtcNowDateTimeProvider());

            var validation = new ValidationResult();
            validation.Errors.Add(new ValidationFailure("test", "test error"));

            mockCredentialService
                .Setup(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(credential)
                .Verifiable();

            mockCredentialService
                .Setup(credentialService => credentialService.UnlinkCredentialAsync(It.IsAny<Credential>()))
                .ReturnsAsync(validation)
                .Verifiable();

            var authFactorController = new GameCredentialsController(mockCredentialService.Object, mockMapper.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            authFactorController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await authFactorController.DeleteAsync(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockCredentialService.Verify(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);

            mockCredentialService.Verify(credentialService => credentialService.UnlinkCredentialAsync(It.IsAny<Credential>()), Times.Once);
        }
    }
}
