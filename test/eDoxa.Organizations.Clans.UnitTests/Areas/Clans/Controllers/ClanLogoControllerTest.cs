// Filename: ClansControllerTest.cs
// Date Created: 2019-09-15
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
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
using Microsoft.Win32.SafeHandles;

using Moq;

namespace eDoxa.Organizations.Clans.UnitTests.Areas.Clans.Controllers
{
    [TestClass]
    public class ClanLogoControllerTest
    {
        [TestMethod]
        public async Task GetByIdAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockClanService= new Mock<IClanService>();
            mockClanService.Setup(clanService => clanService.GetClanLogoAsync(It.IsAny<ClanId>())).Verifiable();

            var clanLogoController = new ClanLogoController(mockClanService.Object);

            // Act
            var result = await clanLogoController.GetAsync(new ClanId());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockClanService.Verify(clanService => clanService.GetClanLogoAsync(It.IsAny<ClanId>()), Times.Once);
        }

        //[TestMethod]
        //public async Task GetAsync_ShouldBeOfTypeOkObjectResult()
        //{
        //    // Arrange
        //    var mockClanService = new Mock<IClanService>();

        //    mockClanService.Setup(clanService => clanService.GetClanLogoAsync(It.IsAny<ClanId>())).ReturnsAsync(new FileStream()).Verifiable();

        //    var clanLogoController = new ClanLogoController(mockClanService.Object);

        //    // Act
        //    var result = await clanLogoController.GetAsync(new ClanId());

        //    // Assert
        //    result.Should().BeOfType<OkObjectResult>();

        //    mockClanService.Verify(clanService => clanService.GetClanLogoAsync(It.IsAny<ClanId>()), Times.Once);
        //}

        [TestMethod]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockClanService= new Mock<IClanService>();

            var clan = new Clan("Test", "This is a summary", new UserId());

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(clan).Verifiable();

            mockClanService.Setup(clanService => clanService.CreateOrUpdateClanLogoAsync(It.IsAny<Clan>(), It.IsAny<FileStream>(), It.IsAny<UserId>()))
                .ReturnsAsync(new ValidationResult()).Verifiable();

            var clanLogoController = new ClanLogoController(mockClanService.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanLogoController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            //var result = await clanLogoController.PostAsync(new ClanId(), new FileStream());

            // Assert
            //result.Should().BeOfType<NotFoundObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.CreateOrUpdateClanLogoAsync(It.IsAny<Clan>(), It.IsAny<FileStream>(), It.IsAny<UserId>()), Times.Once);
        }

        [TestMethod]
        public async Task PostAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            var clan = new Clan("Test", "This is a summary", new UserId());

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).Verifiable();

            mockClanService.Setup(clanService => clanService.CreateOrUpdateClanLogoAsync(It.IsAny<Clan>(), It.IsAny<FileStream>(), It.IsAny<UserId>()))
                .ReturnsAsync(new ValidationResult()).Verifiable();

            var clanLogoController = new ClanLogoController(mockClanService.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanLogoController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            //var result = await clanLogoController.PostAsync(new ClanId(), new FileStream());

            // Assert
            //result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.CreateOrUpdateClanLogoAsync(It.IsAny<Clan>(), It.IsAny<FileStream>(), It.IsAny<UserId>()), Times.Never);
        }

        [TestMethod]
        public async Task PostAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            var clan = new Clan("Test", "This is a summary", new UserId());

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(clan).Verifiable();

            mockClanService.Setup(clanService => clanService.CreateOrUpdateClanLogoAsync(It.IsAny<Clan>(), It.IsAny<FileStream>(), It.IsAny<UserId>()))
                .Verifiable();

            var clanLogoController = new ClanLogoController(mockClanService.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanLogoController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            //var result = await clanLogoController.PostAsync(new ClanId(), new FileStream());

            // Assert
            //result.Should().BeOfType<BadRequestObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.CreateOrUpdateClanLogoAsync(It.IsAny<Clan>(), It.IsAny<FileStream>(), It.IsAny<UserId>()), Times.Once);
        }

    }
}
