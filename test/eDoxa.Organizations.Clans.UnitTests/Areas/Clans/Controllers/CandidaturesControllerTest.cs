// Filename: CandidaturesControllerTest.cs
// Date Created: 2019-09-24
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
using eDoxa.Seedwork.Application.Validations.Extensions;

using FluentAssertions;

using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static eDoxa.Organizations.Clans.UnitTests.Helpers.Extensions.MapperExtensions;

using Moq;

namespace eDoxa.Organizations.Clans.UnitTests.Areas.Clans.Controllers
{
    [TestClass]
    public class CandidaturesControllerTest
    {
        [TestMethod]
        public async Task GetByClanIdAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(candidatureService => candidatureService.FetchCandidaturesAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(new List<Candidature>()).Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, Mapper);

            // Act
            var result = await candidatureController.GetAsync(new ClanId());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockCandidatureService.Verify(clanService => clanService.FetchCandidaturesAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [TestMethod]
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

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, Mapper);

            // Act
            var result = await candidatureController.GetAsync(new ClanId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockCandidatureService.Verify(clanService => clanService.FetchCandidaturesAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [TestMethod]
        public async Task GetByUserIdAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(candidatureService => candidatureService.FetchCandidaturesAsync(It.IsAny<UserId>()))
                .ReturnsAsync(new List<Candidature>()).Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, Mapper);

            // Act
            var result = await candidatureController.GetAsync(null, new UserId());

            // Assert
            result.Should().BeOfType<NoContentResult>();
            mockCandidatureService.Verify(clanService => clanService.FetchCandidaturesAsync(It.IsAny<UserId>()), Times.Once);
        }

        [TestMethod]
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

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, Mapper);

            // Act
            var result = await candidatureController.GetAsync(null,new UserId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockCandidatureService.Verify(clanService => clanService.FetchCandidaturesAsync(It.IsAny<UserId>()), Times.Once);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(candidatureService => candidatureService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync((Candidature) null).Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, Mapper);

            // Act
            var result = await candidatureController.GetByIdAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            mockCandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);
        }

        [TestMethod]
        public async Task GetByIdAsync_ShouldBeOfTypeOkContentResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(candidatureService => candidatureService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync(new Candidature(new UserId(), new ClanId())).Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, Mapper);

            // Act
            var result = await candidatureController.GetByIdAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            mockCandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);
        }

        [TestMethod]
        public async Task PostByIdAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync(new Candidature(new UserId(), new ClanId())).Verifiable();

            mockCandidatureService.Setup(clanService => clanService.AcceptCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()))
                .ReturnsAsync(new ValidationResult()).Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            candidatureController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await candidatureController.PostByIdAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockCandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);

            mockCandidatureService.Verify(clanService => clanService.AcceptCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()), Times.Once);
        }

        [TestMethod]
        public async Task PostByIdAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync((Candidature) null).Verifiable();

            mockCandidatureService.Setup(clanService => clanService.AcceptCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()))
                .ReturnsAsync(new ValidationResult()).Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            candidatureController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await candidatureController.PostByIdAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockCandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);

            mockCandidatureService.Verify(clanService => clanService.AcceptCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()), Times.Never);
        }

        [TestMethod]
        public async Task PostByIdAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync(new Candidature(new UserId(), new ClanId())).Verifiable();

            mockCandidatureService.Setup(clanService => clanService.AcceptCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()))
                .ReturnsAsync(new ValidationFailure(string.Empty, "Error").ToResult()).Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            candidatureController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await candidatureController.PostByIdAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockCandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);

            mockCandidatureService.Verify(clanService => clanService.AcceptCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()), Times.Once);
        }

        [TestMethod]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(clanService => clanService.SendCandidatureAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()))
                .ReturnsAsync(new ValidationResult()).Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            candidatureController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await candidatureController.PostAsync(new CandidaturePostRequest(new UserId(), new ClanId()));

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockCandidatureService.Verify(clanService => clanService.SendCandidatureAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()), Times.Once);
        }

        [TestMethod]
        public async Task PostAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();
            
            mockCandidatureService.Setup(clanService => clanService.SendCandidatureAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()))
                .ReturnsAsync(new ValidationFailure(string.Empty, "Error").ToResult()).Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            candidatureController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await candidatureController.PostAsync(new CandidaturePostRequest(new UserId(), new ClanId()));

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            
            mockCandidatureService.Verify(clanService => clanService.SendCandidatureAsync(It.IsAny<UserId>(), It.IsAny<ClanId>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteByIdAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync(new Candidature(new UserId(), new ClanId())).Verifiable();

            mockCandidatureService.Setup(clanService => clanService.DeclineCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()))
                .ReturnsAsync(new ValidationResult()).Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            candidatureController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await candidatureController.DeleteByIdAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockCandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);

            mockCandidatureService.Verify(clanService => clanService.DeclineCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteByIdAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync((Candidature) null).Verifiable();

            mockCandidatureService.Setup(clanService => clanService.DeclineCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()))
                .ReturnsAsync(new ValidationResult()).Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            candidatureController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await candidatureController.DeleteByIdAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockCandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);

            mockCandidatureService.Verify(clanService => clanService.DeclineCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()), Times.Never);
        }

        [TestMethod]
        public async Task DeleteByIdAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockCandidatureService = new Mock<ICandidatureService>();

            mockCandidatureService.Setup(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()))
                .ReturnsAsync(new Candidature(new UserId(), new ClanId())).Verifiable();

            mockCandidatureService.Setup(clanService => clanService.DeclineCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()))
                .ReturnsAsync(new ValidationFailure(string.Empty, "Error").ToResult()).Verifiable();

            var candidatureController = new CandidaturesController(mockCandidatureService.Object, Mapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            candidatureController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await candidatureController.DeleteByIdAsync(new CandidatureId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockCandidatureService.Verify(clanService => clanService.FindCandidatureAsync(It.IsAny<CandidatureId>()), Times.Once);

            mockCandidatureService.Verify(clanService => clanService.DeclineCandidatureAsync(It.IsAny<Candidature>(), It.IsAny<UserId>()), Times.Once);
        }
    }
}
