// Filename: AddFundsCommandHandlerTest.cs
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

using AutoMapper;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Application.Commands.Handlers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.Services;
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
    public sealed class AddFundsCommandHandlerTest
    {
        private Mock<IMapper> _mockMapper;
        private Mock<IMoneyAccountService> _mockMoneyAccountService;
        private Mock<IUserInfoService> _mockUserInfoService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMapper = new Mock<IMapper>();
            _mockMoneyAccountService = new Mock<IMoneyAccountService>();
            _mockUserInfoService = new Mock<IUserInfoService>();
            _mockUserInfoService.SetupGetProperties();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<AddFundsCommandHandler>.For(typeof(IUserInfoService), typeof(IMoneyAccountService), typeof(IMapper))
                .WithName("AddFundsCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_AddFundsCommand_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var command = new AddFundsCommand(MoneyBundleType.Ten);

            _mockMoneyAccountService.Setup(mock =>
                    mock.DepositAsync(It.IsAny<UserId>(), It.IsAny<CustomerId>(), It.IsAny<MoneyBundle>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DepositMoneyTransaction(new Money(10)))
                .Verifiable();

            var handler = new AddFundsCommandHandler(_mockUserInfoService.Object, _mockMoneyAccountService.Object, _mockMapper.Object);

            // Act
            await handler.HandleAsync(command);

            // Assert
            _mockMoneyAccountService.Verify(
                mock => mock.DepositAsync(It.IsAny<UserId>(), It.IsAny<CustomerId>(), It.IsAny<MoneyBundle>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}