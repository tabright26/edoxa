// Filename: AccountWithdrawalControllerTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Accounts.Controllers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.TestHelpers;
using eDoxa.Cashier.TestHelpers.Fixtures;
using eDoxa.Cashier.TestHelpers.Mocks;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Areas.Accounts.Controllers
{
    public sealed class AccountWithdrawalControllerTest : UnitTest
    {
        public AccountWithdrawalControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        //[Fact]
        //public async Task PostAsync_ShouldBeOfTypeBadRequestObjectResult()
        //{
        //    // Arrange
        //    var mockAccountService = new Mock<IAccountService>();

        //    mockAccountService.Setup(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>())).Verifiable();

        //    mockAccountService.Setup(
        //            accountService => accountService.WithdrawalAsync(
        //                It.IsAny<IAccount>(),
        //                It.IsAny<Money>(),
        //                It.IsAny<string>(),
        //                It.IsAny<CancellationToken>()))
        //        .Verifiable();

        //    var controller = new AccountWithdrawalController(mockAccountService.Object);

        //    var mockHttpContextAccessor = new MockHttpContextAccessor();

        //    controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

        //    controller.ControllerContext.ModelState.AddModelError("error", "error");

        //    // Act
        //    var result = await controller.PostAsync(Currency.Money, Money.Fifty);

        //    // Assert
        //    result.Should().BeOfType<BadRequestObjectResult>();

        //    mockAccountService.Verify(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>()), Times.Never);

        //    mockHttpContextAccessor.VerifyGet(Times.Never());

        //    mockAccountService.Verify(
        //        accountService => accountService.WithdrawalAsync(
        //            It.IsAny<IAccount>(),
        //            It.IsAny<Money>(),
        //            It.IsAny<string>(),
        //            It.IsAny<CancellationToken>()),
        //        Times.Never);
        //}

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockAccountService = new Mock<IAccountService>();

            mockAccountService.Setup(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>())).Verifiable();

            mockAccountService.Setup(
                    accountService => accountService.WithdrawalAsync(
                        It.IsAny<IAccount>(),
                        It.IsAny<Money>(),
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var mockBundlesService = new Mock<IBundlesService>();

            var controller = new AccountWithdrawalController(mockAccountService.Object, mockBundlesService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await controller.PostAsync(Currency.Money, Money.Fifty);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockAccountService.Verify(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>()), Times.Once);

            mockHttpContextAccessor.VerifyGet(Times.Exactly(2));

            mockAccountService.Verify(
                accountService => accountService.WithdrawalAsync(
                    It.IsAny<IAccount>(),
                    It.IsAny<Money>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockAccountService = new Mock<IAccountService>();

            mockAccountService.Setup(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>()))
                .ReturnsAsync(new Account(new UserId()))
                .Verifiable();

            mockAccountService.Setup(
                    accountService => accountService.WithdrawalAsync(
                        It.IsAny<IAccount>(),
                        It.IsAny<Money>(),
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var mockBundlesService = new Mock<IBundlesService>();

            var controller = new AccountWithdrawalController(mockAccountService.Object, mockBundlesService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await controller.PostAsync(Currency.Money, Money.Fifty);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockAccountService.Verify(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>()), Times.Once);

            mockHttpContextAccessor.VerifyGet(Times.Exactly(2));

            mockAccountService.Verify(
                accountService => accountService.WithdrawalAsync(
                    It.IsAny<IAccount>(),
                    It.IsAny<Money>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
