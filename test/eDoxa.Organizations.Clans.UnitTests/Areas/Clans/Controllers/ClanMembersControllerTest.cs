// Filename: ClanMembersControllerTest.cs
// Date Created: 2019-10-02
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Api.Areas.Clans.Controllers;
using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Services;
using eDoxa.Organizations.Clans.TestHelpers;
using eDoxa.Organizations.Clans.TestHelpers.Fixtures;
using eDoxa.Organizations.Clans.TestHelpers.Mocks;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Organizations.Clans.UnitTests.Areas.Clans.Controllers
{
    public class ClanMemberControllerTest : UnitTest
    {
        public ClanMemberControllerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task DeleteAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(new Clan("Test", new UserId())).Verifiable();

            mockClanService.Setup(clanService => clanService.LeaveClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>()))
                .ReturnsAsync(new ValidationFailure(string.Empty, "Error").ToResult())
                .Verifiable();

            var clanMemberController = new ClanMembersController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanMemberController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanMemberController.DeleteAsync(new ClanId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.LeaveClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync((Clan) null).Verifiable();

            mockClanService.Setup(clanService => clanService.LeaveClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var clanMemberController = new ClanMembersController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanMemberController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanMemberController.DeleteAsync(new ClanId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.LeaveClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>()), Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(new Clan("Test", new UserId())).Verifiable();

            mockClanService.Setup(clanService => clanService.LeaveClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var clanMemberController = new ClanMembersController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanMemberController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanMemberController.DeleteAsync(new ClanId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.LeaveClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(new Clan("Test", new UserId())).Verifiable();

            mockClanService.Setup(clanService => clanService.KickMemberFromClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<MemberId>()))
                .ReturnsAsync(new ValidationFailure(string.Empty, "Error").ToResult())
                .Verifiable();

            var clanMemberController = new ClanMembersController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanMemberController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanMemberController.DeleteByIdAsync(new ClanId(), new MemberId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.KickMemberFromClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<MemberId>()), Times.Once);
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();
            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync((Clan) null).Verifiable();

            mockClanService.Setup(clanService => clanService.KickMemberFromClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<MemberId>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var clanMemberController = new ClanMembersController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanMemberController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanMemberController.DeleteByIdAsync(new ClanId(), new MemberId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.KickMemberFromClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<MemberId>()), Times.Never);
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(new Clan("Test", new UserId())).Verifiable();

            mockClanService.Setup(clanService => clanService.KickMemberFromClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<MemberId>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var clanMemberController = new ClanMembersController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanMemberController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanMemberController.DeleteByIdAsync(new ClanId(), new MemberId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.KickMemberFromClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<MemberId>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(new Clan("Test", new UserId())).Verifiable();

            mockClanService.Setup(clanService => clanService.FetchMembersAsync(It.IsAny<Clan>())).ReturnsAsync(new List<Member>()).Verifiable();

            var clanMemberController = new ClanMembersController(mockClanService.Object, TestMapper);

            // Act
            var result = await clanMemberController.GetAsync(new ClanId());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.FetchMembersAsync(It.IsAny<Clan>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var member = new Member(new Candidature(new UserId(), new ClanId()));

            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(new Clan("Test", new UserId())).Verifiable();

            mockClanService.Setup(clanService => clanService.FetchMembersAsync(It.IsAny<Clan>()))
                .ReturnsAsync(
                    new List<Member>
                    {
                        member,
                        member,
                        member
                    })
                .Verifiable();

            var clanMemberController = new ClanMembersController(mockClanService.Object, TestMapper);

            // Act
            var result = await clanMemberController.GetAsync(new ClanId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.FetchMembersAsync(It.IsAny<Clan>()), Times.Once);
        }
    }
}
