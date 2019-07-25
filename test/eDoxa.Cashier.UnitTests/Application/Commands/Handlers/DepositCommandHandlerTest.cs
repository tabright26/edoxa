// Filename: DepositCommandHandlerTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Commands;
using eDoxa.Cashier.Api.Application.Commands.Handlers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.UnitTests.Helpers.Mocks;
using eDoxa.Seedwork.Application.Commands.Extensions;
using eDoxa.Seedwork.Testing.TestConstructor;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Application.Commands.Handlers
{
    [TestClass]
    public sealed class DepositCommandHandlerTest
    {
        private MockHttpContextAccessor _mockHttpContextAccessor;
        private Mock<IAccountService> _mockMoneyAccountService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockHttpContextAccessor = new MockHttpContextAccessor();
            _mockMoneyAccountService = new Mock<IAccountService>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<DepositCommandHandler>.ForParameters(typeof(IHttpContextAccessor), typeof(IAccountService))
                .WithClassName("DepositCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_DepositMoneyCommand_ShouldBeOfTypeEither()
        {
            // Arrange
            var command = new DepositCommand(Currency.Money.Name, Money.Fifty);

            _mockMoneyAccountService
                .Setup(
                    accountService => accountService.DepositAsync(It.IsAny<string>(), It.IsAny<UserId>(), It.IsAny<ICurrency>(), It.IsAny<CancellationToken>())
                )
                .Returns(Unit.Task)
                .Verifiable();

            var handler = new DepositCommandHandler(_mockHttpContextAccessor.Object, _mockMoneyAccountService.Object);

            // Act
            await handler.HandleAsync(command);

            // Assert
            _mockMoneyAccountService.Verify(
                accountService => accountService.DepositAsync(It.IsAny<string>(), It.IsAny<UserId>(), It.IsAny<ICurrency>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}
