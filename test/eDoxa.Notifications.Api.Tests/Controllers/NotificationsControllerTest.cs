// Filename: NotificationsControllerTest.cs
// Date Created: 2019-03-26
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Notifications.Api.Controllers;
using eDoxa.Notifications.Application.Commands;
using eDoxa.Notifications.Domain.AggregateModels;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Notifications.Api.Tests.Controllers
{
    [TestClass]
    public class NotificationsControllerTest
    {
        private Mock<ILogger<NotificationsController>> _logger;
        private Mock<IMediator> _mediator;

        [TestInitialize]
        public void TestInitialize()
        {
            _logger = new Mock<ILogger<NotificationsController>>();
            _mediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task ReadNotificationAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            _mediator.Setup(mediator => mediator.Send(It.IsAny<ReadNotificationCommand>(), default))
                     .ReturnsAsync(It.IsAny<Unit>())
                     .Verifiable();

            var controller = new NotificationsController(_logger.Object, _mediator.Object);

            // Act
            var result = await controller.ReadNotificationAsync(new NotificationId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _logger.VerifyNoOtherCalls();
            _mediator.Verify();
        }

        [TestMethod]
        public async Task ReadNotificationAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            _mediator.Setup(mediator => mediator.Send(It.IsAny<ReadNotificationCommand>(), default))
                     .ThrowsAsync(new Exception())
                     .Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new NotificationsController(_logger.Object, _mediator.Object);

            // Act
            var result = await controller.ReadNotificationAsync(new NotificationId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            _logger.Verify();
            _mediator.Verify();
        }

        [TestMethod]
        public async Task DeleteNotificationAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            _mediator.Setup(mediator => mediator.Send(It.IsAny<DeleteNotificationCommand>(), default))
                     .ReturnsAsync(It.IsAny<Unit>)
                     .Verifiable();

            var controller = new NotificationsController(_logger.Object, _mediator.Object);

            // Act
            var result = await controller.DeleteNotificationAsync(new NotificationId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _logger.VerifyNoOtherCalls();
            _mediator.Verify();
        }

        [TestMethod]
        public async Task DeleteNotificationAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            _mediator.Setup(mediator => mediator.Send(It.IsAny<DeleteNotificationCommand>(), default))
                     .ThrowsAsync(new Exception())
                     .Verifiable();

            _logger.SetupLoggerWithLogLevelErrorVerifiable();

            var controller = new NotificationsController(_logger.Object, _mediator.Object);

            // Act
            var result = await controller.DeleteNotificationAsync(new NotificationId());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            _logger.Verify();
            _mediator.Verify();
        }
    }
}