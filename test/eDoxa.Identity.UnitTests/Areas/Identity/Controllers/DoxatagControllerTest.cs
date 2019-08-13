// Filename: DoxatagControllerTest.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Controllers;
using eDoxa.Identity.Api.Areas.Identity.Requests;
using eDoxa.Identity.Api.Areas.Identity.Responses;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Models;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using static eDoxa.Identity.UnitTests.Helpers.Extensions.MapperExtensions;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Controllers
{
    [TestClass]
    public sealed class DoxaTagControllerTest
    {
        [TestMethod]
        public async Task GetAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User
            {
                DoxaTag = new DoxaTag
                {
                    Name = "Test",
                    Code = 1234
                }
            };

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.GetDoxaTagAsync(It.IsAny<User>())).ReturnsAsync(user.DoxaTag).Verifiable();

            var controller = new DoxaTagController(mockUserManager.Object, Mapper);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(Mapper.Map<DoxaTagResponse>(user.DoxaTag));

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.GetDoxaTagAsync(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var user = new User();

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsNotNull<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            var controller = new DoxaTagController(mockUserManager.Object, Mapper);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.GetDoxaTagAsync(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public async Task PutAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User
            {
                AddressBook = new List<UserAddress>
                {
                    new UserAddress
                    {
                        City = "Test",
                        PostalCode = "Test",
                        Country = "Test",
                        Line1 = "Test"
                    }
                }
            };

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.SetDoxaTagAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var controller = new DoxaTagController(mockUserManager.Object, Mapper);

            var request = new DoxaTagPutRequest("Doxatag");

            // Act
            var result = await controller.PutAsync(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeOfType<string>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.SetDoxaTagAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task PutAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var user = new User();

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsNotNull<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.SetDoxaTagAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed())
                .Verifiable();

            var controller = new DoxaTagController(mockUserManager.Object, Mapper);

            var request = new DoxaTagPutRequest("Doxatag");

            // Act
            var result = await controller.PutAsync(request);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            result.As<BadRequestObjectResult>().Should().BeEquivalentTo(new BadRequestObjectResult(controller.ModelState));

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.SetDoxaTagAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }
    }
}
