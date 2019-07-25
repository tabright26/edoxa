// Filename: WithdrawalRequestHandlerTest.cs
// Date Created: 2019-07-05
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
using eDoxa.Seedwork.Testing.TestConstructor;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Application.Requests.Handlers
{
    [TestClass]
    public sealed class WithdrawalRequestHandlerTest
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
            TestConstructor<WithdrawalHandler>.ForParameters(typeof(IHttpContextAccessor), typeof(IAccountService))
                .WithClassName("WithdrawalRequestHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_WithdrawMoneyRequest_ShouldBeOfTypeEither()
        {
            // Arrange
            var request = new WithdrawalRequest(Money.Fifty.Amount);

            _mockAccountService
                .Setup(
                    accountService => accountService.WithdrawalAsync(It.IsAny<string>(), It.IsAny<UserId>(), It.IsAny<Money>(), It.IsAny<CancellationToken>())
                )
                .Returns(Unit.Task)
                .Verifiable();

            var handler = new WithdrawalHandler(_mockHttpContextAccessor.Object, _mockAccountService.Object);

            // Act
            await handler.HandleAsync(request);

            // Assert
            _mockAccountService.Verify(
                accountService => accountService.WithdrawalAsync(It.IsAny<string>(), It.IsAny<UserId>(), It.IsAny<Money>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}
