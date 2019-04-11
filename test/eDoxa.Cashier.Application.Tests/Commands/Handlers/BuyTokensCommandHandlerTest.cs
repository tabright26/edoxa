﻿// Filename: BuyTokensCommandHandlerTest.cs
// Date Created: 2019-04-09
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
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Domain.Repositories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class BuyTokensCommandHandlerTest
    {
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        [TestMethod]
        public async Task Handle_FindAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var token = _userAggregateFactory.CreateToken();

            var command = new BuyTokensCommand(token.ToDecimal());

            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(repository => repository.FindAsync(It.IsAny<UserId>())).ReturnsAsync(_userAggregateFactory.CreateUser()).Verifiable();

            mockUserRepository.Setup(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()))
                              .Returns(Task.CompletedTask)
                              .Verifiable();

            var handler = new BuyTokensCommandHandler(mockUserRepository.Object);

            // Act
            await handler.Handle(command, default(CancellationToken));

            // Assert
            mockUserRepository.Verify(repository => repository.FindAsync(It.IsAny<UserId>()), Times.Once);

            mockUserRepository.Verify(repository => repository.UnitOfWork.CommitAndDispatchDomainEventsAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}