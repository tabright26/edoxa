// Filename: ClansControllerTest.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Clans.Api.Areas.Clans.Controllers;
using eDoxa.Clans.Api.Areas.Clans.Requests;
using eDoxa.Clans.Api.Areas.Clans.Services.Abstractions;
using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;
using eDoxa.Clans.TestHelper.Mocks;
using eDoxa.Seedwork.Application.FluentValidation.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Clans.UnitTests.Areas.Clans.Controllers
{
    public class ClansControllerTest : UnitTest
    {
        public ClansControllerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();
            mockClanService.Setup(clanService => clanService.FetchClansAsync()).ReturnsAsync(new List<Clan>()).Verifiable();

            var clansController = new ClansController(mockClanService.Object, TestMapper);

            // Act
            var result = await clansController.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockClanService.Verify(clanService => clanService.FetchClansAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FetchClansAsync())
                .ReturnsAsync(
                    new List<Clan>
                    {
                        new Clan("Test", new UserId()),
                        new Clan("Test", new UserId()),
                        new Clan("Test", new UserId())
                    })
                .Verifiable();

            var clansController = new ClansController(mockClanService.Object, TestMapper);

            // Act
            var result = await clansController.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FetchClansAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync((Clan) null).Verifiable();

            var clanController = new ClansController(mockClanService.Object, TestMapper);

            // Act
            var result = await clanController.GetByIdAsync(new ClanId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(new Clan("Test", new UserId())).Verifiable();

            var clansController = new ClansController(mockClanService.Object, TestMapper);

            // Act
            var result = await clansController.GetByIdAsync(new ClanId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.CreateClanAsync(It.IsAny<UserId>(), It.IsAny<string>()))
                .ReturnsAsync(new ValidationFailure(string.Empty, "Test error").ToResult())
                .Verifiable();

            var clansController = new ClansController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clansController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            var request = new ClanPostRequest("DONTINVADE", "URSSINWINTER");

            // Act
            var result = await clansController.PostAsync(request);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockClanService.Verify(clanService => clanService.CreateClanAsync(It.IsAny<UserId>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.CreateClanAsync(It.IsAny<UserId>(), It.IsAny<string>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var clansController = new ClansController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clansController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            var request = new ClanPostRequest("DONTINVADE", "URSSINWINTER");

            // Act
            var result = await clansController.PostAsync(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.CreateClanAsync(It.IsAny<UserId>(), It.IsAny<string>()), Times.Once);
        }
    }
}
