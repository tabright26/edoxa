// Filename: CandidaturesControllerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright � 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Clans.Api.Controllers;
using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.Domain.Services;
using eDoxa.Clans.TestHelper;
using eDoxa.Clans.TestHelper.Fixtures;
using eDoxa.Clans.TestHelper.Mocks;
using eDoxa.Grpc.Protos.Clans.Requests;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Clans.UnitTests.Controllers
{
    public class CandidaturesControllerTest : UnitTest
    {
        public CandidaturesControllerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync(new Candidature(new UserId(), new ClanId()))
                .Verifiable();

            mockCandidatureService.Setup(clanService => clanService.DeclineCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()))
                .ReturnsAsync(DomainValidationResult.Failure("Error"))
                .Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            candidatureController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await candidatureController.DeleteByIdAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockCandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);

            mockCandidatureService.Verify(clanService => clanService.DeclineCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync((Candidature) null)
                .Verifiable();

            mockCandidatureService.Setup(clanService => clanService.DeclineCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()))
                .ReturnsAsync(new DomainValidationResult())
                .Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            candidatureController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await candidatureController.DeleteByIdAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockCandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);

            mockCandidatureService.Verify(clanService => clanService.DeclineCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()), Times.Never);
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync(new Candidature(new UserId(), new ClanId()))
                .Verifiable();

            mockCandidatureService.Setup(clanService => clanService.DeclineCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()))
                .ReturnsAsync(new DomainValidationResult())
                .Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            candidatureController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await candidatureController.DeleteByIdAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockCandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);

            mockCandidatureService.Verify(clanService => clanService.DeclineCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task GetByClanIdAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(candidatureService => candidatureService.FetchCandidaturesAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(new List<Candidature>())
                .Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, TestMapper);

            // Act
            var result = await candidatureController.GetAsync(new ClanId());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockCandidatureService.Verify(clanService => clanService.FetchCandidaturesAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task GetByClanIdAsync_ShouldBeOfTypeOkContentResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(candidatureService => candidatureService.FetchCandidaturesAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(
                    new List<Candidature>
                    {
                        new Candidature(new UserId(), new ClanId()),
                        new Candidature(new UserId(), new ClanId()),
                        new Candidature(new UserId(), new ClanId())
                    })
                .Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, TestMapper);

            // Act
            var result = await candidatureController.GetAsync(new ClanId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockCandidatureService.Verify(clanService => clanService.FetchCandidaturesAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(candidatureService => candidatureService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync((Candidature) null)
                .Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, TestMapper);

            // Act
            var result = await candidatureController.GetByIdAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            mockCandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldBeOfTypeOkContentResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(candidatureService => candidatureService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync(new Candidature(new UserId(), new ClanId()))
                .Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, TestMapper);

            // Act
            var result = await candidatureController.GetByIdAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockCandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);
        }

        [Fact]
        public async Task GetByUserIdAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(candidatureService => candidatureService.FetchCandidaturesAsync(It.IsAny<UserId>()))
                .ReturnsAsync(new List<Candidature>())
                .Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, TestMapper);

            // Act
            var result = await candidatureController.GetAsync(null, new UserId());

            // Assert
            result.Should().BeOfType<NoContentResult>();
            mockCandidatureService.Verify(clanService => clanService.FetchCandidaturesAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task GetByUserIdAsync_ShouldBeOfTypeOkContentResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(candidatureService => candidatureService.FetchCandidaturesAsync(It.IsAny<UserId>()))
                .ReturnsAsync(
                    new List<Candidature>
                    {
                        new Candidature(new UserId(), new ClanId()),
                        new Candidature(new UserId(), new ClanId()),
                        new Candidature(new UserId(), new ClanId())
                    })
                .Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, TestMapper);

            // Act
            var result = await candidatureController.GetAsync(null, new UserId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockCandidatureService.Verify(clanService => clanService.FetchCandidaturesAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task GetByUserIdAsync_WithAllParameters_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, TestMapper);

            // Act
            var result = await candidatureController.GetAsync(new ClanId(), new UserId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetByUserIdAsync_WithNullParameters_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, TestMapper);

            // Act
            var result = await candidatureController.GetAsync();

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(clanService => clanService.SendCandidatureAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()))
                .ReturnsAsync(DomainValidationResult.Failure("Error"))
                .Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            candidatureController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await candidatureController.PostAsync(
                new SendCandidatureRequest
                {
                    ClanId = new ClanId(),
                    UserId = new UserId()
                });

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockCandidatureService.Verify(clanService => clanService.SendCandidatureAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(clanService => clanService.SendCandidatureAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()))
                .ReturnsAsync(new DomainValidationResult())
                .Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            candidatureController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await candidatureController.PostAsync(
                new SendCandidatureRequest
                {
                    ClanId = new ClanId(),
                    UserId = new UserId()
                });

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockCandidatureService.Verify(clanService => clanService.SendCandidatureAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task PostByIdAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync(new Candidature(new UserId(), new ClanId()))
                .Verifiable();

            mockCandidatureService.Setup(clanService => clanService.AcceptCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()))
                .ReturnsAsync(DomainValidationResult.Failure("Error"))
                .Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            candidatureController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await candidatureController.PostByIdAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockCandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);

            mockCandidatureService.Verify(clanService => clanService.AcceptCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task PostByIdAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync((Candidature) null)
                .Verifiable();

            mockCandidatureService.Setup(clanService => clanService.AcceptCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()))
                .ReturnsAsync(new DomainValidationResult())
                .Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            candidatureController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await candidatureController.PostByIdAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockCandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);

            mockCandidatureService.Verify(clanService => clanService.AcceptCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()), Times.Never);
        }

        [Fact]
        public async Task PostByIdAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync(new Candidature(new UserId(), new ClanId()))
                .Verifiable();

            mockCandidatureService.Setup(clanService => clanService.AcceptCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()))
                .ReturnsAsync(new DomainValidationResult())
                .Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            candidatureController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await candidatureController.PostByIdAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockCandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);

            mockCandidatureService.Verify(clanService => clanService.AcceptCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()), Times.Once);
        }
    }
}
