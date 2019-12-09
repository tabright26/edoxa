// Filename: GameOptionsControllerTest.cs
// Date Created: 2019-11-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Api.Areas.Games.Controllers;
using eDoxa.Games.Api.Infrastructure;
using eDoxa.Games.Domain.Services;
using eDoxa.Games.LeagueOfLegends;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Games.TestHelper.Mocks;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Moq;

using Xunit;

namespace eDoxa.Games.UnitTests.Areas.Games.Controllers
{
    public sealed class GamesControllerTest : UnitTest
    {
        public GamesControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var serviceCredential = new Mock<IGameCredentialService>();

            serviceCredential.Setup(x => x.CredentialExistsAsync(It.IsAny<UserId>(), It.IsAny<Game>())).ReturnsAsync(true).Verifiable(); // TODO: Verify.

            var gameOptionsController = new GamesController(serviceCredential.Object, new OptionsWrapper<GamesOptions>(new GamesOptions {LeagueOfLegends = new LeagueOfLegendsOptions()}));

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            gameOptionsController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await gameOptionsController.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
