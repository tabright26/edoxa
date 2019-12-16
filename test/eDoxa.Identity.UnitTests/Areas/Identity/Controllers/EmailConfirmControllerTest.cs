// Filename: EmailConfirmControllerTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Controllers;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Services;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Controllers
{
    public sealed class EmailConfirmControllerTest: UnitTest
    {
        [Fact]
        public async Task GetAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            var mockUserManager = new Mock<IUserService>();

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

        [Fact]
        public async Task GetAsync_ShouldBeOkResult()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var controller = new EmailConfirmController(mockUserManager.Object);

            // Act
            var result = await controller.GetAsync(Guid.NewGuid().ToString(), "code");

            // Assert
            result.Should().BeOfType<OkResult>();

            mockUserManager.Verify(userManager => userManager.FindByIdAsync(It.IsAny<string>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GetAsync_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed())
                .Verifiable();

            var controller = new EmailConfirmController(mockUserManager.Object);

            // Act
            var result = new Func<Task>(async () => await controller.GetAsync(Guid.NewGuid().ToString(), "code"));

            // Assert
            result.Should().Throw<InvalidOperationException>();

            mockUserManager.Verify(userManager => userManager.FindByIdAsync(It.IsAny<string>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        public EmailConfirmControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }
    }
}
