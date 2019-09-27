// Filename: ClansControllerTest.cs
// Date Created: 2019-09-15
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Api.Areas.Clans.Controllers;
using eDoxa.Organizations.Clans.Api.Areas.Clans.Requests;
using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Services;
using eDoxa.Organizations.Clans.UnitTests.Helpers.Mocks;

using FluentAssertions;

using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static eDoxa.Organizations.Clans.UnitTests.Helpers.Extensions.MapperExtensions;

using Moq;

namespace eDoxa.Organizations.Clans.UnitTests.Areas.Clans.Controllers
{
    [TestClass]
    public class ClansControllerTest
    {
        [TestMethod]
        public async Task GetAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockClanService= new Mock<IClanService>();
            mockClanService.Setup(clanService => clanService.FetchClansAsync()).ReturnsAsync(new List<Clan>());

            var clansController = new ClansController(mockClanService.Object, Mapper);

            // Act
            var result = await clansController.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockClanService.Verify(clanService => clanService.FetchClansAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockClanService= new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FetchClansAsync())
                .ReturnsAsync(
                    new List<Clan>
                    {
                        new Clan("Test", new UserId()),
                        new Clan("Test", new UserId()),
                        new Clan("Test", new UserId())
                    });

            var clansController = new ClansController(mockClanService.Object, Mapper);

            // Act
            var result = await clansController.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FetchClansAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();
            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).Verifiable();
            var clanController = new ClansController(mockClanService.Object, Mapper);
            // Act
            var result = await clanController.GetByIdAsync(new ClanId());
            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockClanService= new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(new Clan("Test", new UserId()));

            var clansController = new ClansController(mockClanService.Object, Mapper);

            // Act
            var result = await clansController.GetByIdAsync(new ClanId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [TestMethod]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockClanService= new Mock<IClanService>();
            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).Verifiable();

            mockClanService.Setup(clanService => clanService.CreateClanAsync(It.IsAny<UserId>(), It.IsAny<string>()))
                .ReturnsAsync(new ValidationResult()).Verifiable();

            var clansController = new ClansController(mockClanService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();
            clansController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            var request = new ClanPostRequest("DONTINVADE", "URSSINWINTER");

            // Act
            var result = await clansController.GetByIdAsync(new ClanId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.CreateClanAsync(It.IsAny<UserId>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task PostAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();
            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(new Clan("Test", new UserId())).Verifiable();

            mockClanService.Setup(clanService => clanService.CreateClanAsync(It.IsAny<UserId>(), It.IsAny<string>()))
                .ReturnsAsync(new ValidationResult()).Verifiable();

            var clansController = new ClansController(mockClanService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clansController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            var request = new ClanPostRequest("DONTINVADE", "URSSINWINTER");

            // Act
            var result = await clansController.GetByIdAsync(new ClanId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.CreateClanAsync(It.IsAny<UserId>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task PostAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).Verifiable();

            mockClanService.Setup(clanService => clanService.CreateClanAsync(It.IsAny<UserId>(), It.IsAny<string>())).Verifiable();

            var clansController = new ClansController(mockClanService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clansController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            var request = new ClanPostRequest("DONTINVADE", "URSSINWINTER");

            // Act
            var result = await clansController.GetByIdAsync(new ClanId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.CreateClanAsync(It.IsAny<UserId>(), It.IsAny<string>()), Times.Once);
        }

    }
}
