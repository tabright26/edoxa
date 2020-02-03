// Filename: ClanDivisionsControllerTest.cs
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
    public class ClanDivisionsControllerTest : UnitTest
    {
        public ClanDivisionsControllerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task CreateDivisionAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var userId = new UserId();
            var clan = new Clan("testClan", userId);

            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(clan).Verifiable();

            mockClanService.Setup(
                    clanService => clanService.CreateDivisionAsync(
                        It.IsAny<Clan>(),
                        It.IsAny<UserId>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()))
                .ReturnsAsync(DomainValidationResult<Division>.Failure("Test error"))
                .Verifiable();

            var clanDivisionsController = new ClanDivisionsController(mockClanService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await clanDivisionsController.CreateDivisionAsync(
                new ClanId(),
                new CreateDivisionRequest
                {
                    Name = "test",
                    Description = "division"
                });

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(
                clanService => clanService.CreateDivisionAsync(
                    It.IsAny<Clan>(),
                    It.IsAny<UserId>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task CreateDivisionAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).Verifiable();

            var clanDivisionsController = new ClanDivisionsController(mockClanService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await clanDivisionsController.CreateDivisionAsync(
                new ClanId(),
                new CreateDivisionRequest
                {
                    Name = "test",
                    Description = "division"
                });

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task CreateDivisionAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var userId = new UserId();

            var clan = new Clan("testClan", userId);

            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(clan).Verifiable();

            mockClanService.Setup(
                    clanService => clanService.CreateDivisionAsync(
                        It.IsAny<Clan>(),
                        It.IsAny<UserId>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()))
                .ReturnsAsync(new DomainValidationResult<Division>())
                .Verifiable();

            var clanDivisionsController = new ClanDivisionsController(mockClanService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await clanDivisionsController.CreateDivisionAsync(
                new ClanId(),
                new CreateDivisionRequest
                {
                    Name = "test",
                    Description = "division"
                });

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(
                clanService => clanService.CreateDivisionAsync(
                    It.IsAny<Clan>(),
                    It.IsAny<UserId>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task DeleteDivisionAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var userId = new UserId();

            var clan = new Clan("testClan", userId);

            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(clan).Verifiable();

            mockClanService.Setup(clanService => clanService.DeleteDivisionAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<DivisionId>()))
                .ReturnsAsync(DomainValidationResult<Division>.Failure("Test error"))
                .Verifiable();

            var clanDivisionsController = new ClanDivisionsController(mockClanService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await clanDivisionsController.DeleteDivisionAsync(new ClanId(), new DivisionId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.DeleteDivisionAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<DivisionId>()), Times.Once);
        }

        [Fact]
        public async Task DeleteDivisionAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).Verifiable();

            var clanDivisionsController = new ClanDivisionsController(mockClanService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await clanDivisionsController.DeleteDivisionAsync(new ClanId(), new DivisionId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task DeleteDivisionAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var userId = new UserId();

            var clan = new Clan("testClan", userId);

            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(clan).Verifiable();

            mockClanService.Setup(clanService => clanService.DeleteDivisionAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<DivisionId>()))
                .ReturnsAsync(new DomainValidationResult<Division>())
                .Verifiable();

            var clanDivisionsController = new ClanDivisionsController(mockClanService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await clanDivisionsController.DeleteDivisionAsync(new ClanId(), new DivisionId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(clanService => clanService.DeleteDivisionAsync(It.IsAny<Clan>(), It.IsAny<UserId>(), It.IsAny<DivisionId>()), Times.Once);
        }

        [Fact]
        public async Task FetchDivisionsAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockClanService = new Mock<IClanService>();
            mockClanService.Setup(clanService => clanService.FetchDivisionsAsync(It.IsAny<ClanId>())).ReturnsAsync(new List<Division>()).Verifiable();

            var clanDivisionsController = new ClanDivisionsController(mockClanService.Object, TestMapper);

            // Act
            var result = await clanDivisionsController.FetchDivisionsAsync(new ClanId());

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockClanService.Verify(clanService => clanService.FetchDivisionsAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task FetchDivisionsAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var clanId = new ClanId();

            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FetchDivisionsAsync(It.IsAny<ClanId>()))
                .ReturnsAsync(
                    new List<Division>
                    {
                        new Division(clanId, "Test", "Division"),
                        new Division(clanId, "Test", "Division"),
                        new Division(clanId, "Test", "Division")
                    })
                .Verifiable();

            var clanDivisionsController = new ClanDivisionsController(mockClanService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await clanDivisionsController.FetchDivisionsAsync(clanId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FetchDivisionsAsync(It.IsAny<ClanId>()), Times.Once);
        }

        [Fact]
        public async Task UpdateDivisionAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var userId = new UserId();

            var clan = new Clan("testClan", userId);

            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(clan).Verifiable();

            mockClanService.Setup(
                    clanService => clanService.UpdateDivisionAsync(
                        It.IsAny<Clan>(),
                        It.IsAny<UserId>(),
                        It.IsAny<DivisionId>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()))
                .ReturnsAsync(DomainValidationResult<Division>.Failure("Test error"))
                .Verifiable();

            var clanDivisionsController = new ClanDivisionsController(mockClanService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await clanDivisionsController.UpdateDivisionAsync(
                new ClanId(),
                new DivisionId(),
                new UpdateDivisionRequest
                {
                    Name = "test",
                    Description = "division"
                });

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(
                clanService => clanService.UpdateDivisionAsync(
                    It.IsAny<Clan>(),
                    It.IsAny<UserId>(),
                    It.IsAny<DivisionId>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task UpdateDivisionAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var userId = new UserId();

            var clan = new Clan("testClan", userId);

            var mockClanService = new Mock<IClanService>();

            mockClanService.Setup(clanService => clanService.FindClanAsync(It.IsAny<ClanId>())).ReturnsAsync(clan).Verifiable();

            mockClanService.Setup(
                    clanService => clanService.UpdateDivisionAsync(
                        It.IsAny<Clan>(),
                        It.IsAny<UserId>(),
                        It.IsAny<DivisionId>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()))
                .ReturnsAsync(new DomainValidationResult<Division>())
                .Verifiable();

            var clanDivisionsController = new ClanDivisionsController(mockClanService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await clanDivisionsController.UpdateDivisionAsync(
                new ClanId(),
                new DivisionId(),
                new UpdateDivisionRequest
                {
                    Name = "test",
                    Description = "division"
                });

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockClanService.Verify(clanService => clanService.FindClanAsync(It.IsAny<ClanId>()), Times.Once);

            mockClanService.Verify(
                clanService => clanService.UpdateDivisionAsync(
                    It.IsAny<Clan>(),
                    It.IsAny<UserId>(),
                    It.IsAny<DivisionId>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Once);
        }
    }
}
