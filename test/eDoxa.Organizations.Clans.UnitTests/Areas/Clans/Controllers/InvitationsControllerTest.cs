// Filename: InvitationsControllerTest.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Api.Areas.Clans.Controllers;
using eDoxa.Organizations.Clans.Api.Areas.Clans.Requests;
using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Services;
using eDoxa.Organizations.Clans.TestHelper;
using eDoxa.Organizations.Clans.TestHelper.Fixtures;
using eDoxa.Organizations.Clans.TestHelper.Mocks;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Organizations.Clans.UnitTests.Areas.Clans.Controllers
{
    public class InvitationsControllerTest : UnitTest
    {
        public InvitationsControllerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()))
                .ReturnsAsync(new Invitation(new UserId(), new ClanId()))
                .Verifiable();

            mockInvitationService.Setup(clanService => clanService.DeclineInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()))
                .ReturnsAsync(new ValidationFailure(string.Empty, "Test error").ToResult())
                .Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            invitationController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await invitationController.DeleteByIdAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockInvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);

            mockInvitationService.Verify(clanService => clanService.DeclineInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>())).ReturnsAsync((Invitation) null).Verifiable();

            mockInvitationService.Setup(clanService => clanService.DeclineInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            invitationController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await invitationController.DeleteByIdAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockInvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);

            mockInvitationService.Verify(clanService => clanService.DeclineInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()), Times.Never);
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()))
                .ReturnsAsync(new Invitation(new UserId(), new ClanId()))
                .Verifiable();

            mockInvitationService.Setup(clanService => clanService.DeclineInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            invitationController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await invitationController.DeleteByIdAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockInvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);

            mockInvitationService.Verify(clanService => clanService.DeclineInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task GetByClanIdAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(invitationService => invitationService.FetchInvitationsAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(new List<Invitation>())
                .Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, TestMapper);

            // Act
            var result = await invitationController.GetAsync(new ClanId());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockInvitationService.Verify(clanService => clanService.FetchInvitationsAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task GetByClanIdAsync_ShouldBeOfTypeOkContentResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(invitationService => invitationService.FetchInvitationsAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(
                    new List<Invitation>
                    {
                        new Invitation(new UserId(), new ClanId()),
                        new Invitation(new UserId(), new ClanId()),
                        new Invitation(new UserId(), new ClanId())
                    })
                .Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, TestMapper);

            // Act
            var result = await invitationController.GetAsync(new ClanId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockInvitationService.Verify(clanService => clanService.FetchInvitationsAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(invitationService => invitationService.FindInvitationAsync(It.IsAny<InvitationId>()))
                .ReturnsAsync((Invitation) null)
                .Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, TestMapper);

            // Act
            var result = await invitationController.GetByIdAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            mockInvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldBeOfTypeOkContentResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(invitationService => invitationService.FindInvitationAsync(It.IsAny<InvitationId>()))
                .ReturnsAsync(new Invitation(new UserId(), new ClanId()))
                .Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, TestMapper);

            // Act
            var result = await invitationController.GetByIdAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockInvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);
        }

        [Fact]
        public async Task GetByUserIdAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(invitationService => invitationService.FetchInvitationsAsync(It.IsAny<UserId>()))
                .ReturnsAsync(new List<Invitation>())
                .Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, TestMapper);

            // Act
            var result = await invitationController.GetAsync(null, new UserId());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockInvitationService.Verify(clanService => clanService.FetchInvitationsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task GetByUserIdAsync_ShouldBeOfTypeOkContentResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(invitationService => invitationService.FetchInvitationsAsync(It.IsAny<UserId>()))
                .ReturnsAsync(
                    new List<Invitation>
                    {
                        new Invitation(new UserId(), new ClanId()),
                        new Invitation(new UserId(), new ClanId()),
                        new Invitation(new UserId(), new ClanId())
                    })
                .Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, TestMapper);

            // Act
            var result = await invitationController.GetAsync(null, new UserId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockInvitationService.Verify(clanService => clanService.FetchInvitationsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(clanService => clanService.SendInvitationAsync(It.IsAny<ClanId>(), It.IsAny<UserId>(), It.IsAny<UserId>()))
                .ReturnsAsync(new ValidationFailure(string.Empty, "Error").ToResult())
                .Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            invitationController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await invitationController.PostAsync(new InvitationPostRequest(new UserId(), new ClanId()));

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockInvitationService.Verify(
                clanService => clanService.SendInvitationAsync(It.IsAny<ClanId>(), It.IsAny<UserId>(), It.IsAny<UserId>()),
                Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(clanService => clanService.SendInvitationAsync(It.IsAny<ClanId>(), It.IsAny<UserId>(), It.IsAny<UserId>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            invitationController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await invitationController.PostAsync(new InvitationPostRequest(new UserId(), new ClanId()));

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockInvitationService.Verify(
                clanService => clanService.SendInvitationAsync(It.IsAny<ClanId>(), It.IsAny<UserId>(), It.IsAny<UserId>()),
                Times.Once);
        }

        [Fact]
        public async Task PostByIdAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()))
                .ReturnsAsync(new Invitation(new UserId(), new ClanId()))
                .Verifiable();

            mockInvitationService.Setup(clanService => clanService.AcceptInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()))
                .ReturnsAsync(new ValidationFailure("", "Error").ToResult())
                .Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            invitationController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await invitationController.PostByIdAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockInvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);

            mockInvitationService.Verify(clanService => clanService.AcceptInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task PostByIdAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>())).ReturnsAsync((Invitation) null).Verifiable();

            mockInvitationService.Setup(clanService => clanService.AcceptInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            invitationController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await invitationController.PostByIdAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockInvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);

            mockInvitationService.Verify(clanService => clanService.AcceptInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()), Times.Never);
        }

        [Fact]
        public async Task PostByIdAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()))
                .ReturnsAsync(new Invitation(new UserId(), new ClanId()))
                .Verifiable();

            mockInvitationService.Setup(clanService => clanService.AcceptInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            invitationController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await invitationController.PostByIdAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockInvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);

            mockInvitationService.Verify(clanService => clanService.AcceptInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()), Times.Once);
        }
    }
}
