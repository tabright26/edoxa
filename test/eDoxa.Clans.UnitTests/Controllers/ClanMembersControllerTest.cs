// Filename: ClanMembersControllerTest.cs
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
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Clans.UnitTests.Controllers
{
    public class ClanMemberControllerTest : UnitTest
    {
        public ClanMemberControllerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task FetchMembersAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(new Clan("Test", new UserId())).Verifiable();

            mockClanService.Setup(clanService => clanService.FetchMembersAsync(It.IsAny<Clan>())).ReturnsAsync(new List<Member>()).Verifiable();

            var clanMemberController = new ClanMembersController(mockClanService.Object, TestMapper);

            // Act
            var result = await clanMemberController.FetchMembersAsync(new ClanId());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.FetchMembersAsync(It.IsAny<Clan>()), Times.Once);
        }

        [Fact]
        public async Task FetchMembersAsync_ShouldBeOfTypeOkObjectResult()
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
            var result = await clanMemberController.FetchMembersAsync(new ClanId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.FetchMembersAsync(It.IsAny<Clan>()), Times.Once);
        }

        [Fact]
        public async Task KickMemberFromClanAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(new Clan("Test", new UserId())).Verifiable();

            mockClanService.Setup(clanService => clanService.KickMemberFromClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<MemberId>()))
                .ReturnsAsync(DomainValidationResult<Member>.Failure("Error"))
                .Verifiable();

            var clanMemberController = new ClanMembersController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanMemberController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanMemberController.KickMemberFromClanAsync(new ClanId(), new MemberId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.KickMemberFromClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<MemberId>()), Times.Once);
        }

        [Fact]
        public async Task KickMemberFromClanAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();
            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync((Clan) null).Verifiable();

            mockClanService.Setup(clanService => clanService.KickMemberFromClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<MemberId>()))
                .ReturnsAsync(new DomainValidationResult<Member>())
                .Verifiable();

            var clanMemberController = new ClanMembersController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanMemberController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanMemberController.KickMemberFromClanAsync(new ClanId(), new MemberId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.KickMemberFromClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<MemberId>()), Times.Never);
        }

        [Fact]
        public async Task KickMemberFromClanAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(new Clan("Test", new UserId())).Verifiable();

            mockClanService.Setup(clanService => clanService.KickMemberFromClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<MemberId>()))
                .ReturnsAsync(new DomainValidationResult<Member>())
                .Verifiable();

            var clanMemberController = new ClanMembersController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanMemberController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanMemberController.KickMemberFromClanAsync(new ClanId(), new MemberId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.KickMemberFromClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<MemberId>()), Times.Once);
        }

        [Fact]
        public async Task LeaveClanAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(new Clan("Test", new UserId())).Verifiable();

            mockClanService.Setup(clanService => clanService.LeaveClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>()))
                .ReturnsAsync(DomainValidationResult<Clan>.Failure("Error"))
                .Verifiable();

            var clanMemberController = new ClanMembersController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanMemberController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanMemberController.LeaveClanAsync(new ClanId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.LeaveClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task LeaveClanAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync((Clan) null).Verifiable();

            mockClanService.Setup(clanService => clanService.LeaveClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>()))
                .ReturnsAsync(new DomainValidationResult<Clan>())
                .Verifiable();

            var clanMemberController = new ClanMembersController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanMemberController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanMemberController.LeaveClanAsync(new ClanId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.LeaveClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>()), Times.Never);
        }

        [Fact]
        public async Task LeaveClanAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(new Clan("Test", new UserId())).Verifiable();

            mockClanService.Setup(clanService => clanService.LeaveClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>()))
                .ReturnsAsync(new DomainValidationResult<Clan>())
                .Verifiable();

            var clanMemberController = new ClanMembersController(mockClanService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            clanMemberController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await clanMemberController.LeaveClanAsync(new ClanId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.LeaveClanAsync(It.IsAny<Clan>(), It.IsAny<UserId>()), Times.Once);
        }
    }
}
