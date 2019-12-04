// Filename: AccountWithdrawalControllerTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

namespace eDoxa.Cashier.UnitTests.Areas.Accounts.Controllers
{
    public sealed class AccountWithdrawalControllerTest : UnitTest
    {
        public AccountWithdrawalControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        //[Fact]
        //public void GetAsync_WithCurrencyAll_ShouldBeOfTypeBadRequestObjectResult()
        //{
        //    // Arrange
        //    var mockBundlesService = new Mock<IBundleService>();

        //    var controller = new AccountWithdrawalController(mockBundlesService.Object, TestMapper);

        //    var mockHttpContextAccessor = new MockHttpContextAccessor();

        //    controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

        //    // Act
        //    var result = controller.Get(Currency.All);

        //    // Assert
        //    result.Should().BeOfType<BadRequestObjectResult>();
        //}

        //[Fact]
        //public void GetAsync_WithCurrencyMoney_ShouldBeOfTypeOkObjectResult()
        //{
        //    // Arrange
        //    var mockBundlesService = new Mock<IBundleService>();

        //    var bundle = new List<Bundle>
        //    {
        //        new Bundle(new Token(100), new Price(new Money(50)))
        //    };

        //    mockBundlesService.Setup(bundleService => bundleService.FetchWithdrawalMoneyBundles()).Returns(bundle.ToImmutableHashSet()).Verifiable();

        //    var controller = new AccountWithdrawalController(mockBundlesService.Object, TestMapper);

        //    var mockHttpContextAccessor = new MockHttpContextAccessor();

        //    controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

        //    // Act
        //    var result = controller.Get(Currency.Money);

        //    // Assert
        //    result.Should().BeOfType<OkObjectResult>();

        //    mockBundlesService.Verify(accountService => accountService.FetchWithdrawalMoneyBundles(), Times.Once);
        //}

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
        //        .ReturnsAsync(DomainValidationResult.Failure("error", "error test"))
        //        .Verifiable();

        //    var mockBundlesService = new Mock<IBundlesService>();

        //    var controller = new AccountWithdrawalController(mockBundlesService.Object, TestMapper);

        //    var mockHttpContextAccessor = new MockHttpContextAccessor();

        //    controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

        //    // Act
        //    var result = await controller.PostAsync(Currency.Money, Money.Fifty);

        //    // Assert
        //    result.Should().BeOfType<BadRequestObjectResult>();

        //    mockAccountService.Verify(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>()), Times.AtLeastOnce);

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

        //    var controller = new AccountWithdrawalController(mockBundlesService.Object, TestMapper);

        //    var mockHttpContextAccessor = new MockHttpContextAccessor();

        //    controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

        //    // Act
        //    var result = await controller.PostAsync(Currency.Money, Money.Fifty);

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

        //    var controller = new AccountWithdrawalController(mockBundlesService.Object, TestMapper);

        //    var mockHttpContextAccessor = new MockHttpContextAccessor();

        //    controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

        //    // Act
        //    var result = await controller.PostAsync(Currency.Money, Money.Fifty);

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
