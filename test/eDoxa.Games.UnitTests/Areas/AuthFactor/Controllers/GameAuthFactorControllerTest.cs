// Filename: GameAuthFactorControllerTest.cs
// Date Created: 2019-11-04
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Abstractions.Services;
using eDoxa.Games.Api.Areas.AuthFactor.Controllers;
using eDoxa.Games.Domain.AggregateModels;
using eDoxa.Games.LeagueOfLegends;
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
            var mockAuthFactorService = new Mock<IAuthenticationService>();

            mockAuthFactorService
                .Setup(authFactorService => authFactorService.GenerateAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<object>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            mockAuthFactorService.Setup(authFactorService => authFactorService.FindAuthenticationAsync<LeagueOfLegendsAuthenticationFactor>(It.IsAny<UserId>(), It.IsAny<Game>()))
                .ReturnsAsync(new Authentication<LeagueOfLegendsAuthenticationFactor>(PlayerId.Parse("playerId"), new LeagueOfLegendsAuthenticationFactor(1, string.Empty, 2, string.Empty)))
                .Verifiable();

            var authFactorController = new GameAuthFactorController(mockAuthFactorService.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            authFactorController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await authFactorController.PostAsync(Game.LeagueOfLegends, "playerId");

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockAuthFactorService.Verify(
                authFactorService => authFactorService.GenerateAuthenticationAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<object>()),
                Times.Once);

            mockAuthFactorService.Verify(authFactorService => authFactorService.FindAuthenticationAsync<LeagueOfLegendsAuthenticationFactor>(It.IsAny<UserId>(), It.IsAny<Game>()), Times.Once);
        }
    }
}
