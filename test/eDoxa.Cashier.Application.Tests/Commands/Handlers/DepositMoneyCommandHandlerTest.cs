// Filename: DepositMoneyCommandHandlerTest.cs
// Date Created: 2019-05-03
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
using eDoxa.Cashier.Domain.Services;
using eDoxa.Commands.Extensions;
using eDoxa.Security.Abstractions;
using eDoxa.Testing.MSTest.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class DepositMoneyCommandHandlerTest
    {
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
            var command = new DepositMoneyCommand(MoneyBundleType.Ten);

            _mockMoneyAccountService.Setup(service =>
                    service.TransactionAsync(It.IsAny<UserId>(), It.IsAny<CustomerId>(), It.IsAny<MoneyBundle>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new MoneyTransaction(new Money(10)))
                .Verifiable();

            var handler = new DepositMoneyCommandHandler(_mockUserProfile.Object, _mockMoneyAccountService.Object);

            // Act
            await handler.HandleAsync(command);

            // Assert
            _mockMoneyAccountService.Verify(
                service => service.TransactionAsync(It.IsAny<UserId>(), It.IsAny<CustomerId>(), It.IsAny<MoneyBundle>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}