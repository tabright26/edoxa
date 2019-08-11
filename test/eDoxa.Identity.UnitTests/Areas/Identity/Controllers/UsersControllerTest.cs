// Filename: UsersControllerTest.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Controllers;
using eDoxa.Identity.Api.Areas.Identity.Responses;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using static eDoxa.Identity.UnitTests.Helpers.Extensions.MapperExtensions;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Controllers
{
    [TestClass]
    public sealed class UsersControllerTest
    {
        [TestMethod]
        public async Task GetAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Doxatag = new Doxatag
                {
                    Name = "Test",
                    Discriminator = 234
                }
            };

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.SetupGet(userManager => userManager.Users).Returns(user.ToMockAsyncEnumerable()).Verifiable();

            var controller = new UsersController(mockUserManager.Object, Mapper);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(Mapper.Map<IEnumerable<UserResponse>>(user.ToList()));

            mockUserManager.VerifyGet(userManager => userManager.Users, Times.Once);
        }

        [TestMethod]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.SetupGet(userManager => userManager.Users).Returns(Enumerable.Empty<User>().ToMockAsyncEnumerable()).Verifiable();

            var controller = new UsersController(mockUserManager.Object, Mapper);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockUserManager.VerifyGet(userManager => userManager.Users, Times.Once);
        }

        [TestMethod]
        public async Task GetByAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Doxatag = new Doxatag
                {
                    Name = "Test",
                    Discriminator = 234
                }
            };

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();

            var controller = new UsersController(mockUserManager.Object, Mapper);

            // Act
            var result = await controller.GetByIdAsync(user.Id);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(Mapper.Map<UserResponse>(user));

            mockUserManager.Verify(userManager => userManager.FindByIdAsync(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task GetByAsync_ShouldBeNotFoundResult()
        {
            // Arrange
            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((User) null).Verifiable();

            var controller = new UsersController(mockUserManager.Object, Mapper);

            // Act
            var result = await controller.GetByIdAsync(Guid.NewGuid());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            result.As<NotFoundObjectResult>().Value.Should().BeOfType<string>();

            mockUserManager.Verify(userManager => userManager.FindByIdAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
