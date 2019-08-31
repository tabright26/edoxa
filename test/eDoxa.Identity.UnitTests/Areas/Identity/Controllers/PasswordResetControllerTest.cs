// Filename: PasswordResetControllerTest.cs
// Date Created: 2019-08-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Controllers;
using eDoxa.Identity.Api.Areas.Identity.Requests;
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
    public sealed class PasswordResetControllerTest
    {
        [TestMethod]
        public async Task PostAsync_ShouldBeOkResult()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var controller = new PasswordResetController(mockUserManager.Object);

            // Act
            var result = await controller.PostAsync(new PasswordResetPostRequest("admin@edoxa.gg", "Pass@word1", "code"));

            // Assert
            result.Should().BeOfType<OkResult>();

            mockUserManager.Verify(userManager => userManager.FindByEmailAsync(It.IsAny<string>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task PostAsync_WhenUserNotFound_ShouldBeOkResult()
        {
            // Arrange
            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.FindByEmailAsync(It.IsAny<string>())).Verifiable();

            mockUserManager.Setup(userManager => userManager.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(
                    IdentityResult.Failed(
                        new IdentityError
                        {
                            Code = "code",
                            Description = "description"
                        }))
                .Verifiable();

            var controller = new PasswordResetController(mockUserManager.Object);

            // Act
            var result = await controller.PostAsync(new PasswordResetPostRequest("admin@edoxa.gg", "Pass@word1", "code"));

            // Assert
            result.Should().BeOfType<OkResult>();

            mockUserManager.Verify(userManager => userManager.FindByEmailAsync(It.IsAny<string>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task PostAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(
                    IdentityResult.Failed(
                        new IdentityError
                        {
                            Code = "code",
                            Description = "description"
                        }))
                .Verifiable();

            var controller = new PasswordResetController(mockUserManager.Object);

            // Act
            var result = await controller.PostAsync(new PasswordResetPostRequest("admin@edoxa.gg", "Pass@word1", "code"));

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockUserManager.Verify(userManager => userManager.FindByEmailAsync(It.IsAny<string>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
