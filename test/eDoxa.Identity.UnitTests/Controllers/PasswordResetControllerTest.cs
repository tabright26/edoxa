﻿// Filename: PasswordResetControllerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Api.Controllers;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Services;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.Controllers
{
    public sealed class PasswordResetControllerTest : UnitTest
    {
        public PasswordResetControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public async Task PostAsync_ShouldBeBadRequestObjectResult()
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

            var controller = new PasswordResetController(mockUserManager.Object);

            // Act
            var result = await controller.PostAsync(
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
        public async Task PostAsync_ShouldBeOkResult()
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

            var controller = new PasswordResetController(mockUserManager.Object);

            // Act
            var result = await controller.PostAsync(
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
        public async Task PostAsync_WhenUserNotFound_ShouldBeOkResult()
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

            var controller = new PasswordResetController(mockUserManager.Object);

            // Act
            var result = await controller.PostAsync(
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
