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
    public class InvitationsControllerTest
    {
        [TestMethod]
        public async Task GetByClanIdAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(invitationService => invitationService.FetchInvitationsAsync(It.IsAny<ClanId>())).Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, Mapper);

            // Act
            var result = await invitationController.GetAsync(new ClanId());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockInvitationService.Verify(clanService => clanService.FetchInvitationsAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [TestMethod]
        public async Task GetByClanIdAsync_ShouldBeOfTypeOkContentResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();
            mockInvitationService.Setup(invitationService => invitationService.FetchInvitationsAsync(It.IsAny<ClanId>())).ReturnsAsync(
                    new List<Invitation>
                    {
                        new Invitation(new UserId(), new ClanId()),
                        new Invitation(new UserId(), new ClanId()),
                        new Invitation(new UserId(), new ClanId())
                    }).Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, Mapper);

            // Act
            var result = await invitationController.GetAsync(new ClanId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockInvitationService.Verify(clanService => clanService.FetchInvitationsAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [TestMethod]
        public async Task GetByUserIdAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();
            mockInvitationService.Setup(invitationService => invitationService.FetchInvitationsAsync(It.IsAny<UserId>())).Verifiable();
            var invitationController = new InvitationsController(mockInvitationService.Object, Mapper);
            // Act
            var result = await invitationController.GetAsync(new UserId());
            // Assert
            result.Should().BeOfType<NoContentResult>();
            mockInvitationService.Verify(clanService => clanService.FetchInvitationsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [TestMethod]
        public async Task GetByUserIdAsync_ShouldBeOfTypeOkContentResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();
            mockInvitationService.Setup(invitationService => invitationService.FetchInvitationsAsync(It.IsAny<UserId>())).ReturnsAsync(
                    new List<Invitation>
                    {
                        new Invitation(new UserId(), new ClanId()),
                        new Invitation(new UserId(), new ClanId()),
                        new Invitation(new UserId(), new ClanId())
                    }).Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, Mapper);

            // Act
            var result = await invitationController.GetAsync(new UserId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockInvitationService.Verify(clanService => clanService.FetchInvitationsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();
            mockInvitationService.Setup(invitationService => invitationService.FindInvitationAsync(It.IsAny<InvitationId>())).ReturnsAsync(
                    new Invitation(new UserId(), new ClanId())).Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, Mapper);

            // Act
            var result = await invitationController.GetByIdAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<NoContentResult>();
            mockInvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldBeOfTypeOkContentResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();
            mockInvitationService.Setup(invitationService => invitationService.FindInvitationAsync(It.IsAny<InvitationId>())).ReturnsAsync(
                    new Invitation(new UserId(), new ClanId())).Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, Mapper);

            // Act
            var result = await invitationController.GetByIdAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockInvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);
        }

        [TestMethod]
        public async Task PostByIdAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>())).ReturnsAsync(
                new Invitation(new UserId(), new ClanId())).Verifiable();

            mockInvitationService.Setup(clanService => clanService.AcceptInvitationAsync(It.IsAny<Invitation>()))
                .ReturnsAsync(new ValidationResult()).Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            invitationController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;


            // Act
            var result = await invitationController.PostAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockInvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);

            mockInvitationService.Verify(clanService => clanService.AcceptInvitationAsync(It.IsAny<Invitation>()), Times.Once);
        }

        [TestMethod]
        public async Task PostByIdAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>())).Verifiable();

            mockInvitationService.Setup(clanService => clanService.AcceptInvitationAsync(It.IsAny<Invitation>()))
                .ReturnsAsync(new ValidationResult()).Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            invitationController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;


            // Act
            var result = await invitationController.PostAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockInvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);

            mockInvitationService.Verify(clanService => clanService.AcceptInvitationAsync(It.IsAny<Invitation>()), Times.Never);
        }

        [TestMethod]
        public async Task PostByIdAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()))
                .ReturnsAsync(new Invitation(new UserId(), new ClanId())).Verifiable();

            mockInvitationService.Setup(clanService => clanService.AcceptInvitationAsync(It.IsAny<Invitation>())).Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            invitationController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await invitationController.PostAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockInvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);

            mockInvitationService.Verify(clanService => clanService.AcceptInvitationAsync(It.IsAny<Invitation>()), Times.Once);
        }

        [TestMethod]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(clanService => clanService.FetchInvitationsAsync(It.IsAny<ClanId>()))
                .Verifiable();

            mockInvitationService.Setup(clanService => clanService.SendInvitationAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            invitationController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await invitationController.PostAsync(new InvitationPostRequest(new UserId(), new ClanId()));

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockInvitationService.Verify(clanService => clanService.FetchInvitationsAsync(It.IsAny<ClanId>()), Times.Once);

            mockInvitationService.Verify(clanService => clanService.SendInvitationAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()), Times.Once);
        }

        [TestMethod]
        public async Task PostAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var clanId = new ClanId();
            var userId = new UserId();

            var invitation = new Invitation(userId, clanId);

            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(clanService => clanService.FetchInvitationsAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(
                    new List<Invitation>
                    {
                        new Invitation(new UserId(), new ClanId()),
                        new Invitation(new UserId(), new ClanId()),
                        invitation
                    })
                .Verifiable();

            mockInvitationService.Setup(clanService => clanService.SendInvitationAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            invitationController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await invitationController.PostAsync(new InvitationPostRequest(userId, clanId));

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockInvitationService.Verify(clanService => clanService.FetchInvitationsAsync(It.IsAny<ClanId>()), Times.Once);

            mockInvitationService.Verify(clanService => clanService.SendInvitationAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()), Times.Never);
        }

        [TestMethod]
        public async Task PostAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(clanService => clanService.FetchInvitationsAsync(It.IsAny<UserId>())).Verifiable();

            mockInvitationService.Setup(clanService => clanService.SendInvitationAsync(It.IsAny<ClanId>(), It.IsAny<UserId>())).Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            invitationController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await invitationController.PostAsync(new InvitationPostRequest(new UserId(), new ClanId()));

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockInvitationService.Verify(clanService => clanService.FetchInvitationsAsync(It.IsAny<ClanId>()), Times.Once);

            mockInvitationService.Verify(clanService => clanService.SendInvitationAsync(It.IsAny<ClanId>(), It.IsAny<UserId>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteByIdAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()))
                .ReturnsAsync(new Invitation(new UserId(), new ClanId())).Verifiable();

            mockInvitationService.Setup(clanService => clanService.DeclineInvitationAsync(It.IsAny<Invitation>()))
                .ReturnsAsync(new ValidationResult()).Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            invitationController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await invitationController.DeleteByIdAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockInvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);

            mockInvitationService.Verify(clanService => clanService.DeclineInvitationAsync(It.IsAny<Invitation>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteByIdAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>())).Verifiable();

            mockInvitationService.Setup(clanService => clanService.DeclineInvitationAsync(It.IsAny<Invitation>())).Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            invitationController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await invitationController.DeleteByIdAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockInvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);

            mockInvitationService.Verify(clanService => clanService.DeclineInvitationAsync(It.IsAny<Invitation>()), Times.Never);
        }

        [TestMethod]
        public async Task DeleteByIdAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockInvitationService = new Mock<IInvitationService>();

            mockInvitationService.Setup(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()))
                .ReturnsAsync(new Invitation(new UserId(), new ClanId())).Verifiable();

            mockInvitationService.Setup(clanService => clanService.DeclineInvitationAsync(It.IsAny<Invitation>())).Verifiable();

            var invitationController = new InvitationsController(mockInvitationService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            invitationController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await invitationController.DeleteByIdAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockInvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);

            mockInvitationService.Verify(clanService => clanService.DeclineInvitationAsync(It.IsAny<Invitation>()), Times.Once);
        }
    }
}


