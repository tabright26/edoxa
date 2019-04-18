// Filename: ReadNotificationCommandHandlerTest.cs
// Date Created: 2019-03-26
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Notifications.Application.Commands;
using eDoxa.Notifications.Application.Commands.Handlers;
using eDoxa.Notifications.Domain.AggregateModels;
using eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate;
using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;
using eDoxa.Notifications.Domain.Repositories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Notifications.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class ReadNotificationCommandHandlerTest
    {
        [TestMethod]
        public async Task HandleAsync_FindAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var command = new ReadNotificationCommand(new NotificationId());

            var mockRepository = new Mock<INotificationRepository>();

            mockRepository.Setup(repository => repository.FindAsync(It.IsAny<NotificationId>()))
                          .ReturnsAsync(new Notification(User.Create(new UserId()), "Title", "Message", "RedirectUrl"))
                          .Verifiable();

            mockRepository.Setup(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()))
                          .Returns(Task.CompletedTask)
                          .Verifiable();

            // Act
            var handler = new ReadNotificationCommandHandler(mockRepository.Object);

            // Assert
            await handler.HandleAsync(command);

            mockRepository.Verify(repository => repository.FindAsync(It.IsAny<NotificationId>()), Times.Once);

            mockRepository.Verify(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}