//// Filename: UserDoxatagControllerTest.cs
//// Date Created: 2019-08-18
//// 
//// ================================================
//// Copyright © 2019, eDoxa. All rights reserved.

//using System;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Threading.Tasks;

//using eDoxa.Identity.Api.Areas.Identity.Controllers;
//using eDoxa.Identity.Api.Areas.Identity.Responses;
//using eDoxa.Identity.Api.Areas.Identity.Services;
//using eDoxa.Identity.Api.Infrastructure.Models;

//using FluentAssertions;

//using Microsoft.AspNetCore.Mvc;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//using Moq;

//using static eDoxa.Identity.UnitTests.Helpers.Extensions.MapperExtensions;

//namespace eDoxa.Identity.UnitTests.Areas.Identity.Controllers
//{
//    [TestClass]
//    public sealed class UserDoxatagControllerTest
//    {
//        [TestMethod]
//        public async Task GetAsync_ShouldBeOkObjectResult()
//        {
//            // Arrange
//            var user = new User
//            {
//                Id = Guid.NewGuid()
//            };

//            user.DoxaTagHistory = new Collection<UserDoxaTag>
//            {
//                new UserDoxaTag
//                {
//                    Id = Guid.NewGuid(),
//                    UserId = user.Id,
//                    Name = "Test",
//                    Code = 1234,
//                    Timestamp = DateTime.UtcNow
//                }
//            };

//            var mockUserManager = new Mock<IUserManager>();

//            mockUserManager.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();

//            mockUserManager.Setup(userManager => userManager.GetDoxaTagAsync(It.IsAny<User>())).ReturnsAsync(user.DoxaTagHistory.First()).Verifiable();

//            var controller = new UserDoxaTagController(mockUserManager.Object, Mapper);

//            // Act
//            var result = await controller.GetAsync(user.Id);

//            // Assert
//            result.Should().BeOfType<OkObjectResult>();

//            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(Mapper.Map<UserDoxaTagResponse>(user.DoxaTagHistory.First()));

//            mockUserManager.Verify(userManager => userManager.FindByIdAsync(It.IsAny<string>()), Times.Once);

//            mockUserManager.Verify(userManager => userManager.GetDoxaTagAsync(It.IsAny<User>()), Times.Once);
//        }

//        [TestMethod]
//        public async Task GetAsync_ShouldBeNoContentResult()
//        {
//            // Arrange
//            var user = new User
//            {
//                Id = Guid.NewGuid(),
//                DoxaTagHistory = new Collection<UserDoxaTag>()
//            };

//            var mockUserManager = new Mock<IUserManager>();

//            mockUserManager.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();

//            mockUserManager.Setup(userManager => userManager.GetDoxaTagAsync(It.IsAny<User>())).ReturnsAsync(user.DoxaTagHistory.FirstOrDefault()).Verifiable();

//            var controller = new UserDoxaTagController(mockUserManager.Object, Mapper);

//            // Act
//            var result = await controller.GetAsync(user.Id);

//            // Assert
//            result.Should().BeOfType<NoContentResult>();

//            mockUserManager.Verify(userManager => userManager.FindByIdAsync(It.IsAny<string>()), Times.Once);

//            mockUserManager.Verify(userManager => userManager.GetDoxaTagAsync(It.IsAny<User>()), Times.Once);
//        }

//        [TestMethod]
//        public async Task GetAsync_ShouldBeNotFoundObjectResult()
//        {
//            // Arrange
//            var mockUserManager = new Mock<IUserManager>();

//            mockUserManager.Setup(userManager => userManager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((User) null).Verifiable();

//            var controller = new UserDoxaTagController(mockUserManager.Object, Mapper);

//            // Act
//            var result = await controller.GetAsync(Guid.NewGuid());

//            // Assert
//            result.Should().BeOfType<NotFoundObjectResult>();

//            result.As<NotFoundObjectResult>().Value.Should().BeOfType<string>();

//            mockUserManager.Verify(userManager => userManager.FindByIdAsync(It.IsAny<string>()), Times.Once);

//            mockUserManager.Verify(userManager => userManager.GetDoxaTagAsync(It.IsAny<User>()), Times.Never);
//        }
//    }
//}
