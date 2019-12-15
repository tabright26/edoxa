// Filename: DoxatagHistoryControllerTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Api.Application.Services;
using eDoxa.Identity.Api.Areas.Identity.Controllers;
using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Controllers
{
    public sealed class DoxatagHistoryControllerTest : UnitTest
    {
        public DoxatagHistoryControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var user = new User();

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsNotNull<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            var mockDoxatagService = new Mock<IDoxatagService>();

            mockDoxatagService.Setup(doxatagService => doxatagService.FetchDoxatagHistoryAsync(It.IsAny<User>())).ReturnsAsync(new Collection<Doxatag>()).Verifiable();

            var controller = new DoxatagHistoryController(mockUserManager.Object, mockDoxatagService.Object, TestMapper);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockDoxatagService.Verify(doxatagService => doxatagService.FetchDoxatagHistoryAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOkObjectResult()
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

            var doxatagHistory = new List<Doxatag> {doxatag};

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            var mockDoxatagService = new Mock<IDoxatagService>();

            mockDoxatagService.Setup(doxatagService => doxatagService.FetchDoxatagHistoryAsync(It.IsAny<User>())).ReturnsAsync(doxatagHistory).Verifiable();

            var controller = new DoxatagHistoryController(mockUserManager.Object, mockDoxatagService.Object, TestMapper);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(TestMapper.Map<IEnumerable<DoxatagDto>>(doxatagHistory));

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockDoxatagService.Verify(doxatagService => doxatagService.FetchDoxatagHistoryAsync(It.IsAny<User>()), Times.Once);
        }

        //[Fact]
        //public async Task PostAsync_ShouldBeBadRequestObjectResult()
        //{
        //    // Arrange
        //    var user = new User();

        //    var mockUserManager = new Mock<IUserManager>();

        //    mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsNotNull<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

        //    mockUserManager.Setup(userManager => userManager.SetDoxatagAsync(It.IsAny<User>(), It.IsAny<string>()))
        //        .ReturnsAsync(IdentityResult.Failed())
        //        .Verifiable();

        //    var controller = new DoxatagHistoryController(mockUserManager.Object, TestMapper);

        //    var request = new DoxatagPostRequest("Doxatag");

        //    // Act
        //    var result = await controller.PostAsync(request);

        //    // Assert
        //    result.Should().BeOfType<BadRequestObjectResult>();

        //    result.As<BadRequestObjectResult>().Should().BeEquivalentTo(new BadRequestObjectResult(controller.ModelState));

        //    mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

        //    mockUserManager.Verify(userManager => userManager.SetDoxatagAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        //}

        [Fact]
        public async Task PostAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            //var address = new Address(
            //    UserId.FromGuid(user.Id),
            //    Country.Canada,
            //    "Line1",
            //    null,
            //    "City",
            //    "State",
            //    "PostalCode");

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            var mockDoxatagService = new Mock<IDoxatagService>();

            mockDoxatagService.Setup(doxatagService => doxatagService.ChangeDoxatagAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var controller = new DoxatagHistoryController(mockUserManager.Object, mockDoxatagService.Object, TestMapper);

            var request = new ChangeDoxatagRequest
            {
                Name = "Doxatag"
            };

            // Act
            var result = await controller.PostAsync(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeOfType<string>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockDoxatagService.Verify(doxatagService => doxatagService.ChangeDoxatagAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }
    }
}
