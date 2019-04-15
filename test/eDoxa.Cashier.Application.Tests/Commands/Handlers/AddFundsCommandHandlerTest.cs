// Filename: AddFundsCommandHandlerTest.cs
// Date Created: 2019-04-14
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Application.Commands.Handlers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class AddFundsCommandHandlerTest
    {
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        [TestMethod]
        public async Task Handle_FindAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var user = _userAggregateFactory.CreateUser();

            var command = new AddFundsCommand(MoneyBundleType.Ten);

            var mockAccountService = new Mock<IAccountService>();

            mockAccountService.Setup(service => service.TransactionAsync(It.IsAny<CustomerId>(), It.IsAny<MoneyBundle>(), It.IsAny<CancellationToken>()))
                              .Returns(Task.CompletedTask)
                              .Verifiable();

            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(repository => repository.FindAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

            mockUserRepository.Setup(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()))
                              .Returns(Task.CompletedTask)
                              .Verifiable();

            var handler = new AddFundsCommandHandler(mockUserRepository.Object, mockAccountService.Object);

            // Act
            await handler.Handle(command, default);

            // Assert
            mockAccountService.Verify(
                service => service.TransactionAsync(It.IsAny<CustomerId>(), It.IsAny<MoneyBundle>(), It.IsAny<CancellationToken>()),
                Times.Once
            );

            mockUserRepository.Verify(repository => repository.FindAsync(It.IsAny<UserId>()), Times.Once);

            mockUserRepository.Verify(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}