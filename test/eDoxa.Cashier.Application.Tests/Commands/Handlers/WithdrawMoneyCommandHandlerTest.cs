// Filename: WithdrawalCommandHandlerTest.cs
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
using eDoxa.Cashier.Domain;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Commands.Extensions;
using eDoxa.Functional.Maybe;
using eDoxa.Security.Abstractions;
using eDoxa.Testing.MSTest.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class WithdrawMoneyCommandHandlerTest
    {
        private static readonly UserAggregateFactory UserAggregateFactory = UserAggregateFactory.Instance;
        private Mock<IMoneyAccountService> _mockMoneyAccountService;
        private Mock<IUserProfile> _mockUserProfile;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMoneyAccountService = new Mock<IMoneyAccountService>();
            _mockUserProfile = new Mock<IUserProfile>();
            _mockUserProfile.SetupGetProperties();
        }

        [TestMethod]
        public async Task Handle_FindAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var money = UserAggregateFactory.CreateMoney();

            var command = new WithdrawMoneyCommand(money);

            _mockMoneyAccountService.Setup(x => x.TryWithdrawAsync(It.IsAny<UserId>(), It.IsAny<decimal>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Option<IMoneyTransaction>(new MoneyTransaction(-new Money(100))))
                .Verifiable();

            var handler = new WithdrawMoneyCommandHandler(_mockUserProfile.Object, _mockMoneyAccountService.Object);

            // Act
            await handler.HandleAsync(command);

            // Assert
            _mockMoneyAccountService.Verify();
        }
    }
}