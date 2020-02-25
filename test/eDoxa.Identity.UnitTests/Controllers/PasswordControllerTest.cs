// Filename: PasswordControllerTest.cs
// Date Created: 2019-12-26
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Api.Controllers;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

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
        public async Task ForgotPasswordAsync_ShouldBeOkResult()
        {
            // Arrange
            var user = new User
            {
                Id = new UserId(),
                Email = "admin@edoxa.gg"
            };

            TestMock.UserService.Setup(userManager => userManager.FindByEmailAsync(user.Email)).ReturnsAsync(user).Verifiable();

            TestMock.UserService.Setup(userManager => userManager.GeneratePasswordResetTokenAsync(user)).ReturnsAsync("code").Verifiable();

            TestMock.ServiceBusPublisher.Setup(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<UserPasswordResetTokenGeneratedIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var controller = new PasswordController(TestMock.UserService.Object, TestMock.ServiceBusPublisher.Object);

            // Act
            var result = await controller.ForgotPasswordAsync(
                new ForgotPasswordRequest
                {
                    Email = user.Email
                });

            // Assert
            result.Should().BeOfType<OkResult>();

            TestMock.UserService.Verify(userManager => userManager.FindByEmailAsync(user.Email), Times.Once);

            TestMock.UserService.Verify(userManager => userManager.GeneratePasswordResetTokenAsync(user), Times.Once);

            TestMock.ServiceBusPublisher.Verify(
                serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<UserPasswordResetTokenGeneratedIntegrationEvent>()),
                Times.Once);
        }

        [Fact]
        public async Task ResetPasswordAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var user = new User
            {
                Id = new UserId()
            };

            TestMock.UserService.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();

            TestMock.UserService.Setup(userManager => userManager.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(
                    IdentityResult.Failed(
                        new IdentityError
                        {
                            Code = "code",
                            Description = "description"
                        }))
                .Verifiable();

            var controller = new PasswordController(TestMock.UserService.Object, TestMock.ServiceBusPublisher.Object);

            // Act
            var result = await controller.ResetPasswordAsync(
                new ResetPasswordRequest
                {
                    UserId = new UserId(),
                    Password = "Pass@word1",
                    Code = "Code"
                });

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            TestMock.UserService.Verify(userManager => userManager.FindByIdAsync(It.IsAny<string>()), Times.Once);

            TestMock.UserService.Verify(userManager => userManager.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ResetPasswordAsync_ShouldBeOkResult()
        {
            // Arrange
            var user = new User
            {
                Id = new UserId()
            };

            TestMock.UserService.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();

            TestMock.UserService.Setup(userManager => userManager.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var controller = new PasswordController(TestMock.UserService.Object, TestMock.ServiceBusPublisher.Object);

            // Act
            var result = await controller.ResetPasswordAsync(
                new ResetPasswordRequest
                {
                    UserId = new UserId(),
                    Password = "Pass@word1",
                    Code = "Code"
                });

            // Assert
            result.Should().BeOfType<OkResult>();

            TestMock.UserService.Verify(userManager => userManager.FindByIdAsync(It.IsAny<string>()), Times.Once);

            TestMock.UserService.Verify(userManager => userManager.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ResetPasswordAsync_WhenUserNotFound_ShouldBeOkResult()
        {
            // Arrange
            TestMock.UserService.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>())).Verifiable();

            TestMock.UserService.Setup(userManager => userManager.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(
                    IdentityResult.Failed(
                        new IdentityError
                        {
                            Code = "code",
                            Description = "description"
                        }))
                .Verifiable();

            var controller = new PasswordController(TestMock.UserService.Object, TestMock.ServiceBusPublisher.Object);

            // Act
            var result = await controller.ResetPasswordAsync(
                new ResetPasswordRequest
                {
                    UserId = new UserId(),
                    Password = "Pass@word1",
                    Code = "Code"
                });

            // Assert
            result.Should().BeOfType<OkResult>();

            TestMock.UserService.Verify(userManager => userManager.FindByIdAsync(It.IsAny<string>()), Times.Once);

            TestMock.UserService.Verify(userManager => userManager.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}
