// Filename: UserNotificationsControllerTest.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Notifications.Api.Controllers;
using eDoxa.Notifications.Domain.AggregateModels;
using eDoxa.Notifications.DTO;
using eDoxa.Notifications.DTO.Queries;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Notifications.Api.Tests.Controllers
{
    [TestClass]
    public class UserNotificationsControllerTest
    {
        private Mock<ILogger<UserNotificationsController>> _logger;
        private Mock<INotificationQueries> _queries;

        [TestInitialize]
        public void TestInitialize()
        {
            _logger = new Mock<ILogger<UserNotificationsController>>();
            _queries = new Mock<INotificationQueries>();
        }

        [TestMethod]
        public async Task FindUserNotificationsAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindUserNotificationsAsync(It.IsAny<UserId>()))
                    .ReturnsAsync(
                        new NotificationListDTO
                        {
                            Items = new List<NotificationDTO>
                            {
                                new NotificationDTO()
                            }
                        }
                    )
                    .Verifiable();

            var controller = new UserNotificationsController(_logger.Object, _queries.Object);

            // Act
            var result = await controller.FindUserNotificationsAsync(It.IsAny<UserId>());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _logger.VerifyNoOtherCalls();
            _queries.Verify();
        }

        [TestMethod]
        public async Task FindUserNotificationsAsync_ShouldBeNoContentResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindUserNotificationsAsync(It.IsAny<UserId>())).ReturnsAsync(new NotificationListDTO()).Verifiable();

            var controller = new UserNotificationsController(_logger.Object, _queries.Object);

            // Act
            var result = await controller.FindUserNotificationsAsync(It.IsAny<UserId>());

            // Assert
            result.Should().BeOfType<NoContentResult>();
            _logger.VerifyNoOtherCalls();
            _queries.Verify();
        }

        [TestMethod]
        public async Task FindUserNotificationsAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindUserNotificationsAsync(It.IsAny<UserId>())).ThrowsAsync(new Exception()).Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new UserNotificationsController(_logger.Object, _queries.Object);

            // Act
            var result = await controller.FindUserNotificationsAsync(It.IsAny<UserId>());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            _logger.Verify();
            _queries.Verify();
        }

        [TestMethod]
        public async Task FindUserNotificationAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindUserNotificationAsync(It.IsAny<UserId>(), It.IsAny<NotificationId>()))
                    .ReturnsAsync(new NotificationDTO())
                    .Verifiable();

            var controller = new UserNotificationsController(_logger.Object, _queries.Object);

            // Act
            var result = await controller.FindUserNotificationAsync(It.IsAny<UserId>(), It.IsAny<NotificationId>());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _logger.VerifyNoOtherCalls();
            _queries.Verify();
        }

        [TestMethod]
        public async Task FindUserNotificationAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindUserNotificationAsync(It.IsAny<UserId>(), It.IsAny<NotificationId>()))
                    .ReturnsAsync((NotificationDTO) null)
                    .Verifiable();

            var controller = new UserNotificationsController(_logger.Object, _queries.Object);

            // Act
            var result = await controller.FindUserNotificationAsync(It.IsAny<UserId>(), It.IsAny<NotificationId>());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            _logger.VerifyNoOtherCalls();
            _queries.Verify();
        }

        [TestMethod]
        public async Task FindUserNotificationAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindUserNotificationAsync(It.IsAny<UserId>(), It.IsAny<NotificationId>()))
                    .ThrowsAsync(new Exception())
                    .Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new UserNotificationsController(_logger.Object, _queries.Object);

            // Act
            var result = await controller.FindUserNotificationAsync(It.IsAny<UserId>(), It.IsAny<NotificationId>());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            _logger.Verify();
            _queries.Verify();
        }
    }
}