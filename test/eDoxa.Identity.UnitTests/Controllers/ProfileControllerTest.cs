// Filename: ProfileControllerTest.cs
// Date Created: 2019-12-28
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Enums;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Api.Controllers;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Services;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.Controllers
{
    public sealed class ProfileControllerTest : UnitTest
    {
        public ProfileControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public async Task CreateProfileAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User();

            var profile = new UserProfile("FirstName", "LastName", Gender.Male);

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(
                    userManager => userManager.CreateProfileAsync(
                        It.IsAny<User>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<Gender>()))
                .ReturnsAsync(DomainValidationResult<UserProfile>.Succeeded(profile))
                .Verifiable();

            var controller = new ProfileController(mockUserManager.Object, TestMapper);

            // Act
            var result = await controller.CreateProfileAsync(
                new CreateProfileRequest
                {
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Gender = profile.Gender.ToEnum<EnumGender>()
                });

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(
                userManager => userManager.CreateProfileAsync(
                    It.IsAny<User>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<Gender>()),
                Times.Once);
        }

        [Fact]
        public async Task FetchProfileAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var user = new User();

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsNotNull<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            var controller = new ProfileController(mockUserManager.Object, TestMapper);

            // Act
            var result = await controller.FetchProfileAsync();

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.GetProfileAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task FetchProfileAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User
            {
                Profile = new UserProfile("FirstName", "LastName", Gender.Male)
            };

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.GetProfileAsync(It.IsAny<User>())).ReturnsAsync(user.Profile).Verifiable();

            var controller = new ProfileController(mockUserManager.Object, TestMapper);

            // Act
            var result = await controller.FetchProfileAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.GetProfileAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task UpdateProfileAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User
            {
                Profile = new UserProfile("FirstName", "LastName", Gender.Male)
            };

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.UpdateProfileAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(DomainValidationResult<UserProfile>.Succeeded(user.Profile))
                .Verifiable();

            var controller = new ProfileController(mockUserManager.Object, TestMapper);

            // Act
            var result = await controller.UpdateProfileAsync(
                new UpdateProfileRequest
                {
                    FirstName = "Bob"
                });

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.UpdateProfileAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }
    }
}
