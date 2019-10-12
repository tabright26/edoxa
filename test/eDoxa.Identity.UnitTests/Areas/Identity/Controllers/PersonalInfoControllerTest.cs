// Filename: PersonalInfoControllerTest.cs
// Date Created: 2019-10-06
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
using eDoxa.Identity.TestHelpers;
using eDoxa.Identity.TestHelpers.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Controllers
{
    public sealed class PersonalInfoControllerTest : UnitTest
    {
        public PersonalInfoControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var user = new User();

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsNotNull<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            var controller = new PersonalInfoController(mockUserManager.Object, TestMapper);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.GetPersonalInfoAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOkObjectResult()
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

            var controller = new PersonalInfoController(mockUserManager.Object, TestMapper);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(TestMapper.Map<UserPersonalInfoResponse>(user.PersonalInfo));

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.GetPersonalInfoAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var user = new User();

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.GetPersonalInfoAsync(It.IsAny<User>())).Verifiable();

            mockUserManager.Setup(
                    userManager => userManager.CreatePersonalInfoAsync(
                        It.IsAny<User>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<Gender>(),
                        It.IsAny<DateTime>()))
                .ReturnsAsync(IdentityResult.Failed())
                .Verifiable();

            var controller = new PersonalInfoController(mockUserManager.Object, TestMapper);

            // Act
            var result = await controller.PostAsync(
                new PersonalInfoPostRequest(
                    "Bob",
                    "Bob",
                    Gender.Male,
                    new DateTime(2000, 1, 1)));

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            result.As<BadRequestObjectResult>().Should().BeEquivalentTo(new BadRequestObjectResult(controller.ModelState));

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.GetPersonalInfoAsync(It.IsAny<User>()), Times.Once);

            mockUserManager.Verify(
                userManager => userManager.CreatePersonalInfoAsync(
                    It.IsAny<User>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<Gender>(),
                    It.IsAny<DateTime>()),
                Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User();

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.GetPersonalInfoAsync(It.IsAny<User>())).Verifiable();

            mockUserManager.Setup(
                    userManager => userManager.CreatePersonalInfoAsync(
                        It.IsAny<User>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<Gender>(),
                        It.IsAny<DateTime>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var controller = new PersonalInfoController(mockUserManager.Object, TestMapper);

            // Act
            var result = await controller.PostAsync(
                new PersonalInfoPostRequest(
                    "Bob",
                    "Bob",
                    Gender.Male,
                    new DateTime(2000, 1, 1)));

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeOfType<string>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.GetPersonalInfoAsync(It.IsAny<User>()), Times.Once);

            mockUserManager.Verify(
                userManager => userManager.CreatePersonalInfoAsync(
                    It.IsAny<User>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<Gender>(),
                    It.IsAny<DateTime>()),
                Times.Once);
        }

        [Fact]
        public async Task PutAsync_ShouldBeBadRequestObjectResult()
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

            mockUserManager.Setup(userManager => userManager.UpdatePersonalInfoAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed())
                .Verifiable();

            var controller = new PersonalInfoController(mockUserManager.Object, TestMapper);

            // Act
            var result = await controller.PutAsync(new PersonalInfoPutRequest("Bob"));

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            result.As<BadRequestObjectResult>().Should().BeEquivalentTo(new BadRequestObjectResult(controller.ModelState));

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.GetPersonalInfoAsync(It.IsAny<User>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.UpdatePersonalInfoAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task PutAsync_ShouldBeOkObjectResult()
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

            mockUserManager.Setup(userManager => userManager.UpdatePersonalInfoAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var controller = new PersonalInfoController(mockUserManager.Object, TestMapper);

            // Act
            var result = await controller.PutAsync(new PersonalInfoPutRequest("Bob"));

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeOfType<string>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.GetPersonalInfoAsync(It.IsAny<User>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.UpdatePersonalInfoAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }
    }
}
