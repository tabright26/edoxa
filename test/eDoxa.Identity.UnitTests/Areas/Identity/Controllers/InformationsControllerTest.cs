﻿// Filename: InformationsControllerTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Enums;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Api.Application.Services;
using eDoxa.Identity.Api.Areas.Identity.Controllers;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Controllers
{
    public sealed class InformationsControllerTest : UnitTest
    {
        public InformationsControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var user = new User();

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsNotNull<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            var controller = new ProfileController(mockUserManager.Object, TestMapper);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.GetInformationsAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var dob = DateTime.UtcNow.AddDays(-20);

            var user = new User
            {
                Profile = new UserProfile("Test", "Test", Gender.Male, new UserDob(dob))
            };

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.GetInformationsAsync(It.IsAny<User>())).ReturnsAsync(user.Profile).Verifiable();

            var controller = new ProfileController(mockUserManager.Object, TestMapper);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(TestMapper.Map<ProfileDto>(user.Profile));

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.GetInformationsAsync(It.IsAny<User>()), Times.Once);
        }

        //[Fact]
        //public async Task PostAsync_ShouldBeBadRequestObjectResult()
        //{
        //    // Arrange
        //    var user = new User();

        //    var mockUserManager = new Mock<IUserManager>();

        //    mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

        //    mockUserManager.Setup(userManager => userManager.GetInformationsAsync(It.IsAny<User>())).Verifiable();

        //    mockUserManager.Setup(
        //            userManager => userManager.CreateInformationsAsync(
        //                It.IsAny<User>(),
        //                It.IsAny<string>(),
        //                It.IsAny<string>(),
        //                It.IsAny<Gender>(),
        //                It.IsAny<DateTime>()))
        //        .ReturnsAsync(IdentityResult.Failed())
        //        .Verifiable();

        //    var controller = new InformationsController(mockUserManager.Object, TestMapper);

        //    // Act
        //    var result = await controller.PostAsync(
        //        new InformationsPostRequest(
        //            "Bob",
        //            "Bob",
        //            Gender.Male,
        //            new DateTime(2000, 1, 1)));

        //    // Assert
        //    result.Should().BeOfType<BadRequestObjectResult>();

        //    result.As<BadRequestObjectResult>().Should().BeEquivalentTo(new BadRequestObjectResult(controller.ModelState));

        //    mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

        //    mockUserManager.Verify(userManager => userManager.GetInformationsAsync(It.IsAny<User>()), Times.Once);

        //    mockUserManager.Verify(
        //        userManager => userManager.CreateInformationsAsync(
        //            It.IsAny<User>(),
        //            It.IsAny<string>(),
        //            It.IsAny<string>(),
        //            It.IsAny<Gender>(),
        //            It.IsAny<DateTime>()),
        //        Times.Once);
        //}

        [Fact]
        public async Task PostAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User();

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.GetInformationsAsync(It.IsAny<User>())).Verifiable();

            mockUserManager.Setup(
                    userManager => userManager.CreateInformationsAsync(
                        It.IsAny<User>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<Gender>(),
                        It.IsAny<UserDob>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var controller = new ProfileController(mockUserManager.Object, TestMapper);

            // Act
            var result = await controller.PostAsync(
                new CreateProfileRequest
                {
                    FirstName = "Bob",
                    LastName = "Bob",
                    Gender = GenderDto.Male,
                    Dob = new DobDto
                    {
                        Year = 2000,
                        Day = 1,
                        Month = 1
                    }
                });

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeOfType<string>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.GetInformationsAsync(It.IsAny<User>()), Times.Once);

            mockUserManager.Verify(
                userManager => userManager.CreateInformationsAsync(
                    It.IsAny<User>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<Gender>(),
                    It.IsAny<UserDob>()),
                Times.Once);
        }

        //[Fact]
        //public async Task PutAsync_ShouldBeBadRequestObjectResult()
        //{
        //    // Arrange
        //    var user = new User
        //    {
        //        Informations = new UserInformations
        //        {
        //            FirstName = "FirstName",
        //            LastName = "LastName",
        //            Gender = Gender.Male,
        //            BirthDate = DateTime.UtcNow.AddDays(-20)
        //        }
        //    };

        //    var mockUserManager = new Mock<IUserManager>();

        //    mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

        //    mockUserManager.Setup(userManager => userManager.GetInformationsAsync(It.IsAny<User>())).ReturnsAsync(user.Informations).Verifiable();

        //    mockUserManager.Setup(userManager => userManager.UpdateInformationsAsync(It.IsAny<User>(), It.IsAny<string>()))
        //        .ReturnsAsync(IdentityResult.Failed())
        //        .Verifiable();

        //    var controller = new InformationsController(mockUserManager.Object, TestMapper);

        //    // Act
        //    var result = await controller.PutAsync(new InformationsPutRequest("Bob"));

        //    // Assert
        //    result.Should().BeOfType<BadRequestObjectResult>();

        //    result.As<BadRequestObjectResult>().Should().BeEquivalentTo(new BadRequestObjectResult(controller.ModelState));

        //    mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

        //    mockUserManager.Verify(userManager => userManager.GetInformationsAsync(It.IsAny<User>()), Times.Once);

        //    mockUserManager.Verify(userManager => userManager.UpdateInformationsAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        //}

        [Fact]
        public async Task PutAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var dob = DateTime.UtcNow.AddDays(-20);

            var user = new User
            {
                Profile = new UserProfile("FirstName", "LastName", Gender.Male, new UserDob(dob))
            };

            var mockUserManager = new Mock<IUserService>();

            mockUserManager.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            mockUserManager.Setup(userManager => userManager.GetInformationsAsync(It.IsAny<User>())).ReturnsAsync(user.Profile).Verifiable();

            mockUserManager.Setup(userManager => userManager.UpdateInformationsAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            var controller = new ProfileController(mockUserManager.Object, TestMapper);

            // Act
            var result = await controller.PutAsync(new UpdateProfileRequest
            {
                FirstName = "Bob"
            });

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeOfType<string>();

            mockUserManager.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.GetInformationsAsync(It.IsAny<User>()), Times.Once);

            mockUserManager.Verify(userManager => userManager.UpdateInformationsAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }
    }
}
