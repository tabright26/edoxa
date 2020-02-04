// Filename: EmailControllerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Controllers;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.Controllers
{
    public sealed class EmailControllerTest : UnitTest
    {
        public EmailControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public async Task ConfirmEmailAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            TestMock.UserService.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>())).Verifiable();

            TestMock.UserService.Setup(userManager => userManager.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>())).Verifiable();

            var controller = new EmailController(TestMock.UserService.Object, TestMapper);

            // Act
            var result = await controller.ConfirmEmailAsync(Guid.NewGuid().ToString(), "code");

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            TestMock.UserService.Verify(userManager => userManager.FindByIdAsync(It.IsAny<string>()), Times.Once);

            TestMock.UserService.Verify(userManager => userManager.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task ConfirmEmailAsync_ShouldBeOkResult()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@edoxa.gg",
                EmailConfirmed = true
            };

            TestMock.UserService.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();

            TestMock.UserService.Setup(userManager => userManager.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var controller = new EmailController(TestMock.UserService.Object, TestMapper);

            // Act
            var result = await controller.ConfirmEmailAsync(user.Id.ToString(), "code");

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            TestMock.UserService.Verify(userManager => userManager.FindByIdAsync(It.IsAny<string>()), Times.Once);

            TestMock.UserService.Verify(userManager => userManager.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void ConfirmEmailAsync_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            TestMock.UserService.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();

            TestMock.UserService.Setup(userManager => userManager.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed())
                .Verifiable();

            var controller = new EmailController(TestMock.UserService.Object, TestMapper);

            // Act
            var result = new Func<Task>(async () => await controller.ConfirmEmailAsync(Guid.NewGuid().ToString(), "code"));

            // Assert
            result.Should().Throw<InvalidOperationException>();

            TestMock.UserService.Verify(userManager => userManager.FindByIdAsync(It.IsAny<string>()), Times.Once);

            TestMock.UserService.Verify(userManager => userManager.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }
    }
}
