// Filename: PasswordForgotControllerTest.cs
// Date Created: 2019-09-16
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

using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Controllers
{
    public sealed class PasswordForgotControllerTest
    {
        [Fact]
        public async Task PostAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.FindByEmailAsync(It.IsAny<string>())).Verifiable();

            mockUserManager.Setup(userManager => userManager.IsEmailConfirmedAsync(It.IsAny<User>())).Verifiable();

            mockUserManager.Setup(userManager => userManager.GeneratePasswordResetTokenAsync(It.IsAny<User>())).Verifiable();

            var mockEmailSender = new Mock<IEmailSender>();

            mockEmailSender.Setup(emailSender => emailSender.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            var mockRedirectService = new Mock<IRedirectService>();

            mockRedirectService.Setup(redirectService => redirectService.RedirectToWebSpa(It.IsAny<string>())).Verifiable();

            var controller = new PasswordForgotController(mockUserManager.Object, mockEmailSender.Object, mockRedirectService.Object);

            controller.ModelState.AddModelError("error", "error");

            // Act
            var result = await controller.PostAsync(new PasswordForgotPostRequest("admin@edoxa.gg"));

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockUserManager.Verify(userManager => userManager.FindByEmailAsync(It.IsAny<string>()), Times.Never);

            mockUserManager.Verify(userManager => userManager.IsEmailConfirmedAsync(It.IsAny<User>()), Times.Never);

            mockUserManager.Verify(userManager => userManager.IsEmailConfirmedAsync(It.IsAny<User>()), Times.Never);

            mockEmailSender.Verify(emailSender => emailSender.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);

            mockRedirectService.Verify(redirectService => redirectService.RedirectToWebSpa(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOkResult()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.IsEmailConfirmedAsync(It.IsAny<User>())).ReturnsAsync(true).Verifiable();

            mockUserManager.Setup(userManager => userManager.GeneratePasswordResetTokenAsync(It.IsAny<User>())).ReturnsAsync("code").Verifiable();

            var mockEmailSender = new Mock<IEmailSender>();

            mockEmailSender.Setup(emailSender => emailSender.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var mockRedirectService = new Mock<IRedirectService>();

            mockRedirectService.Setup(redirectService => redirectService.RedirectToWebSpa(It.IsAny<string>())).Returns("https://edoxa.gg/").Verifiable();

            var controller = new PasswordForgotController(mockUserManager.Object, mockEmailSender.Object, mockRedirectService.Object);

            // Act
            var result = await controller.PostAsync(new PasswordForgotPostRequest("admin@edoxa.gg"));

            // Assert
            result.Should().BeOfType<OkResult>();

            mockUserManager.Verify(userManager => userManager.FindByEmailAsync(It.IsAny<string>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.IsEmailConfirmedAsync(It.IsAny<User>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.IsEmailConfirmedAsync(It.IsAny<User>()), Times.Once);

            mockEmailSender.Verify(emailSender => emailSender.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            mockRedirectService.Verify(redirectService => redirectService.RedirectToWebSpa(It.IsAny<string>()), Times.Once);
        }
    }
}
