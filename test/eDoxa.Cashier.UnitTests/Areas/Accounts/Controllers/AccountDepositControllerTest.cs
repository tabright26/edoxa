// Filename: AccountDepositControllerTest.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Accounts.Controllers;
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
    public sealed class AccountDepositControllerTest
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
                    accountService => accountService.DepositAsync(
                        It.IsAny<IAccount>(),
                        It.IsAny<Token>(),
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var controller = new AccountDepositController(mockAccountService.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await controller.PostAsync(Currency.Token, Token.FiftyThousand);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockAccountService.Verify(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>()), Times.Once);

            mockHttpContextAccessor.VerifyGet(Times.Exactly(2));

            mockAccountService.Verify(
                accountService => accountService.DepositAsync(
                    It.IsAny<IAccount>(),
                    It.IsAny<Token>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [TestMethod]
        public async Task PostAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockAccountService = new Mock<IAccountService>();

            mockAccountService.Setup(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>())).Verifiable();

            mockAccountService.Setup(
                    accountService => accountService.DepositAsync(
                        It.IsAny<IAccount>(),
                        It.IsAny<Token>(),
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var controller = new AccountDepositController(mockAccountService.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await controller.PostAsync(Currency.Token, Token.FiftyThousand);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockAccountService.Verify(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>()), Times.Once);

            mockHttpContextAccessor.VerifyGet(Times.Exactly(2));

            mockAccountService.Verify(
                accountService => accountService.DepositAsync(
                    It.IsAny<IAccount>(),
                    It.IsAny<Token>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [TestMethod]
        public async Task PostAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockAccountService = new Mock<IAccountService>();

            mockAccountService.Setup(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>())).Verifiable();

            mockAccountService.Setup(
                    accountService => accountService.DepositAsync(
                        It.IsAny<IAccount>(),
                        It.IsAny<Token>(),
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()))
                .Verifiable();

            var controller = new AccountDepositController(mockAccountService.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            controller.ControllerContext.ModelState.AddModelError("error", "error");

            // Act
            var result = await controller.PostAsync(Currency.Token, Token.FiftyThousand);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockAccountService.Verify(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>()), Times.Never);

            mockHttpContextAccessor.VerifyGet(Times.Never());

            mockAccountService.Verify(
                accountService => accountService.DepositAsync(
                    It.IsAny<IAccount>(),
                    It.IsAny<Token>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}
