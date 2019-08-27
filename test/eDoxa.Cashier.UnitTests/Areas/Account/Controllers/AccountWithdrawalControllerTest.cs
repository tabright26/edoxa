// Filename: AccountWithdrawalControllerTest.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Requests;
using eDoxa.Cashier.Api.Areas.Accounts.Controllers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.UnitTests.Helpers.Mocks;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Areas.Account.Controllers
{
    [TestClass]
    public sealed class AccountWithdrawalControllerTest
    {

        [TestMethod]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var withdrawal = new WithdrawalRequest(Money.Fifty);

            var mockAccountService = new Mock<IAccountService>();

            mockAccountService.Setup(accountService => accountService.WithdrawalAsync(It.IsAny<string>(), It.IsAny<UserId>(),
                It.IsAny<Money>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask).Verifiable();

            var controller = new AccountWithdrawalController(mockAccountService.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await controller.PostAsync(withdrawal);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockAccountService.Verify(accountService => accountService.WithdrawalAsync(It.IsAny<string>(), It.IsAny<UserId>(),
                It.IsAny<Money>(), It.IsAny<CancellationToken>()), Times.Once);

            mockHttpContextAccessor.Verify();
        }
    }
}
