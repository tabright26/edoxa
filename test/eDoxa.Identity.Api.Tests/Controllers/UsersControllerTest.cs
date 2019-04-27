// Filename: UsersControllerTest.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Identity.Application.Services;
using eDoxa.Identity.Controllers;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.DTO;
using eDoxa.Identity.DTO.Queries;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Identity.Api.Tests.Controllers
{
    [TestClass]
    public sealed class UsersControllerTest
    {
        private Mock<IUserQueries> _queries;
        private Mock<IUserService> _service;

        [TestInitialize]
        public void TestInitialize()
        {
            _queries = new Mock<IUserQueries>();
            _service = new Mock<IUserService>();
        }

        [TestMethod]
        public async Task FindUsersAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            _queries.Setup(service => service.FindUsersAsync())
                .ReturnsAsync(
                    new UserListDTO
                    {
                        Items =
                        {
                            new UserDTO()
                        }
                    }
                )
                .Verifiable();

            var controller = new UsersController(_queries.Object, _service.Object);

            // Act
            var result = await controller.FindUsersAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _service.Verify();
        }

        [TestMethod]
        public async Task FindUsersAsync_ShouldBeNoContentObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindUsersAsync()).ReturnsAsync(new UserListDTO()).Verifiable();

            var controller = new UsersController(_queries.Object, _service.Object);

            // Act
            var result = await controller.FindUsersAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _service.Verify();
        }

        [TestMethod]
        public async Task ChangeUserStatusAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            _service.Setup(service => service.UserExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true).Verifiable();

            _service.Setup(service => service.ChangeStatusAsync(It.IsAny<Guid>(), It.IsAny<UserStatus>())).Returns(Task.CompletedTask).Verifiable();

            var controller = new UsersController(_queries.Object, _service.Object);

            // Act
            var result = await controller.ChangeUserStatusAsync(It.IsAny<Guid>(), It.IsAny<UserStatus>());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _service.Verify();
        }

        [TestMethod]
        public async Task ChangeUserStatusAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            _service.Setup(service => service.UserExistsAsync(It.IsAny<Guid>())).ReturnsAsync(false).Verifiable();

            var controller = new UsersController(_queries.Object, _service.Object);

            // Act
            var result = await controller.ChangeUserStatusAsync(It.IsAny<Guid>(), It.IsAny<UserStatus>());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            _service.Verify();
        }
    }
}