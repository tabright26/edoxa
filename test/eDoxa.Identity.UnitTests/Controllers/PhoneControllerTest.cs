// Filename: PhoneControllerTest.cs
// Date Created: 2020-02-04
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Requests;
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
    public sealed class PhoneControllerTest : UnitTest
    {
        public PhoneControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        private const string TestPhoneNumber = "1-800-123-4567";

        private static User GenerateUser()
        {
            return new User
            {
                Id = Guid.NewGuid()
            };
        }

        [Fact]
        public async Task ChangePhoneAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            TestMock.UserService.Setup(userService => userService.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(GenerateUser()).Verifiable();

            TestMock.UserService.Setup(userService => userService.UpdatePhoneNumberAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed())
                .Verifiable();

            var controller = new PhoneController(TestMock.UserService.Object, TestMapper);

            var request = new ChangePhoneRequest
            {
                Number = TestPhoneNumber
            };

            // Act
            var result = await controller.ChangePhoneAsync(request);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            TestMock.UserService.Verify(userService => userService.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);
            TestMock.UserService.Verify(userService => userService.UpdatePhoneNumberAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ChangePhoneAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            TestMock.UserService.Setup(userService => userService.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(GenerateUser()).Verifiable();

            TestMock.UserService.Setup(userService => userService.UpdatePhoneNumberAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var controller = new PhoneController(TestMock.UserService.Object, TestMapper);

            var request = new ChangePhoneRequest
            {
                Number = TestPhoneNumber
            };

            // Act
            var result = await controller.ChangePhoneAsync(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(TestMapper.Map<PhoneDto>(TestPhoneNumber));
            TestMock.UserService.Verify(userService => userService.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);
            TestMock.UserService.Verify(userService => userService.UpdatePhoneNumberAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task FindPhoneAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            TestMock.UserService.Setup(userService => userService.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(GenerateUser()).Verifiable();

            TestMock.UserService.Setup(userService => userService.GetPhoneNumberAsync(It.IsAny<User>())).Verifiable();

            var controller = new PhoneController(TestMock.UserService.Object, TestMapper);

            // Act
            var result = await controller.FindPhoneAsync();

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            TestMock.UserService.Verify(userService => userService.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);
            TestMock.UserService.Verify(userService => userService.GetPhoneNumberAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task FindPhoneAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            TestMock.UserService.Setup(userService => userService.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(GenerateUser()).Verifiable();

            TestMock.UserService.Setup(userService => userService.GetPhoneNumberAsync(It.IsAny<User>())).ReturnsAsync(TestPhoneNumber).Verifiable();

            var controller = new PhoneController(TestMock.UserService.Object, TestMapper);

            // Act
            var result = await controller.FindPhoneAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(TestMapper.Map<PhoneDto>(TestPhoneNumber));
            TestMock.UserService.Verify(userService => userService.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);
            TestMock.UserService.Verify(userService => userService.GetPhoneNumberAsync(It.IsAny<User>()), Times.Once);
        }
    }
}
