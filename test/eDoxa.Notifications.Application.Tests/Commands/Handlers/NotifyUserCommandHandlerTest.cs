// Filename: NotifyUserCommandHandlerTest.cs
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
using eDoxa.Notifications.Domain.Factories;
using eDoxa.Notifications.Domain.Repositories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Notifications.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class NotifyUserCommandHandlerTest
    {
        private readonly NotificationAggregateFactory _factory = NotificationAggregateFactory.Instance;

        [TestMethod]
        public async Task HandleAsync_FindAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var command = new NotifyUserCommand(new UserId(), NotificationNames.ChallengeParticipantRegistered, _factory.CreateMetadata(new[] { "value1", "value2" }));
            var mockRepository = new Mock<IUserRepository>();

            mockRepository.Setup(repository => repository.FindAsync(It.IsAny<UserId>())).ReturnsAsync(User.Create(new UserId())).Verifiable();

            mockRepository.Setup(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()))
                          .Returns(Task.CompletedTask)
                          .Verifiable();

            // Act
            var handler = new NotifyUserCommandHandler(mockRepository.Object);

            // Assert
            await handler.HandleAsync(command, default(CancellationToken));

            mockRepository.Verify(repository => repository.FindAsync(It.IsAny<UserId>()), Times.Once);

            mockRepository.Verify(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}