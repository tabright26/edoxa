// Filename: CreateUserCommandHandlerTest.cs
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
using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;
using eDoxa.Notifications.Domain.Repositories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Notifications.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class CreateUserCommandHandlerTest
    {
        [TestMethod]
        public async Task HandleAsync_Create_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var command = new CreateUserCommand(new UserId());
            var mockRepository = new Mock<IUserRepository>();

            mockRepository.Setup(repository => repository.Create(It.IsAny<User>())).Verifiable();

            mockRepository.Setup(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()))
                              .Returns(Task.CompletedTask)
                              .Verifiable();

            // Act
            var handler = new CreateUserCommandHandler(mockRepository.Object);

            // Assert
            await handler.HandleAsync(command, default(CancellationToken));

            mockRepository.Verify(repository => repository.Create(It.IsAny<User>()), Times.Once);

            mockRepository.Verify(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}