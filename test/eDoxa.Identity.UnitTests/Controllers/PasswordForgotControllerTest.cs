// Filename: PasswordForgotControllerTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Grpc.Protos.Notifications.IntegrationEvents;
using eDoxa.Identity.Api.Areas.Identity.Controllers;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Services;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.ServiceBus.Abstractions;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.Controllers
{
    public sealed class PasswordForgotControllerTest : UnitTest
    {
        public PasswordForgotControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task PostAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.FindByEmailAsync(It.IsAny<string>())).Verifiable();

            mockUserManager.Setup(userManager => userManager.IsEmailConfirmedAsync(It.IsAny<User>())).Verifiable();

            mockUserManager.Setup(userManager => userManager.GeneratePasswordResetTokenAsync(It.IsAny<User>())).Verifiable();

            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            mockServiceBusPublisher.Setup(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<EmailSentIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var mockRedirectService = new Mock<IRedirectService>();

            mockRedirectService.Setup(redirectService => redirectService.RedirectToWebSpa(It.IsAny<string>())).Verifiable();

            var controller = new PasswordForgotController(mockUserManager.Object, mockServiceBusPublisher.Object, mockRedirectService.Object);

            controller.ModelState.AddModelError("error", "error");

            // Act
            var result = await controller.PostAsync(
                new ForgotPasswordRequest
                {
                    Email = "admin@edoxa.gg"
                });

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockUserManager.Verify(userManager => userManager.FindByEmailAsync(It.IsAny<string>()), Times.Never);

            mockUserManager.Verify(userManager => userManager.IsEmailConfirmedAsync(It.IsAny<User>()), Times.Never);

            mockUserManager.Verify(userManager => userManager.IsEmailConfirmedAsync(It.IsAny<User>()), Times.Never);

            mockServiceBusPublisher.Verify(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<EmailSentIntegrationEvent>()), Times.Never);

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

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.IsEmailConfirmedAsync(It.IsAny<User>())).ReturnsAsync(true).Verifiable();

            mockUserManager.Setup(userManager => userManager.GeneratePasswordResetTokenAsync(It.IsAny<User>())).ReturnsAsync("code").Verifiable();

            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            mockServiceBusPublisher.Setup(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<EmailSentIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var mockRedirectService = new Mock<IRedirectService>();

            mockRedirectService.Setup(redirectService => redirectService.RedirectToWebSpa(It.IsAny<string>())).Returns("https://edoxa.gg/").Verifiable();

            var controller = new PasswordForgotController(mockUserManager.Object, mockServiceBusPublisher.Object, mockRedirectService.Object);

            // Act
            var result = await controller.PostAsync(
                new ForgotPasswordRequest
                {
                    Email = "admin@edoxa.gg"
                });

            // Assert
            result.Should().BeOfType<OkResult>();

            mockUserManager.Verify(userManager => userManager.FindByEmailAsync(It.IsAny<string>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.IsEmailConfirmedAsync(It.IsAny<User>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.IsEmailConfirmedAsync(It.IsAny<User>()), Times.Once);

            mockServiceBusPublisher.Verify(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<EmailSentIntegrationEvent>()), Times.Once);

            mockRedirectService.Verify(redirectService => redirectService.RedirectToWebSpa(It.IsAny<string>()), Times.Once);
        }
    }
}
