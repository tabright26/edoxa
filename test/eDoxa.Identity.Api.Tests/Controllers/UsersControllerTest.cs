// Filename: UsersControllerTest.cs
// Date Created: 2019-04-02
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Identity.Application.Services;
using eDoxa.Identity.Controllers;
using eDoxa.Identity.Properties;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.DTO;
using eDoxa.Identity.DTO.Queries;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Identity.Api.Tests.Controllers
{
    [TestClass]
    public sealed class UsersControllerTest
    {
        private Mock<ILogger<UsersController>> _logger;
        private Mock<IUserQueries> _queries;
        private Mock<IUserService> _service;

        [TestInitialize]
        public void TestInitialize()
        {
            _logger = new Mock<ILogger<UsersController>>();
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

            var controller = new UsersController(_logger.Object, _queries.Object, _service.Object);

            // Act
            var result = await controller.FindUsersAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _logger.VerifyNoOtherCalls();
            _service.Verify();
        }

        [TestMethod]
        public async Task FindUsersAsync_ShouldBeNoContentObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindUsersAsync()).ReturnsAsync(new UserListDTO()).Verifiable();

            var controller = new UsersController(_logger.Object, _queries.Object, _service.Object);

            // Act
            var result = await controller.FindUsersAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _logger.VerifyNoOtherCalls();
            _service.Verify();
        }

        [TestMethod]
        public async Task FindUsersAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindUsersAsync()).ThrowsAsync(new Exception()).Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new UsersController(_logger.Object, _queries.Object, _service.Object);

            // Act
            var result = await controller.FindUsersAsync();

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be(Resources.UsersController_BadRequest_FetchUsersAsync);

            _logger.Verify();
            _service.Verify();
        }

        [TestMethod]
        public async Task ChangeUserTagAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            _service.Setup(service => service.UserExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true).Verifiable();

            _service.Setup(service => service.ChangeTagAsync(It.IsAny<Guid>(), It.IsAny<string>())).Returns(Task.CompletedTask).Verifiable();

            var controller = new UsersController(_logger.Object, _queries.Object, _service.Object);

            // Act
            var result = await controller.ChangeUserTagAsync(It.IsAny<Guid>(), It.IsAny<string>());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().Be(Resources.UsersController_Ok_ChangeUserTagAsync);

            _logger.VerifyNoOtherCalls();
            _service.Verify();
        }

        [TestMethod]
        public async Task ChangeUserTagAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            _service.Setup(service => service.UserExistsAsync(It.IsAny<Guid>())).ReturnsAsync(false).Verifiable();

            var controller = new UsersController(_logger.Object, _queries.Object, _service.Object);

            // Act
            var result = await controller.ChangeUserTagAsync(It.IsAny<Guid>(), It.IsAny<string>());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            result.As<NotFoundObjectResult>().Value.Should().Be(Resources.UsersController_NotFound_ChangeUserTagAsync);

            _logger.VerifyNoOtherCalls();
            _service.Verify();
        }

        [TestMethod]
        public async Task ChangeUserTagAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            _service.Setup(service => service.UserExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true).Verifiable();

            _service.Setup(service => service.ChangeTagAsync(It.IsAny<Guid>(), It.IsAny<string>())).ThrowsAsync(new Exception()).Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new UsersController(_logger.Object, _queries.Object, _service.Object);

            // Act
            var result = await controller.ChangeUserTagAsync(It.IsAny<Guid>(), It.IsAny<string>());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be(Resources.UsersController_BadRequest_ChangeUserTagAsync);

            _logger.Verify();
            _service.Verify();
        }

        [TestMethod]
        public async Task ChangeUserStatusAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            _service.Setup(service => service.UserExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true).Verifiable();

            _service.Setup(service => service.ChangeStatusAsync(It.IsAny<Guid>(), It.IsAny<UserStatus>())).Returns(Task.CompletedTask).Verifiable();

            var controller = new UsersController(_logger.Object, _queries.Object, _service.Object);

            // Act
            var result = await controller.ChangeUserStatusAsync(It.IsAny<Guid>(), It.IsAny<UserStatus>());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().Be(Resources.UsersController_Ok_ChangeUserStatusAsync);

            _logger.VerifyNoOtherCalls();
            _service.Verify();
        }

        [TestMethod]
        public async Task ChangeUserStatusAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            _service.Setup(service => service.UserExistsAsync(It.IsAny<Guid>())).ReturnsAsync(false).Verifiable();

            var controller = new UsersController(_logger.Object, _queries.Object, _service.Object);

            // Act
            var result = await controller.ChangeUserStatusAsync(It.IsAny<Guid>(), It.IsAny<UserStatus>());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            result.As<NotFoundObjectResult>().Value.Should().Be(Resources.UsersController_NotFound_ChangeUserStatusAsync);

            _logger.VerifyNoOtherCalls();
            _service.Verify();
        }

        [TestMethod]
        public async Task ChangeUserStatusAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            _service.Setup(service => service.UserExistsAsync(It.IsAny<Guid>())).ReturnsAsync(true).Verifiable();

            _service.Setup(service => service.ChangeStatusAsync(It.IsAny<Guid>(), It.IsAny<UserStatus>())).ThrowsAsync(new Exception()).Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new UsersController(_logger.Object, _queries.Object, _service.Object);

            // Act
            var result = await controller.ChangeUserStatusAsync(It.IsAny<Guid>(), It.IsAny<UserStatus>());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be(Resources.UsersController_BadRequest_ChangeUserStatusAsync);

            _logger.Verify();
            _service.Verify();
        }
    }
}