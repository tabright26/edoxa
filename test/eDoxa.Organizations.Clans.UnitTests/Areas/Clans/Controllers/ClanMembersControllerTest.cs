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

using Moq;

using static eDoxa.Organizations.Clans.UnitTests.Helpers.Extensions.MapperExtensions;

namespace eDoxa.Organizations.Clans.UnitTests.Areas.Clans.Controllers
{
    [TestClass]
    public class ClanMemberControllerTest
    {
        [TestMethod]
        public async Task GetByIdAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FetchMembersAsync(It.IsAny<ClanId>())).Verifiable();

            var clanMemberController = new ClanMembersController(mockClanService.Object, Mapper);

            // Act
            var result = await clanMemberController.GetAsync(new ClanId());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockClanService.Verify(clanService => clanService.FetchMembersAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FetchMembersAsync(It.IsAny<ClanId>())).ReturnsAsync(new List<Member>()).Verifiable();

            var clanMemberController = new ClanMembersController(mockClanService.Object, Mapper);

            // Act
            var result = await clanMemberController.GetAsync(new ClanId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FetchMembersAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            var clan = new Clan("Test", "This is a summary", new UserId());

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(clan).Verifiable();

            mockClanService.Setup(clanService => clanService.KickMemberFromClanAsync(It.IsAny<Clan>(), It.IsAny<MemberId>()))
                .ReturnsAsync(new ValidationResult()).Verifiable();

            var clanMemberController = new ClanMembersController(mockClanService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanMemberController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanMemberController.GetAsync(new ClanId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.KickMemberFromClanAsync(It.IsAny<Clan>(), It.IsAny<MemberId>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();
            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).Verifiable();

            mockClanService.Setup(clanService => clanService.KickMemberFromClanAsync(It.IsAny<Clan>(), It.IsAny<MemberId>()))
                .ReturnsAsync(new ValidationResult()).Verifiable();

            var clanMemberController = new ClanMembersController(mockClanService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanMemberController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanMemberController.GetAsync(new ClanId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.KickMemberFromClanAsync(It.IsAny<Clan>(), It.IsAny<MemberId>()), Times.Never);
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            var clan = new Clan("Test", "This is a summary", new UserId());

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(clan).Verifiable();

            mockClanService.Setup(clanService => clanService.KickMemberFromClanAsync(It.IsAny<Clan>(), It.IsAny<MemberId>())).Verifiable();

            var clanMemberController = new ClanMembersController(mockClanService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanMemberController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanMemberController.GetAsync(new ClanId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.KickMemberFromClanAsync(It.IsAny<Clan>(), It.IsAny<MemberId>()), Times.Once);
        }
    }
}
