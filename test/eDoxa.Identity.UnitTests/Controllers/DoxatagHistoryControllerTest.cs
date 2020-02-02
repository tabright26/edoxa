// Filename: DoxatagHistoryControllerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Api.Controllers;
using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Services;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.Controllers
{
    public sealed class DoxatagHistoryControllerTest : UnitTest
    {
        public DoxatagHistoryControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public async Task ChangeDoxatagAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            var doxatag = new Doxatag(
                UserId.FromGuid(user.Id),
                "Name",
                1000,
                new UtcNowDateTimeProvider());

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            var mockDoxatagService = new Mock<IDoxatagService>();

            mockDoxatagService.Setup(doxatagService => doxatagService.ChangeDoxatagAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(DomainValidationResult<Doxatag>.Succeeded(doxatag))
                .Verifiable();

            var controller = new DoxatagHistoryController(mockUserManager.Object, mockDoxatagService.Object, TestMapper);

            var request = new ChangeDoxatagRequest
            {
                Name = doxatag.Name
            };

            // Act
            var result = await controller.ChangeDoxatagAsync(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockDoxatagService.Verify(doxatagService => doxatagService.ChangeDoxatagAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task FetchDoxatagHistoryAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var user = new User();

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsNotNull<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            var mockDoxatagService = new Mock<IDoxatagService>();

            mockDoxatagService.Setup(doxatagService => doxatagService.FetchDoxatagHistoryAsync(It.IsAny<User>()))
                .ReturnsAsync(new Collection<Doxatag>())
                .Verifiable();

            var controller = new DoxatagHistoryController(mockUserManager.Object, mockDoxatagService.Object, TestMapper);

            // Act
            var result = await controller.FetchDoxatagHistoryAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockDoxatagService.Verify(doxatagService => doxatagService.FetchDoxatagHistoryAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task FetchDoxatagHistoryAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            var doxatag = new Doxatag(
                UserId.FromGuid(user.Id),
                "Name",
                1000,
                new UtcNowDateTimeProvider());

            var doxatagHistory = new List<Doxatag>
            {
                doxatag
            };

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            var mockDoxatagService = new Mock<IDoxatagService>();

            mockDoxatagService.Setup(doxatagService => doxatagService.FetchDoxatagHistoryAsync(It.IsAny<User>())).ReturnsAsync(doxatagHistory).Verifiable();

            var controller = new DoxatagHistoryController(mockUserManager.Object, mockDoxatagService.Object, TestMapper);

            // Act
            var result = await controller.FetchDoxatagHistoryAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(TestMapper.Map<IEnumerable<DoxatagDto>>(doxatagHistory));

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockDoxatagService.Verify(doxatagService => doxatagService.FetchDoxatagHistoryAsync(It.IsAny<User>()), Times.Once);
        }
    }
}
