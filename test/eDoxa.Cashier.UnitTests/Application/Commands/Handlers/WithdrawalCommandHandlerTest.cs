// Filename: WithdrawCommandHandlerTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Commands;
using eDoxa.Cashier.Api.Application.Commands.Handlers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.UnitTests.Helpers.Mocks;
using eDoxa.Commands.Extensions;
using eDoxa.Seedwork.Testing.TestConstructor;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Application.Commands.Handlers
{
    [TestClass]
    public sealed class WithdrawalCommandHandlerTest
    {
        private MockHttpContextAccessor _mockHttpContextAccessor;
        private Mock<IAccountService> _mockAccountService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockHttpContextAccessor = new MockHttpContextAccessor();
            _mockAccountService = new Mock<IAccountService>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<WithdrawalCommandHandler>.ForParameters(typeof(IHttpContextAccessor), typeof(IAccountService))
                .WithClassName("WithdrawalCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_WithdrawMoneyCommand_ShouldBeOfTypeEither()
        {
            // Arrange
            var command = new WithdrawalCommand(Money.Fifty.Amount);
            _mockAccountService.Setup(accountService => accountService.WithdrawalAsync(It.IsAny<string>(), It.IsAny<UserId>(), It.IsAny<Money>(), It.IsAny<CancellationToken>())).Returns(Unit.Task).Verifiable();
            var handler = new WithdrawalCommandHandler(_mockHttpContextAccessor.Object, _mockAccountService.Object);

            // Act
            await handler.HandleAsync(command);

            // Assert
            _mockAccountService.Verify(accountService => accountService.WithdrawalAsync(It.IsAny<string>(), It.IsAny<UserId>(), It.IsAny<Money>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
