// Filename: GameAuthFactorControllerTest.cs
// Date Created: 2019-11-04
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Abstractions.Services;
using eDoxa.Games.Api.Areas.AuthFactor.Controllers;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Games.TestHelper.Mocks;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Games.UnitTests.Areas.AuthFactor.Controllers
{
    public sealed class GameAuthFactorControllerTest : UnitTest
    {
        public GameAuthFactorControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        //Todo Test other games too.
        [Fact]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockAuthFactorService = new Mock<IAuthFactorService>();

            mockAuthFactorService
                .Setup(authFactorService => authFactorService.GenerateAuthFactorAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<object>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            mockAuthFactorService.Setup(authFactorService => authFactorService.FindAuthFactorAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(new eDoxa.Games.Domain.AggregateModels.AuthFactorAggregate.AuthFactor(PlayerId.Parse("playerId"), ""))
                .Verifiable();

            var authFactorController = new GameAuthFactorController(mockAuthFactorService.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            authFactorController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await authFactorController.PostAsync(Game.LeagueOfLegends, "playerId");

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockAuthFactorService.Verify(
                authFactorService => authFactorService.GenerateAuthFactorAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<object>()),
                Times.Once);

            mockAuthFactorService.Verify(authFactorService => authFactorService.FindAuthFactorAsync(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);
        }
    }
}
