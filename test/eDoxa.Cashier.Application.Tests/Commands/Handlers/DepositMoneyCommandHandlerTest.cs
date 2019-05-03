// Filename: AddFundsCommandHandlerTest.cs
// Date Created: 2019-04-14
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Application.Commands.Handlers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Functional.Maybe;
using eDoxa.Security;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class DepositMoneyCommandHandlerTest
    {
        [TestMethod]
        public async Task Handle_FindAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var command = new DepositMoneyCommand(MoneyBundleType.Ten);

            var mockUserInfoService = new Mock<IUserInfoService>();

            mockUserInfoService.SetupGet(userInfoService => userInfoService.Subject).Returns(new Option<Guid>(Guid.NewGuid()));

            mockUserInfoService.SetupGet(userInfoService => userInfoService.CustomerId).Returns(new Option<string>("cus_123qweqwe"));

            var mockAccountService = new Mock<IMoneyAccountService>();

            mockAccountService.Setup(service => service.TransactionAsync(It.IsAny<UserId>(), It.IsAny<CustomerId>(), It.IsAny<MoneyBundle>(), It.IsAny<CancellationToken>()))
                              .ReturnsAsync(new MoneyTransaction(new Money(10)))
                              .Verifiable();

            var handler = new DepositMoneyCommandHandler(mockUserInfoService.Object, mockAccountService.Object);

            // Act
            await handler.Handle(command, default);

            // Assert
            mockAccountService.Verify(service => service.TransactionAsync(It.IsAny<UserId>(), It.IsAny<CustomerId>(), It.IsAny<MoneyBundle>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}