// Filename: PersonalInfoControllerTest.cs
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
    public sealed class PersonalInfoControllerTest
    {
        [TestMethod]
        public async Task GetAsync_WithNewUserPersonnalInfo_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User
            {
                PersonalInfo = new UserPersonalInfo
                {
                    FirstName = "Test",
                    LastName = "Test",
                    Gender = Gender.Male,
                    BirthDate = DateTime.UtcNow.AddDays(-20)
                }
            };

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.GetPersonalInfoAsync(It.IsAny<User>())).ReturnsAsync(user.PersonalInfo).Verifiable();

            var controller = new PersonalInfoController(mockUserManager.Object, Mapper);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(Mapper.Map<UserPersonalInfoResponse>(user.PersonalInfo));

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.GetPersonalInfoAsync(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public async Task GetAsync_WithEmptyUserPersonnalInfo_ShouldBeNoContentResult()
        {
            // Arrange
            var user = new User();

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsNotNull<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            var controller = new PersonalInfoController(mockUserManager.Object, Mapper);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.GetPersonalInfoAsync(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public async Task PatchAsync_WithNewUserPersonnalInfo_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User
            {
                PersonalInfo = new UserPersonalInfo
                {
                    FirstName = "FirstName",
                    LastName = "LastName",
                    Gender = Gender.Male,
                    BirthDate = DateTime.UtcNow.AddDays(-20)
                }
            };

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.GetPersonalInfoAsync(It.IsAny<User>())).ReturnsAsync(user.PersonalInfo).Verifiable();

            mockUserManager.Setup(
                    userManager => userManager.SetPersonalInfoAsync(
                        It.IsAny<User>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<Gender>(),
                        It.IsAny<DateTime?>()
                    )
                )
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var controller = new PersonalInfoController(mockUserManager.Object, Mapper);

            var document = new JsonPatchDocument<PersonalInfoPatchRequest>();

            // Act
            var result = await controller.PatchAsync(document);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeOfType<string>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.GetPersonalInfoAsync(It.IsAny<User>()), Times.Once);

            mockUserManager.Verify(
                userManager => userManager.SetPersonalInfoAsync(
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
        public async Task PatchAsync_WithNewUserPersonnalInfo_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var user = new User
            {
                PersonalInfo = new UserPersonalInfo
                {
                    FirstName = "FirstName",
                    LastName = "LastName",
                    Gender = Gender.Male,
                    BirthDate = DateTime.UtcNow.AddDays(-20)
                }
            };

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.GetPersonalInfoAsync(It.IsAny<User>())).ReturnsAsync(user.PersonalInfo).Verifiable();

            mockUserManager.Setup(
                    userManager => userManager.SetPersonalInfoAsync(
                        It.IsAny<User>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<Gender>(),
                        It.IsAny<DateTime?>()
                    )
                )
                .ReturnsAsync(IdentityResult.Failed())
                .Verifiable();

            var controller = new PersonalInfoController(mockUserManager.Object, Mapper);

            var document = new JsonPatchDocument<PersonalInfoPatchRequest>();

            // Act
            var result = await controller.PatchAsync(document);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            result.As<BadRequestObjectResult>().Should().BeEquivalentTo(new BadRequestObjectResult(controller.ModelState));

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.GetPersonalInfoAsync(It.IsAny<User>()), Times.Once);

            mockUserManager.Verify(
                userManager => userManager.SetPersonalInfoAsync(
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
