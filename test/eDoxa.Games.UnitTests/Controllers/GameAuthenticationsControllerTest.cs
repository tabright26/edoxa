// Filename: GameAuthenticationsControllerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Api.Controllers;
using eDoxa.Games.Domain.AggregateModels.GameAggregate;
using eDoxa.Games.LeagueOfLegends;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Games.UnitTests.Controllers
{
    public sealed class GameAuthenticationsControllerTest : UnitTest
    {
        public GameAuthenticationsControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task GenerateAuthenticationAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var gameAuthentication = new LeagueOfLegendsGameAuthentication(
                PlayerId.Parse("playerId"),
                new LeagueOfLegendsGameAuthenticationFactor(
                    1,
                    string.Empty,
                    2,
                    string.Empty));

            TestMock.GameAuthenticationService
                .Setup(authFactorService => authFactorService.GenerateAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<object>()))
                .ReturnsAsync(DomainValidationResult<GameAuthentication>.Succeeded(gameAuthentication))
                .Verifiable();

            var authFactorController = new GameAuthenticationsController(
                TestMock.GameAuthenticationService.Object,
                TestMock.GameCredentialService.Object,
                TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await authFactorController.GenerateAuthenticationAsync(Game.LeagueOfLegends, "playerId");

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            TestMock.GameAuthenticationService.Verify(
                authFactorService => authFactorService.GenerateAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<object>()),
                Times.Once);
        }

        [Fact]
        public async Task LinkCredentialAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            TestMock.GameCredentialService.Setup(credentialService => credentialService.LinkCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(DomainValidationResult<Credential>.Failure("test error"))
                .Verifiable();

            var authFactorController = new GameAuthenticationsController(
                TestMock.GameAuthenticationService.Object,
                TestMock.GameCredentialService.Object,
                TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await authFactorController.LinkCredentialAsync(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            TestMock.GameCredentialService.Verify(credentialService => credentialService.LinkCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public async Task LinkCredentialAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var userId = new UserId();

            var credential = new Credential(
                userId,
                Game.LeagueOfLegends,
                new PlayerId(),
                new UtcNowDateTimeProvider());

            TestMock.GameCredentialService.Setup(credentialService => credentialService.LinkCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(DomainValidationResult<Credential>.Succeeded(credential))
                .Verifiable();

            var authFactorController = new GameAuthenticationsController(
                TestMock.GameAuthenticationService.Object,
                TestMock.GameCredentialService.Object,
                TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = new MockHttpContextAccessor().Object.HttpContext
                }
            };

            // Act
            var result = await authFactorController.LinkCredentialAsync(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            TestMock.GameCredentialService.Verify(credentialService => credentialService.LinkCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);
        }
    }
}
