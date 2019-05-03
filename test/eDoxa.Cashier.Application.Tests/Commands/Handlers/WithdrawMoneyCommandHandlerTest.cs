// Filename: WithdrawalCommandHandlerTest.cs
// Date Created: 2019-04-09
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
using eDoxa.Cashier.Domain;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Functional.Maybe;
using eDoxa.Security;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class WithdrawMoneyCommandHandlerTest
    {
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        [TestMethod]
        public async Task Handle_FindAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var money = _userAggregateFactory.CreateMoney();

            var command = new WithdrawMoneyCommand(money);

            var mockMoneyAccount = new Mock<IMoneyAccountService>();

            mockMoneyAccount.Setup(x => x.TryWithdrawAsync(It.IsAny<UserId>(), It.IsAny<decimal>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Option<IMoneyTransaction>(new MoneyTransaction(-new Money(100))))
                .Verifiable();

            var mockUserInfoService = new Mock<IUserInfoService>();

            mockUserInfoService.SetupGet(userInfoService => userInfoService.Subject).Returns(new Option<Guid>(Guid.NewGuid()));

            var handler = new WithdrawMoneyCommandHandler(mockUserInfoService.Object, mockMoneyAccount.Object);

            // Act
            await handler.Handle(command, default);

            // Assert
            mockMoneyAccount.Verify();
        }
    }
}