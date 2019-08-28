// Filename: AccountDepositControllerTest.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Accounts.Controllers;
using eDoxa.Cashier.Api.Areas.Accounts.Requests;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.UnitTests.Helpers.Mocks;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Areas.Account.Controllers
{
    [TestClass]
    public sealed class AccountDepositControllerTest
    {
        [TestMethod]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var deposit = new AccountDepositPostRequest(Currency.Token.Name, Token.FiftyThousand);

            var mockAccountService = new Mock<IAccountService>();

            mockAccountService.Setup(accountService => accountService.DepositAsync(It.IsAny<string>(), It.IsAny<UserId>(),
                It.IsAny<Token>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask).Verifiable();

            var controller = new AccountDepositController(mockAccountService.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await controller.PostAsync(deposit);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockAccountService.Verify(accountService => accountService.DepositAsync(It.IsAny<string>(), It.IsAny<UserId>(),
                It.IsAny<Token>(), It.IsAny<CancellationToken>()), Times.Once);

            mockHttpContextAccessor.Verify();
        }
    }
}
