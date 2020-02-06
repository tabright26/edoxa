// Filename: GameCredentialsControllerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Api.Controllers;
using eDoxa.Games.Domain.AggregateModels.GameAggregate;
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
    public sealed class GameCredentialsControllerTest : UnitTest
    {
        public GameCredentialsControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task UnlinkCredentialAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var userId = new UserId();

            var credential = new Credential(
                userId,
                Game.LeagueOfLegends,
                new PlayerId(),
                new UtcNowDateTimeProvider());

            TestMock.GameCredentialService.Setup(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(credential)
                .Verifiable();

            TestMock.GameCredentialService.Setup(credentialService => credentialService.UnlinkCredentialAsync(It.IsAny<Credential>()))
                .ReturnsAsync(DomainValidationResult<Credential>.Failure("test", "test error"))
                .Verifiable();

            var authFactorController = new GameCredentialsController(TestMock.GameCredentialService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await authFactorController.UnlinkCredentialAsync(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            TestMock.GameCredentialService.Verify(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);

            TestMock.GameCredentialService.Verify(credentialService => credentialService.UnlinkCredentialAsync(It.IsAny<Credential>()), Times.Once);
        }

        [Fact]
        public async Task UnlinkCredentialAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            TestMock.GameCredentialService.Setup(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>())).Verifiable();

            var authFactorController = new GameCredentialsController(TestMock.GameCredentialService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await authFactorController.UnlinkCredentialAsync(Game.LeagueOfLegends);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            TestMock.GameCredentialService.Verify(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);
        }

        [Fact]
        public async Task UnlinkCredentialAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var userId = new UserId();

            var credential = new Credential(
                userId,
                Game.LeagueOfLegends,
                new PlayerId(),
                new UtcNowDateTimeProvider());

            TestMock.GameCredentialService.Setup(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(credential)
                .Verifiable();

            TestMock.GameCredentialService.Setup(credentialService => credentialService.UnlinkCredentialAsync(It.IsAny<Credential>()))
                .ReturnsAsync(new DomainValidationResult<Credential>())
                .Verifiable();

            var authFactorController = new GameCredentialsController(TestMock.GameCredentialService.Object, TestMapper)
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

            TestMock.GameCredentialService.Verify(credentialService => credentialService.FindCredentialAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);

            TestMock.GameCredentialService.Verify(credentialService => credentialService.UnlinkCredentialAsync(It.IsAny<Credential>()), Times.Once);
        }
    }
}
