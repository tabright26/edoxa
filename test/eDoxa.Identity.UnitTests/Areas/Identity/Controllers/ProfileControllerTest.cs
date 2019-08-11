// Filename: ProfileControllerTest.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Controllers;
using eDoxa.Identity.Api.Areas.Identity.Requests;
using eDoxa.Identity.Api.Areas.Identity.Responses;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Models;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using static eDoxa.Identity.UnitTests.Helpers.Extensions.MapperExtensions;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Controllers
{
    [TestClass]
    public sealed class ProfileControllerTest
    {
        [TestMethod]
        public async Task GetAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User
            {
                Profile = new Profile
                {
                    FirstName = "Test",
                    LastName = "Test",
                    Gender = Gender.Male,
                    BirthDate = DateTime.UtcNow.AddDays(-20)
                }
            };

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.GetProfileAsync(It.IsAny<User>())).ReturnsAsync(user.Profile).Verifiable();

            var controller = new ProfileController(mockUserManager.Object, Mapper);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(Mapper.Map<ProfileResponse>(user.Profile));

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.GetProfileAsync(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var user = new User();

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsNotNull<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            var controller = new ProfileController(mockUserManager.Object, Mapper);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.GetProfileAsync(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public async Task PatchAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User
            {
                Profile = new Profile
                {
                    FirstName = "FirstName",
                    LastName = "LastName",
                    Gender = Gender.Male,
                    BirthDate = DateTime.UtcNow.AddDays(-20)
                }
            };

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.GetProfileAsync(It.IsAny<User>())).ReturnsAsync(user.Profile).Verifiable();

            mockUserManager.Setup(
                    userManager => userManager.SetProfileAsync(
                        It.IsAny<User>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<Gender>(),
                        It.IsAny<DateTime?>()
                    )
                )
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var controller = new ProfileController(mockUserManager.Object, Mapper);

            var document = new JsonPatchDocument<ProfilePatchRequest>();

            // Act
            var result = await controller.PatchAsync(document);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeOfType<string>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.GetProfileAsync(It.IsAny<User>()), Times.Once);

            mockUserManager.Verify(
                userManager => userManager.SetProfileAsync(
                    It.IsAny<User>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<Gender>(),
                    It.IsAny<DateTime?>()
                ),
                Times.Once
            );
        }

        [TestMethod]
        public async Task PatchAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var user = new User
            {
                Profile = new Profile
                {
                    FirstName = "FirstName",
                    LastName = "LastName",
                    Gender = Gender.Male,
                    BirthDate = DateTime.UtcNow.AddDays(-20)
                }
            };

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.GetProfileAsync(It.IsAny<User>())).ReturnsAsync(user.Profile).Verifiable();

            mockUserManager.Setup(
                    userManager => userManager.SetProfileAsync(
                        It.IsAny<User>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<Gender>(),
                        It.IsAny<DateTime?>()
                    )
                )
                .ReturnsAsync(IdentityResult.Failed())
                .Verifiable();

            var controller = new ProfileController(mockUserManager.Object, Mapper);

            var document = new JsonPatchDocument<ProfilePatchRequest>();

            // Act
            var result = await controller.PatchAsync(document);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            result.As<BadRequestObjectResult>().Should().BeEquivalentTo(new BadRequestObjectResult(controller.ModelState));

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.GetProfileAsync(It.IsAny<User>()), Times.Once);

            mockUserManager.Verify(
                userManager => userManager.SetProfileAsync(
                    It.IsAny<User>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<Gender>(),
                    It.IsAny<DateTime?>()
                ),
                Times.Once
            );
        }
    }
}
