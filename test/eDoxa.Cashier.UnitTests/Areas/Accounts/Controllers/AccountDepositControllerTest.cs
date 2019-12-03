// Filename: AccountDepositControllerTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Collections.Immutable;

using eDoxa.Cashier.Api.Areas.Accounts.Controllers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Cashier.TestHelper.Mocks;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Areas.Accounts.Controllers
{
    public sealed class AccountDepositControllerTest : UnitTest
    {
        public AccountDepositControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void GetAsync_WithCurrencyAll_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockBundlesService = new Mock<IBundleService>();

            var controller = new AccountDepositController(mockBundlesService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = controller.Get(Currency.All);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void GetAsync_WithCurrencyMoney_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockBundlesService = new Mock<IBundleService>();

            var bundle = new List<Bundle>
            {
                new Bundle(new Token(50), new Price(new Money(20)))
            };

            mockBundlesService.Setup(accountService => accountService.FetchDepositMoneyBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();

            var controller = new AccountDepositController(mockBundlesService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = controller.Get(Currency.Money);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockBundlesService.Verify(accountService => accountService.FetchDepositMoneyBundles(), Times.Once);
        }

        [Fact]
        public void GetAsync_WithCurrencyToken_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockBundlesService = new Mock<IBundleService>();

            var bundle = new List<Bundle>
            {
                new Bundle(new Token(50), new Price(new Money(20)))
            };

            mockBundlesService.Setup(accountService => accountService.FetchDepositTokenBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();

            var controller = new AccountDepositController(mockBundlesService.Object, TestMapper);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = controller.Get(Currency.Token);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockBundlesService.Verify(accountService => accountService.FetchDepositTokenBundles(), Times.Once);
        }

        //[Fact]
        //public async Task PostAsync_ShouldBeOfTypeBadRequestObjectResult()
        //{
        //    // Arrange
        //    var mockAccountService = new Mock<IAccountService>();

        //    mockAccountService.Setup(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>()))
        //        .ReturnsAsync(new Account(new UserId()))
        //        .Verifiable();

        //    mockAccountService.Setup(
        //            accountService => accountService.CreateTransactionAsync(
        //                It.IsAny<IAccount>(),
        //                It.IsAny<decimal>(),
        //                It.IsAny<Currency>(),
        //                It.IsAny<TransactionType>(),
        //                It.IsAny<TransactionMetadata>(),
        //                It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(DomainValidationResult.Failure("test", "test error"))
        //        .Verifiable();

        //    var mockBundlesService = new Mock<IBundlesService>();

        //    var controller = new AccountDepositController(mockBundlesService.Object, TestMapper);

        //    var mockHttpContextAccessor = new MockHttpContextAccessor();

        //    controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

        //    // Act
        //    var result = await controller.PostAsync(Currency.Token, Token.FiftyThousand);

        //    // Assert
        //    result.Should().BeOfType<BadRequestObjectResult>();

        //    mockAccountService.Verify(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>()), Times.Once);

        //    mockHttpContextAccessor.VerifyGet(Times.Exactly(2));

        //    mockAccountService.Verify(
        //        accountService => accountService.CreateTransactionAsync(
        //            It.IsAny<IAccount>(),
        //            It.IsAny<decimal>(),
        //            It.IsAny<Currency>(),
        //            It.IsAny<TransactionType>(),
        //            It.IsAny<TransactionMetadata>(),
        //            It.IsAny<CancellationToken>()),
        //        Times.Once);
        //}

        //[Fact]
        //public async Task PostAsync_ShouldBeOfTypeNotFoundObjectResult()
        //{
        //    // Arrange
        //    var mockAccountService = new Mock<IAccountService>();

        //    mockAccountService.Setup(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>())).Verifiable();

        //    mockAccountService.Setup(
        //            accountService => accountService.CreateTransactionAsync(
        //                It.IsAny<IAccount>(),
        //                It.IsAny<decimal>(),
        //                It.IsAny<Currency>(),
        //                It.IsAny<TransactionType>(),
        //                It.IsAny<TransactionMetadata>(),
        //                It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(new DomainValidationResult())
        //        .Verifiable();

        //    var mockBundlesService = new Mock<IBundlesService>();

        //    var controller = new AccountDepositController(mockBundlesService.Object, TestMapper);

        //    var mockHttpContextAccessor = new MockHttpContextAccessor();

        //    controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

        //    // Act
        //    var result = await controller.PostAsync(Currency.Token, Token.FiftyThousand);

        //    // Assert
        //    result.Should().BeOfType<NotFoundObjectResult>();

        //    mockAccountService.Verify(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>()), Times.Once);

        //    mockHttpContextAccessor.VerifyGet(Times.Exactly(2));

        //    mockAccountService.Verify(
        //        accountService => accountService.CreateTransactionAsync(
        //            It.IsAny<IAccount>(),
        //            It.IsAny<decimal>(),
        //            It.IsAny<Currency>(),
        //            It.IsAny<TransactionType>(),
        //            It.IsAny<TransactionMetadata>(),
        //            It.IsAny<CancellationToken>()),
        //        Times.Never);
        //}

        //[Fact]
        //public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        //{
        //    // Arrange
        //    var mockAccountService = new Mock<IAccountService>();

        //    mockAccountService.Setup(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>()))
        //        .ReturnsAsync(new Account(new UserId()))
        //        .Verifiable();

        //    mockAccountService.Setup(
        //            accountService => accountService.CreateTransactionAsync(
        //                It.IsAny<IAccount>(),
        //                It.IsAny<decimal>(),
        //                It.IsAny<Currency>(),
        //                It.IsAny<TransactionType>(),
        //                It.IsAny<TransactionMetadata>(),
        //                It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(new DomainValidationResult())
        //        .Verifiable();

        //    var mockBundlesService = new Mock<IBundlesService>();

        //    var controller = new AccountDepositController(mockBundlesService.Object, TestMapper);

        //    var mockHttpContextAccessor = new MockHttpContextAccessor();

        //    controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

        //    // Act
        //    var result = await controller.PostAsync(Currency.Token, Token.FiftyThousand);

        //    // Assert
        //    result.Should().BeOfType<OkObjectResult>();

        //    mockAccountService.Verify(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>()), Times.Once);

        //    mockHttpContextAccessor.VerifyGet(Times.Exactly(2));

        //    mockAccountService.Verify(
        //        accountService => accountService.CreateTransactionAsync(
        //            It.IsAny<IAccount>(),
        //            It.IsAny<decimal>(),
        //            It.IsAny<Currency>(),
        //            It.IsAny<TransactionType>(),
        //            It.IsAny<TransactionMetadata>(),
        //            It.IsAny<CancellationToken>()),
        //        Times.Once);
        //}
    }
}
