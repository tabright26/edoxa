// Filename: InvitationsControllerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright � 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Clans.Api.Controllers;
using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Clans.Requests;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Clans.UnitTests.Controllers
{
    public class InvitationsControllerTest : UnitTest
    {
        public InvitationsControllerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task AcceptInvitationAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            TestMock.InvitationService.Setup(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()))
                .ReturnsAsync(new Invitation(new UserId(), new ClanId()))
                .Verifiable();

            TestMock.InvitationService.Setup(clanService => clanService.AcceptInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()))
                .ReturnsAsync(DomainValidationResult<Invitation>.Failure("Error"))
                .Verifiable();

            var invitationController = new InvitationsController(TestMock.InvitationService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await invitationController.AcceptInvitationAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            TestMock.InvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);

            TestMock.InvitationService.Verify(clanService => clanService.AcceptInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task AcceptInvitationAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            TestMock.InvitationService.Setup(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()))
                .ReturnsAsync((Invitation) null)
                .Verifiable();

            TestMock.InvitationService.Setup(clanService => clanService.AcceptInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()))
                .ReturnsAsync(new DomainValidationResult<Invitation>())
                .Verifiable();

            var invitationController = new InvitationsController(TestMock.InvitationService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await invitationController.AcceptInvitationAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            TestMock.InvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);

            TestMock.InvitationService.Verify(clanService => clanService.AcceptInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()), Times.Never);
        }

        [Fact]
        public async Task AcceptInvitationAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            TestMock.InvitationService.Setup(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()))
                .ReturnsAsync(new Invitation(new UserId(), new ClanId()))
                .Verifiable();

            TestMock.InvitationService.Setup(clanService => clanService.AcceptInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()))
                .ReturnsAsync(new DomainValidationResult<Invitation>())
                .Verifiable();

            var invitationController = new InvitationsController(TestMock.InvitationService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await invitationController.AcceptInvitationAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            TestMock.InvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);

            TestMock.InvitationService.Verify(clanService => clanService.AcceptInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task DeclineInvitationAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            TestMock.InvitationService.Setup(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()))
                .ReturnsAsync(new Invitation(new UserId(), new ClanId()))
                .Verifiable();

            TestMock.InvitationService.Setup(clanService => clanService.DeclineInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()))
                .ReturnsAsync(DomainValidationResult<Invitation>.Failure("Test error"))
                .Verifiable();

            var controller = new InvitationsController(TestMock.InvitationService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await controller.DeclineInvitationAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            TestMock.InvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);

            TestMock.InvitationService.Verify(clanService => clanService.DeclineInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task DeclineInvitationAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            TestMock.InvitationService.Setup(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()))
                .ReturnsAsync((Invitation) null)
                .Verifiable();

            TestMock.InvitationService.Setup(clanService => clanService.DeclineInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()))
                .ReturnsAsync(new DomainValidationResult<Invitation>())
                .Verifiable();

            var invitationController = new InvitationsController(TestMock.InvitationService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await invitationController.DeclineInvitationAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            TestMock.InvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);

            TestMock.InvitationService.Verify(clanService => clanService.DeclineInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()), Times.Never);
        }

        [Fact]
        public async Task DeclineInvitationAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            TestMock.InvitationService.Setup(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()))
                .ReturnsAsync(new Invitation(new UserId(), new ClanId()))
                .Verifiable();

            TestMock.InvitationService.Setup(clanService => clanService.DeclineInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()))
                .ReturnsAsync(new DomainValidationResult<Invitation>())
                .Verifiable();

            var invitationController = new InvitationsController(TestMock.InvitationService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await invitationController.DeclineInvitationAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            TestMock.InvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);

            TestMock.InvitationService.Verify(clanService => clanService.DeclineInvitationAsync(It.IsAny<Invitation>(), It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task FetchInvitationsAsync_WithAllParameters_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var invitationController = new InvitationsController(TestMock.InvitationService.Object, TestMapper);

            // Act
            var result = await invitationController.FetchInvitationsAsync(new ClanId(), new UserId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task FetchInvitationsAsync_WithClanId_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            TestMock.InvitationService.Setup(invitationService => invitationService.FetchInvitationsAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(new List<Invitation>())
                .Verifiable();

            var invitationController = new InvitationsController(TestMock.InvitationService.Object, TestMapper);

            // Act
            var result = await invitationController.FetchInvitationsAsync(new ClanId());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            TestMock.InvitationService.Verify(clanService => clanService.FetchInvitationsAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task FetchInvitationsAsync_WithClanId_ShouldBeOfTypeOkContentResult()
        {
            // Arrange
            TestMock.InvitationService.Setup(invitationService => invitationService.FetchInvitationsAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(
                    new List<Invitation>
                    {
                        new Invitation(new UserId(), new ClanId()),
                        new Invitation(new UserId(), new ClanId()),
                        new Invitation(new UserId(), new ClanId())
                    })
                .Verifiable();

            var invitationController = new InvitationsController(TestMock.InvitationService.Object, TestMapper);

            // Act
            var result = await invitationController.FetchInvitationsAsync(new ClanId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            TestMock.InvitationService.Verify(clanService => clanService.FetchInvitationsAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task FetchInvitationsAsync_WithNullParameters_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var invitationController = new InvitationsController(TestMock.InvitationService.Object, TestMapper);

            // Act
            var result = await invitationController.FetchInvitationsAsync();

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task FetchInvitationsAsync_WithUserId_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            TestMock.InvitationService.Setup(invitationService => invitationService.FetchInvitationsAsync(It.IsAny<UserId>()))
                .ReturnsAsync(new List<Invitation>())
                .Verifiable();

            var invitationController = new InvitationsController(TestMock.InvitationService.Object, TestMapper);

            // Act
            var result = await invitationController.FetchInvitationsAsync(null, new UserId());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            TestMock.InvitationService.Verify(clanService => clanService.FetchInvitationsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task FetchInvitationsAsync_WithUserId_ShouldBeOfTypeOkContentResult()
        {
            // Arrange
            TestMock.InvitationService.Setup(invitationService => invitationService.FetchInvitationsAsync(It.IsAny<UserId>()))
                .ReturnsAsync(
                    new List<Invitation>
                    {
                        new Invitation(new UserId(), new ClanId()),
                        new Invitation(new UserId(), new ClanId()),
                        new Invitation(new UserId(), new ClanId())
                    })
                .Verifiable();

            var invitationController = new InvitationsController(TestMock.InvitationService.Object, TestMapper);

            // Act
            var result = await invitationController.FetchInvitationsAsync(null, new UserId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            TestMock.InvitationService.Verify(clanService => clanService.FetchInvitationsAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task FindInvitationAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            TestMock.InvitationService.Setup(invitationService => invitationService.FindInvitationAsync(It.IsAny<InvitationId>()))
                .ReturnsAsync((Invitation) null)
                .Verifiable();

            var invitationController = new InvitationsController(TestMock.InvitationService.Object, TestMapper);

            // Act
            var result = await invitationController.FindInvitationAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            TestMock.InvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);
        }

        [Fact]
        public async Task FindInvitationAsync_ShouldBeOfTypeOkContentResult()
        {
            // Arrange
            TestMock.InvitationService.Setup(invitationService => invitationService.FindInvitationAsync(It.IsAny<InvitationId>()))
                .ReturnsAsync(new Invitation(new UserId(), new ClanId()))
                .Verifiable();

            var invitationController = new InvitationsController(TestMock.InvitationService.Object, TestMapper);

            // Act
            var result = await invitationController.FindInvitationAsync(new InvitationId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            TestMock.InvitationService.Verify(clanService => clanService.FindInvitationAsync(It.IsAny<InvitationId>()), Times.Once);
        }

        [Fact]
        public async Task SendInvitationAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            TestMock.InvitationService.Setup(clanService => clanService.SendInvitationAsync(It.IsAny<ClanId>(), It.IsAny<UserId>(), It.IsAny<UserId>()))
                .ReturnsAsync(DomainValidationResult<Invitation>.Failure("Error"))
                .Verifiable();

            var invitationController = new InvitationsController(TestMock.InvitationService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await invitationController.SendInvitationAsync(
                new SendInvitationRequest
                {
                    UserId = new UserId(),
                    ClanId = new ClanId()
                });

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            TestMock.InvitationService.Verify(
                clanService => clanService.SendInvitationAsync(It.IsAny<ClanId>(), It.IsAny<UserId>(), It.IsAny<UserId>()),
                Times.Once);
        }

        [Fact]
        public async Task SendInvitationAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            TestMock.InvitationService.Setup(clanService => clanService.SendInvitationAsync(It.IsAny<ClanId>(), It.IsAny<UserId>(), It.IsAny<UserId>()))
                .ReturnsAsync(new DomainValidationResult<Invitation>())
                .Verifiable();

            var invitationController = new InvitationsController(TestMock.InvitationService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await invitationController.SendInvitationAsync(
                new SendInvitationRequest
                {
                    UserId = new UserId(),
                    ClanId = new ClanId()
                });

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            TestMock.InvitationService.Verify(
                clanService => clanService.SendInvitationAsync(It.IsAny<ClanId>(), It.IsAny<UserId>(), It.IsAny<UserId>()),
                Times.Once);
        }
    }
}
