// Filename: EmailConfirmControllerTest.cs
// Date Created: 2019-08-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Controllers;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Models;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Controllers
{
    [TestClass]
    public sealed class EmailConfirmControllerTest
    {
        [TestMethod]
        public async Task GetAsync_ShouldBeOkResult()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Verifiable();

            var controller = new EmailConfirmController(mockUserManager.Object);

            // Act
            var result = await controller.GetAsync(Guid.NewGuid().ToString(), "code");

            // Assert
            result.Should().BeOfType<OkResult>();

            mockUserManager.Verify(userManager => userManager.FindByIdAsync(It.IsAny<string>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task GetAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>())).Verifiable();

            mockUserManager.Setup(userManager => userManager.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>())).Verifiable();

            var controller = new EmailConfirmController(mockUserManager.Object);

            // Act
            var result = await controller.GetAsync(Guid.NewGuid().ToString(), "code");

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockUserManager.Verify(userManager => userManager.FindByIdAsync(It.IsAny<string>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void GetAsync_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed()).Verifiable();

            var controller = new EmailConfirmController(mockUserManager.Object);

            // Act
            var result = new Func<Task>(async () => await controller.GetAsync(Guid.NewGuid().ToString(), "code"));

            // Assert
            result.Should().Throw<InvalidOperationException>();

            mockUserManager.Verify(userManager => userManager.FindByIdAsync(It.IsAny<string>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }
    }
}
