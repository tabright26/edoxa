// Filename: PasswordControllerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Api.Controllers;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Services;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.ServiceBus.Abstractions;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.Controllers
{
    public sealed class PasswordControllerTest : UnitTest
    {
        public PasswordControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public async Task ForgotPasswordAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.FindByEmailAsync(It.IsAny<string>())).Verifiable();

            mockUserManager.Setup(userManager => userManager.IsEmailConfirmedAsync(It.IsAny<User>())).Verifiable();

            mockUserManager.Setup(userManager => userManager.GeneratePasswordResetTokenAsync(It.IsAny<User>())).Verifiable();

            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            mockServiceBusPublisher.Setup(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<UserPasswordResetTokenGeneratedIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var controller = new PasswordController(mockUserManager.Object, mockServiceBusPublisher.Object);

            controller.ModelState.AddModelError("error", "error");

            // Act
            var result = await controller.ForgotPasswordAsync(
                new ForgotPasswordRequest
                {
                    Email = "admin@edoxa.gg"
                });

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockUserManager.Verify(userManager => userManager.FindByEmailAsync(It.IsAny<string>()), Times.Never);

            mockUserManager.Verify(userManager => userManager.IsEmailConfirmedAsync(It.IsAny<User>()), Times.Never);

            mockUserManager.Verify(userManager => userManager.IsEmailConfirmedAsync(It.IsAny<User>()), Times.Never);

            mockServiceBusPublisher.Verify(
                serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<UserPasswordResetTokenGeneratedIntegrationEvent>()),
                Times.Never);
        }

        [Fact]
        public async Task ForgotPasswordAsync_ShouldBeOkResult()
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

            mockServiceBusPublisher.Setup(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<UserPasswordResetTokenGeneratedIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var controller = new PasswordController(mockUserManager.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await controller.ForgotPasswordAsync(
                new ForgotPasswordRequest
                {
                    Email = "admin@edoxa.gg"
                });

            // Assert
            result.Should().BeOfType<OkResult>();

            mockUserManager.Verify(userManager => userManager.FindByEmailAsync(It.IsAny<string>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.IsEmailConfirmedAsync(It.IsAny<User>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.IsEmailConfirmedAsync(It.IsAny<User>()), Times.Once);

            mockServiceBusPublisher.Verify(
                serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<UserPasswordResetTokenGeneratedIntegrationEvent>()),
                Times.Once);
        }

        [Fact]
        public async Task ResetPasswordAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            var mockUserManager = new Mock<IUserService>();

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

            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var controller = new PasswordController(mockUserManager.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await controller.ResetPasswordAsync(
                new ResetPasswordRequest
                {
                    Email = "admin@edoxa.gg",
                    Password = "Pass@word1",
                    Code = "Code"
                });

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockUserManager.Verify(userManager => userManager.FindByEmailAsync(It.IsAny<string>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ResetPasswordAsync_ShouldBeOkResult()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var controller = new PasswordController(mockUserManager.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await controller.ResetPasswordAsync(
                new ResetPasswordRequest
                {
                    Email = "admin@edoxa.gg",
                    Password = "Pass@word1",
                    Code = "Code"
                });

            // Assert
            result.Should().BeOfType<OkResult>();

            mockUserManager.Verify(userManager => userManager.FindByEmailAsync(It.IsAny<string>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ResetPasswordAsync_WhenUserNotFound_ShouldBeOkResult()
        {
            // Arrange
            var mockUserManager = new Mock<IUserService>();

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

            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var controller = new PasswordController(mockUserManager.Object, mockServiceBusPublisher.Object);

            // Act
            var result = await controller.ResetPasswordAsync(
                new ResetPasswordRequest
                {
                    Email = "admin@edoxa.gg",
                    Password = "Pass@word1",
                    Code = "Code"
                });

            // Assert
            result.Should().BeOfType<OkResult>();

            mockUserManager.Verify(userManager => userManager.FindByEmailAsync(It.IsAny<string>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
