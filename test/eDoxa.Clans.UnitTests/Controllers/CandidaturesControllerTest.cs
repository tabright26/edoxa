// Filename: CandidaturesControllerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

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
    public sealed class CandidaturesControllerTest : UnitTest
    {
        public CandidaturesControllerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task AcceptCandidatureAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            TestMock.CandidatureService.Setup(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync(new Candidature(new UserId(), new ClanId()))
                .Verifiable();

            TestMock.CandidatureService.Setup(clanService => clanService.AcceptCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()))
                .ReturnsAsync(DomainValidationResult<Candidature>.Failure("Error"))
                .Verifiable();

            var candidatureController = new CandidaturesController(TestMock.CandidatureService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await candidatureController.AcceptCandidatureAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            TestMock.CandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);

            TestMock.CandidatureService.Verify(clanService => clanService.AcceptCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task AcceptCandidatureAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            TestMock.CandidatureService.Setup(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync((Candidature) null)
                .Verifiable();

            TestMock.CandidatureService.Setup(clanService => clanService.AcceptCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()))
                .ReturnsAsync(new DomainValidationResult<Candidature>())
                .Verifiable();

            var candidatureController = new CandidaturesController(TestMock.CandidatureService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await candidatureController.AcceptCandidatureAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            TestMock.CandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);

            TestMock.CandidatureService.Verify(clanService => clanService.AcceptCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()), Times.Never);
        }

        [Fact]
        public async Task AcceptCandidatureAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            TestMock.CandidatureService.Setup(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync(new Candidature(new UserId(), new ClanId()))
                .Verifiable();

            TestMock.CandidatureService.Setup(clanService => clanService.AcceptCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()))
                .ReturnsAsync(new DomainValidationResult<Candidature>())
                .Verifiable();

            var candidatureController = new CandidaturesController(TestMock.CandidatureService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await candidatureController.AcceptCandidatureAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            TestMock.CandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);

            TestMock.CandidatureService.Verify(clanService => clanService.AcceptCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task DeclineCandidatureAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            TestMock.CandidatureService.Setup(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync(new Candidature(new UserId(), new ClanId()))
                .Verifiable();

            TestMock.CandidatureService.Setup(clanService => clanService.DeclineCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()))
                .ReturnsAsync(DomainValidationResult<Candidature>.Failure("Error"))
                .Verifiable();

            var candidatureController = new CandidaturesController(TestMock.CandidatureService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await candidatureController.DeclineCandidatureAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            TestMock.CandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);

            TestMock.CandidatureService.Verify(clanService => clanService.DeclineCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task DeclineCandidatureAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            TestMock.CandidatureService.Setup(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync((Candidature) null)
                .Verifiable();

            TestMock.CandidatureService.Setup(clanService => clanService.DeclineCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()))
                .ReturnsAsync(new DomainValidationResult<Candidature>())
                .Verifiable();

            var candidatureController = new CandidaturesController(TestMock.CandidatureService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await candidatureController.DeclineCandidatureAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            TestMock.CandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);

            TestMock.CandidatureService.Verify(clanService => clanService.DeclineCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()), Times.Never);
        }

        [Fact]
        public async Task DeclineCandidatureAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            TestMock.CandidatureService.Setup(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync(new Candidature(new UserId(), new ClanId()))
                .Verifiable();

            TestMock.CandidatureService.Setup(clanService => clanService.DeclineCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()))
                .ReturnsAsync(new DomainValidationResult<Candidature>())
                .Verifiable();

            var candidatureController = new CandidaturesController(TestMock.CandidatureService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await candidatureController.DeclineCandidatureAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            TestMock.CandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);

            TestMock.CandidatureService.Verify(clanService => clanService.DeclineCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task FetchCandidaturesAsync_WithAllParameters_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var candidatureController = new CandidaturesController(TestMock.CandidatureService.Object, TestMapper);

            // Act
            var result = await candidatureController.FetchCandidaturesAsync(new ClanId(), new UserId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task FetchCandidaturesAsync_WithClanId_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            TestMock.CandidatureService.Setup(candidatureService => candidatureService.FetchCandidaturesAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(new List<Candidature>())
                .Verifiable();

            var candidatureController = new CandidaturesController(TestMock.CandidatureService.Object, TestMapper);

            // Act
            var result = await candidatureController.FetchCandidaturesAsync(new ClanId());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            TestMock.CandidatureService.Verify(clanService => clanService.FetchCandidaturesAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task FetchCandidaturesAsync_WithClanId_ShouldBeOfTypeOkContentResult()
        {
            // Arrange
            TestMock.CandidatureService.Setup(candidatureService => candidatureService.FetchCandidaturesAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(
                    new List<Candidature>
                    {
                        new Candidature(new UserId(), new ClanId()),
                        new Candidature(new UserId(), new ClanId()),
                        new Candidature(new UserId(), new ClanId())
                    })
                .Verifiable();

            var candidatureController = new CandidaturesController(TestMock.CandidatureService.Object, TestMapper);

            // Act
            var result = await candidatureController.FetchCandidaturesAsync(new ClanId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            TestMock.CandidatureService.Verify(clanService => clanService.FetchCandidaturesAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task FetchCandidaturesAsync_WithNullParameters_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var candidatureController = new CandidaturesController(TestMock.CandidatureService.Object, TestMapper);

            // Act
            var result = await candidatureController.FetchCandidaturesAsync();

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task FetchCandidaturesAsync_WithUserId_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            TestMock.CandidatureService.Setup(candidatureService => candidatureService.FetchCandidaturesAsync(It.IsAny<UserId>()))
                .ReturnsAsync(new List<Candidature>())
                .Verifiable();

            var candidatureController = new CandidaturesController(TestMock.CandidatureService.Object, TestMapper);

            // Act
            var result = await candidatureController.FetchCandidaturesAsync(null, new UserId());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            TestMock.CandidatureService.Verify(clanService => clanService.FetchCandidaturesAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task FetchCandidaturesAsync_WithUserId_ShouldBeOfTypeOkContentResult()
        {
            // Arrange
            TestMock.CandidatureService.Setup(candidatureService => candidatureService.FetchCandidaturesAsync(It.IsAny<UserId>()))
                .ReturnsAsync(
                    new List<Candidature>
                    {
                        new Candidature(new UserId(), new ClanId()),
                        new Candidature(new UserId(), new ClanId()),
                        new Candidature(new UserId(), new ClanId())
                    })
                .Verifiable();

            var candidatureController = new CandidaturesController(TestMock.CandidatureService.Object, TestMapper);

            // Act
            var result = await candidatureController.FetchCandidaturesAsync(null, new UserId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            TestMock.CandidatureService.Verify(clanService => clanService.FetchCandidaturesAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task FindCandidatureAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            TestMock.CandidatureService.Setup(candidatureService => candidatureService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync((Candidature) null)
                .Verifiable();

            var candidatureController = new CandidaturesController(TestMock.CandidatureService.Object, TestMapper);

            // Act
            var result = await candidatureController.FindCandidatureAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            TestMock.CandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);
        }

        [Fact]
        public async Task FindCandidatureAsync_ShouldBeOfTypeOkContentResult()
        {
            // Arrange
            TestMock.CandidatureService.Setup(candidatureService => candidatureService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync(new Candidature(new UserId(), new ClanId()))
                .Verifiable();

            var candidatureController = new CandidaturesController(TestMock.CandidatureService.Object, TestMapper);

            // Act
            var result = await candidatureController.FindCandidatureAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            TestMock.CandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);
        }

        [Fact]
        public async Task SendCandidatureAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            TestMock.CandidatureService.Setup(clanService => clanService.SendCandidatureAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()))
                .ReturnsAsync(DomainValidationResult<Candidature>.Failure("Error"))
                .Verifiable();

            var candidatureController = new CandidaturesController(TestMock.CandidatureService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await candidatureController.SendCandidatureAsync(
                new SendCandidatureRequest
                {
                    ClanId = new ClanId(),
                    UserId = new UserId()
                });

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            TestMock.CandidatureService.Verify(clanService => clanService.SendCandidatureAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task SendCandidatureAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            TestMock.CandidatureService.Setup(clanService => clanService.SendCandidatureAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()))
                .ReturnsAsync(new DomainValidationResult<Candidature>())
                .Verifiable();

            var candidatureController = new CandidaturesController(TestMock.CandidatureService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await candidatureController.SendCandidatureAsync(
                new SendCandidatureRequest
                {
                    ClanId = new ClanId(),
                    UserId = new UserId()
                });

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            TestMock.CandidatureService.Verify(clanService => clanService.SendCandidatureAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()), Times.Once);
        }
    }
}
