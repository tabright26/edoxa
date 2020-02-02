// Filename: ClansControllerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Clans.Api.Controllers;
using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.Domain.Services;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;
using eDoxa.Clans.TestHelper.Mocks;
using eDoxa.Grpc.Protos.Clans.Requests;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Clans.UnitTests.Controllers
{
    public class ClansControllerTest : UnitTest
    {
        public ClansControllerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task CreateClanAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.CreateClanAsync(It.IsAny<UserId>(), It.IsAny<string>()))
                .ReturnsAsync(DomainValidationResult<Clan>.Failure("Test error"))
                .Verifiable();

            var clansController = new ClansController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clansController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            var request = new CreateClanRequest
            {
                Name = "DONTINVADE",
                Summary = "URSSINWINTER"
            };

            // Act
            var result = await clansController.CreateClanAsync(request);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockClanService.Verify(clanService => clanService.CreateClanAsync(It.IsAny<UserId>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task CreateClanAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.CreateClanAsync(It.IsAny<UserId>(), It.IsAny<string>()))
                .ReturnsAsync(new DomainValidationResult<Clan>())
                .Verifiable();

            var clansController = new ClansController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clansController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            var request = new CreateClanRequest
            {
                Name = "DONTINVADE",
                Summary = "URSSINWINTER"
            };

            // Act
            var result = await clansController.CreateClanAsync(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.CreateClanAsync(It.IsAny<UserId>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task FetchClansAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();
            mockClanService.Setup(clanService => clanService.FetchClansAsync()).ReturnsAsync(new List<Clan>()).Verifiable();

            var clansController = new ClansController(mockClanService.Object, TestMapper);

            // Act
            var result = await clansController.FetchClansAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockClanService.Verify(clanService => clanService.FetchClansAsync(), Times.Once);
        }

        [Fact]
        public async Task FetchClansAsync_ShouldBeOfTypeOkObjectResult()
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
            var result = await clansController.FetchClansAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FetchClansAsync(), Times.Once);
        }

        [Fact]
        public async Task FindClanAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync((Clan) null).Verifiable();

            var clanController = new ClansController(mockClanService.Object, TestMapper);

            // Act
            var result = await clanController.FindClanAsync(new ClanId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task FindClanAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(new Clan("Test", new UserId())).Verifiable();

            var clansController = new ClansController(mockClanService.Object, TestMapper);

            // Act
            var result = await clansController.FindClanAsync(new ClanId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);
        }
    }
}
