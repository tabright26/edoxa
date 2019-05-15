﻿// Filename: WithdrawalFundsCommandHandlerTest.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Application.Commands.Handlers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Commands.Extensions;
using eDoxa.Security.Abstractions;
using eDoxa.Testing.MSTest;
using eDoxa.Testing.MSTest.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class WithdrawalFundsCommandHandlerTest
    {
        private Mock<IMoneyAccountService> _mockMoneyAccountService;
        private Mock<IUserInfoService> _mockUserInfoService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMoneyAccountService = new Mock<IMoneyAccountService>();
            _mockUserInfoService = new Mock<IUserInfoService>();
            _mockUserInfoService.SetupGetProperties();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<WithdrawalFundsCommandHandler>.For(typeof(IUserInfoService), typeof(IMoneyAccountService))
                .WithName("WithdrawalFundsCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_WithdrawalFundsCommand_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var command = new WithdrawalFundsCommand(WithdrawalMoneyBundleType.Fifty);

            _mockMoneyAccountService.Setup(mock => mock.TryWithdrawalAsync(It.IsAny<StripeAccountId>(), It.IsAny<UserId>(), It.IsAny<MoneyBundle>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new WithdrawalMoneyTransaction(Money.Fifty))
                .Verifiable();

            var handler = new WithdrawalFundsCommandHandler(_mockUserInfoService.Object, _mockMoneyAccountService.Object);

            // Act
            await handler.HandleAsync(command);

            // Assert
            _mockMoneyAccountService.Verify(mock => mock.TryWithdrawalAsync(It.IsAny<StripeAccountId>(), It.IsAny<UserId>(), It.IsAny<MoneyBundle>(), It.IsAny<CancellationToken>()),Times.Once); 
        }
    }
}