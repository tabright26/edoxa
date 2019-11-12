// Filename: GameCredentialControllerTest.cs
// Date Created: 2019-11-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Games.Abstractions.Services;
using eDoxa.Games.Api.Areas.Credential.Controllers;
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

namespace eDoxa.Games.UnitTests.Areas.Credential.Controllers
{
    public sealed class GameCredentialControllerTest : UnitTest
    {
        public GameCredentialControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task GetByGameAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockCredentialService = new Mock<ICredentialService>();
            var mockMapper = new Mock<IMapper>();

            var userId = new UserId();

            var credential = new eDoxa.Games.Domain.AggregateModels.CredentialAggregate.Credential(
                userId,
                Game.LeagueOfLegends,
                new PlayerId(),
                new UtcNowDateTimeProvider());

            mockCredentialService
                .Setup(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(credential)
                .Verifiable();


            var authFactorController = new GameCredentialController(mockCredentialService.Object, mockMapper.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            authFactorController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await authFactorController.GetByGameAsync(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockCredentialService.Verify(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public async Task GetByGameAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockCredentialService = new Mock<ICredentialService>();
            var mockMapper = new Mock<IMapper>();

            var userId = new UserId();

            mockCredentialService
                .Setup(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .Verifiable();


            var authFactorController = new GameCredentialController(mockCredentialService.Object, mockMapper.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            authFactorController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await authFactorController.GetByGameAsync(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockCredentialService.Verify(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public async Task PostByGameAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockCredentialService = new Mock<ICredentialService>();

            var mockMapper = new Mock<IMapper>();

            var userId = new UserId();

            var credential = new eDoxa.Games.Domain.AggregateModels.CredentialAggregate.Credential(
                userId,
                Game.LeagueOfLegends,
                new PlayerId(),
                new UtcNowDateTimeProvider());

            mockCredentialService
                .Setup(credentialService => credentialService.LinkCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            mockCredentialService
                .Setup(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(credential)
                .Verifiable();


            var authFactorController = new GameCredentialController(mockCredentialService.Object, mockMapper.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            authFactorController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await authFactorController.PostByGameAsync(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockCredentialService.Verify(credentialService => credentialService.LinkCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);

            mockCredentialService.Verify(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);

        }

        [Fact]
        public async Task PostByGameAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockCredentialService = new Mock<ICredentialService>();

            var mockMapper = new Mock<IMapper>();

            var validation = new ValidationResult();
            validation.Errors.Add(new ValidationFailure("test", "test error"));

            mockCredentialService
                .Setup(credentialService => credentialService.LinkCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(validation)
                .Verifiable();

            var authFactorController = new GameCredentialController(mockCredentialService.Object, mockMapper.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            authFactorController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;
            // Act
            var result = await authFactorController.PostByGameAsync(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            mockCredentialService.Verify(credentialService => credentialService.LinkCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public async Task DeleteByGameAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockCredentialService = new Mock<ICredentialService>();

            var mockMapper = new Mock<IMapper>();

            var userId = new UserId();

            var credential = new eDoxa.Games.Domain.AggregateModels.CredentialAggregate.Credential(
                userId,
                Game.LeagueOfLegends,
                new PlayerId(),
                new UtcNowDateTimeProvider());

            mockCredentialService
                .Setup(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(credential)
                .Verifiable();

            mockCredentialService
                .Setup(credentialService => credentialService.UnlinkCredentialAsync(It.IsAny<eDoxa.Games.Domain.AggregateModels.CredentialAggregate.Credential>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var authFactorController = new GameCredentialController(mockCredentialService.Object, mockMapper.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            authFactorController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await authFactorController.DeleteByGameAsync(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockCredentialService.Verify(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);

            mockCredentialService.Verify(credentialService => credentialService.UnlinkCredentialAsync(It.IsAny<eDoxa.Games.Domain.AggregateModels.CredentialAggregate.Credential>()), Times.Once);
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

            var authFactorController = new GameCredentialController(mockCredentialService.Object, mockMapper.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            authFactorController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await authFactorController.DeleteByGameAsync(Game.LeagueOfLegends);

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

            var credential = new eDoxa.Games.Domain.AggregateModels.CredentialAggregate.Credential(
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
                .Setup(credentialService => credentialService.UnlinkCredentialAsync(It.IsAny<eDoxa.Games.Domain.AggregateModels.CredentialAggregate.Credential>()))
                .ReturnsAsync(validation)
                .Verifiable();

            var authFactorController = new GameCredentialController(mockCredentialService.Object, mockMapper.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            authFactorController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await authFactorController.DeleteByGameAsync(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockCredentialService.Verify(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);

            mockCredentialService.Verify(credentialService => credentialService.UnlinkCredentialAsync(It.IsAny<eDoxa.Games.Domain.AggregateModels.CredentialAggregate.Credential>()), Times.Once);
        }
    }
}

