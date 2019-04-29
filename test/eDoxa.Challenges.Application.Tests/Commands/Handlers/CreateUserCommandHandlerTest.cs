// Filename: CreateUserCommandHandlerTest.cs
// Date Created: 2019-03-22
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Application.Commands;
using eDoxa.Challenges.Application.Commands.Handlers;
using eDoxa.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.Challenges.Domain.Repositories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Challenges.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class CreateUserCommandHandlerTest
    {
        [TestMethod]
        public async Task HandleAsync_Create_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(repository => repository.Create(It.IsAny<User>())).Verifiable();

            mockUserRepository.Setup(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()))
                              .Returns(Task.CompletedTask)
                              .Verifiable();

            // Act
            var handler = new CreateUserCommandHandler(mockUserRepository.Object);

            // Assert
            await handler.HandleAsync(new CreateUserCommand(new UserId()));

            mockUserRepository.Verify(repository => repository.Create(It.IsAny<User>()), Times.Once);

            mockUserRepository.Verify(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}