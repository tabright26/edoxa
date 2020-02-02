// Filename: GameCredentialsControllerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Games.Api.Controllers;
using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.Domain.Services;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Games.TestHelper.Mocks;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Games.UnitTests.Controllers
{
    public sealed class GameCredentialsControllerTest : UnitTest
    {
        public GameCredentialsControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task UnlinkCredentialAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockCredentialService = new Mock<IGameCredentialService>();

            var mockMapper = new Mock<IMapper>();

            var userId = new UserId();

            var credential = new Credential(
                userId,
                Game.LeagueOfLegends,
                new PlayerId(),
                new UtcNowDateTimeProvider());

            mockCredentialService.Setup(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(credential)
                .Verifiable();

            mockCredentialService.Setup(credentialService => credentialService.UnlinkCredentialAsync(It.IsAny<Credential>()))
                .ReturnsAsync(DomainValidationResult<Credential>.Failure("test", "test error"))
                .Verifiable();

            var authFactorController = new GameCredentialsController(mockCredentialService.Object, mockMapper.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            authFactorController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await authFactorController.UnlinkCredentialAsync(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockCredentialService.Verify(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);

            mockCredentialService.Verify(credentialService => credentialService.UnlinkCredentialAsync(It.IsAny<Credential>()), Times.Once);
        }

        [Fact]
        public async Task UnlinkCredentialAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockCredentialService = new Mock<IGameCredentialService>();

            var mockMapper = new Mock<IMapper>();

            mockCredentialService.Setup(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>())).Verifiable();

            var authFactorController = new GameCredentialsController(mockCredentialService.Object, mockMapper.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            authFactorController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await authFactorController.UnlinkCredentialAsync(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockCredentialService.Verify(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public async Task UnlinkCredentialAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockCredentialService = new Mock<IGameCredentialService>();

            var mockMapper = new Mock<IMapper>();

            var userId = new UserId();

            var credential = new Credential(
                userId,
                Game.LeagueOfLegends,
                new PlayerId(),
                new UtcNowDateTimeProvider());

            mockCredentialService.Setup(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(credential)
                .Verifiable();

            mockCredentialService.Setup(credentialService => credentialService.UnlinkCredentialAsync(It.IsAny<Credential>()))
                .ReturnsAsync(new DomainValidationResult<Credential>())
                .Verifiable();

            var authFactorController = new GameCredentialsController(mockCredentialService.Object, mockMapper.Object)
            {
                ControllerContext =
                {
                    HttpContext = new MockHttpContextAccessor().Object.HttpContext
                }
            };


            // Act
            var result = await authFactorController.UnlinkCredentialAsync(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockCredentialService.Verify(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);

            mockCredentialService.Verify(credentialService => credentialService.UnlinkCredentialAsync(It.IsAny<Credential>()), Times.Once);
        }
    }
}
