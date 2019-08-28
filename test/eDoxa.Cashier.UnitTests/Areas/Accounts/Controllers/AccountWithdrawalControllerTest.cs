// Filename: AccountWithdrawalControllerTest.cs
// Date Created: 2019-08-27
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

using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Areas.Accounts.Controllers
{
    [TestClass]
    public sealed class AccountWithdrawalControllerTest
    {
        [TestMethod]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockAccountService = new Mock<IAccountService>();

            mockAccountService.Setup(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>()))
                .ReturnsAsync(new Account(new UserId()))
                .Verifiable();

            mockAccountService.Setup(
                    accountService => accountService.WithdrawalAsync(
                        It.IsAny<IMoneyAccount>(),
                        It.IsAny<Money>(),
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var controller = new AccountWithdrawalController(mockAccountService.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            var request = new AccountWithdrawalPostRequest(Money.Fifty);

            // Act
            var result = await controller.PostAsync(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockAccountService.Verify(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>()), Times.Once);

            mockAccountService.Verify(
                accountService => accountService.WithdrawalAsync(
                    It.IsAny<IMoneyAccount>(),
                    It.IsAny<Money>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            mockHttpContextAccessor.Verify();
        }

        [TestMethod]
        public async Task PostAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockAccountService = new Mock<IAccountService>();

            mockAccountService.Setup(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>())).Verifiable();

            mockAccountService.Setup(
                    accountService => accountService.WithdrawalAsync(
                        It.IsAny<IMoneyAccount>(),
                        It.IsAny<Money>(),
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var controller = new AccountWithdrawalController(mockAccountService.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            var request = new AccountWithdrawalPostRequest(Money.Fifty);

            // Act
            var result = await controller.PostAsync(request);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockAccountService.Verify(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>()), Times.Once);

            mockAccountService.Verify(
                accountService => accountService.WithdrawalAsync(
                    It.IsAny<IMoneyAccount>(),
                    It.IsAny<Money>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);

            mockHttpContextAccessor.Verify();
        }
    }
}
