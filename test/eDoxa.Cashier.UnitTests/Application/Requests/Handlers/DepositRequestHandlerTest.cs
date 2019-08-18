// Filename: DepositRequestHandlerTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Requests;
using eDoxa.Cashier.Api.Application.Requests.Handlers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.UnitTests.Helpers.Mocks;
using eDoxa.Seedwork.Application.Extensions;

using MediatR;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Application.Requests.Handlers
{
    [TestClass]
    public sealed class DepositRequestHandlerTest
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
        public async Task HandleAsync_DepositMoneyRequest_ShouldBeOfTypeEither()
        {
            // Arrange
            var request = new DepositRequest(Currency.Money.Name, Money.Fifty);

            _mockMoneyAccountService
                .Setup(
                    accountService => accountService.DepositAsync(It.IsAny<string>(), It.IsAny<UserId>(), It.IsAny<ICurrency>(), It.IsAny<CancellationToken>())
                )
                .Returns(Unit.Task)
                .Verifiable();

            var handler = new DepositRequestHandler(_mockHttpContextAccessor.Object, _mockMoneyAccountService.Object);

            // Act
            await handler.HandleAsync(request);

            // Assert
            _mockMoneyAccountService.Verify(
                accountService => accountService.DepositAsync(It.IsAny<string>(), It.IsAny<UserId>(), It.IsAny<ICurrency>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}
