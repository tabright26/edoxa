// Filename: WithdrawalMoneyCommandHandlerTest.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.Abstractions;
using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Application.Commands.Handlers;
using eDoxa.Cashier.Domain;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Cashier.Tests.Extensions;
using eDoxa.Commands.Extensions;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class WithdrawMoneyCommandHandlerTest
    {
        private Mock<IMoneyAccountService> _mockMoneyAccountService;
        private Mock<ICashierSecurity> _mockCashierSecurity;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMoneyAccountService = new Mock<IMoneyAccountService>();
            _mockCashierSecurity = new Mock<ICashierSecurity>();
            _mockCashierSecurity.SetupGetProperties();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<WithdrawMoneyCommandHandler>.For(typeof(ICashierSecurity), typeof(IMoneyAccountService))
                .WithName("WithdrawMoneyCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_WithdrawalFundsCommand_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var command = new WithdrawMoneyCommand(MoneyWithdrawalBundleType.Fifty);

            _mockMoneyAccountService.Setup(mock =>
                    mock.TryWithdrawalAsync(It.IsAny<StripeAccountId>(), It.IsAny<UserId>(), It.IsAny<MoneyBundle>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(TransactionStatus.Succeeded)
                .Verifiable();

            var handler = new WithdrawMoneyCommandHandler(_mockCashierSecurity.Object, _mockMoneyAccountService.Object);

            // Act
            await handler.HandleAsync(command);

            // Assert
            _mockMoneyAccountService.Verify(
                mock => mock.TryWithdrawalAsync(It.IsAny<StripeAccountId>(), It.IsAny<UserId>(), It.IsAny<MoneyBundle>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}