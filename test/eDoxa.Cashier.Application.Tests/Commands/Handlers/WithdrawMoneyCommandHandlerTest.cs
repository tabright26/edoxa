// Filename: WithdrawMoneyCommandHandlerTest.cs
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

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Application.Commands.Handlers;
using eDoxa.Cashier.Domain;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.Services.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Cashier.Security.Abstractions;
using eDoxa.Cashier.Tests.Extensions;
using eDoxa.Commands.Extensions;
using eDoxa.Functional;
using eDoxa.Testing.MSTest;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class WithdrawMoneyCommandHandlerTest
    {
        private Mock<ICashierHttpContext> _mockCashierHttpContext;
        private Mock<IMoneyAccountService> _mockMoneyAccountService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMoneyAccountService = new Mock<IMoneyAccountService>();
            _mockCashierHttpContext = new Mock<ICashierHttpContext>();
            _mockCashierHttpContext.SetupGetProperties();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<WithdrawMoneyCommandHandler>.For(typeof(ICashierHttpContext), typeof(IMoneyAccountService))
                .WithName("WithdrawMoneyCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_WithdrawMoneyCommand_ShouldBeOfTypeEither()
        {
            // Arrange
            var command = new WithdrawMoneyCommand(MoneyWithdrawalBundleType.Fifty);

            _mockMoneyAccountService.Setup(mock =>
                    mock.TryWithdrawalAsync(It.IsAny<StripeAccountId>(), It.IsAny<UserId>(), It.IsAny<MoneyBundle>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(TransactionStatus.Succeeded)
                .Verifiable();

            var handler = new WithdrawMoneyCommandHandler(_mockCashierHttpContext.Object, _mockMoneyAccountService.Object);

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            result.Should().BeOfType<Either<TransactionStatus>>();

            _mockMoneyAccountService.Verify(
                mock => mock.TryWithdrawalAsync(It.IsAny<StripeAccountId>(), It.IsAny<UserId>(), It.IsAny<MoneyBundle>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}