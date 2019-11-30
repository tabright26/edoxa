// Filename: ClansControllerTest.cs
// Date Created: 2019-10-02
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Clans.Api.Areas.Clans.Controllers;
using eDoxa.Clans.Api.Areas.Clans.Services.Abstractions;
using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.Requests;
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
    public class ClanDivisionsControllerTest : UnitTest
    {
        public ClanDivisionsControllerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();
            mockClanService.Setup(clanService => clanService.FetchDivisionsAsync(It.IsAny<ClanId>())).ReturnsAsync(new List<Division>()).Verifiable();

            var clanDivisionsController = new ClanDivisionsController(mockClanService.Object, TestMapper);

            // Act
            var result = await clanDivisionsController.GetAsync(new ClanId());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockClanService.Verify(clanService => clanService.FetchDivisionsAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var clanId = new ClanId();

            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FetchDivisionsAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(new List<Division>
            {
                new Division(clanId, "Test", "Division"),
                new Division(clanId, "Test", "Division"),
                new Division(clanId, "Test", "Division")
            })
                .Verifiable();

            var clanDivisionsController = new ClanDivisionsController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanDivisionsController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanDivisionsController.GetAsync(clanId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FetchDivisionsAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var userId = new UserId();
            var clan = new Clan("testClan", userId);

            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(clan)
                .Verifiable();

            mockClanService.Setup(clanService => clanService.CreateDivisionAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new ValidationFailure(string.Empty, "Test error").ToResult())
                .Verifiable();

            var clanDivisionsController = new ClanDivisionsController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanDivisionsController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanDivisionsController.PostAsync(new ClanId(), new DivisionPostRequest("test", "division"));

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.CreateDivisionAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()))
                .Verifiable();

            var clanDivisionsController = new ClanDivisionsController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanDivisionsController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanDivisionsController.PostAsync(new ClanId(), new DivisionPostRequest("test", "division"));

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var userId = new UserId();

            var clan = new Clan("testClan", userId);

            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(clan)
                .Verifiable();

            mockClanService.Setup(clanService => clanService.CreateDivisionAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var clanDivisionsController = new ClanDivisionsController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanDivisionsController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanDivisionsController.PostAsync(new ClanId(), new DivisionPostRequest("test", "division"));

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.CreateDivisionAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var userId = new UserId();

            var clan = new Clan("testClan", userId);

            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(clan)
                .Verifiable();

            mockClanService.Setup(clanService => clanService.DeleteDivisionAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<DivisionId>()))
                .ReturnsAsync(new ValidationFailure(string.Empty, "Test error").ToResult())
                .Verifiable();


            var clanDivisionsController = new ClanDivisionsController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanDivisionsController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanDivisionsController.DeleteByIdAsync(new ClanId(), new DivisionId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.DeleteDivisionAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<DivisionId>()), Times.Once);
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()))
                .Verifiable();

            var clanDivisionsController = new ClanDivisionsController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanDivisionsController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanDivisionsController.DeleteByIdAsync(new ClanId(), new DivisionId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var userId = new UserId();

            var clan = new Clan("testClan", userId);

            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(clan)
                .Verifiable();

            mockClanService.Setup(clanService => clanService.DeleteDivisionAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<DivisionId>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var clanDivisionsController = new ClanDivisionsController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanDivisionsController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanDivisionsController.DeleteByIdAsync(new ClanId(), new DivisionId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.DeleteDivisionAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<DivisionId>()), Times.Once);

        }

        [Fact]
        public async Task UpdateByIdAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var userId = new UserId();

            var clan = new Clan("testClan", userId);

            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(clan)
                .Verifiable();

            mockClanService.Setup(clanService => clanService.UpdateDivisionAsync(
                    It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<DivisionId>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new ValidationFailure(string.Empty, "Test error").ToResult())
                .Verifiable();

            var clanDivisionsController = new ClanDivisionsController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanDivisionsController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanDivisionsController.UpdateByIdAsync(new ClanId(), new DivisionId(), new DivisionPostRequest("test", "division"));

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.UpdateDivisionAsync(
                It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<DivisionId>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task UpdateByIdAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();
            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()))
                .Verifiable();
            var clanDivisionsController = new ClanDivisionsController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanDivisionsController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanDivisionsController.DeleteByIdAsync(new ClanId(), new DivisionId());
            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);
        }
        [Fact]
        public async Task UpdateByIdAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var userId = new UserId();

            var clan = new Clan("testClan", userId);

            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(clan)
                .Verifiable();

            mockClanService.Setup(clanService => clanService.UpdateDivisionAsync(
                    It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<DivisionId>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var clanDivisionsController = new ClanDivisionsController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanDivisionsController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanDivisionsController.UpdateByIdAsync(new ClanId(), new DivisionId(), new DivisionPostRequest("test", "division"));

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.UpdateDivisionAsync(
                It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<DivisionId>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
