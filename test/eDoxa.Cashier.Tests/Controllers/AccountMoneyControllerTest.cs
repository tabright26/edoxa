// Filename: AccountMoneyControllerTest.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Cashier.Tests.Controllers
{
    //[TestClass]
    //public sealed class AccountMoneyControllerTest
    //{
    //    private Mock<IMediator> _mockMediator;

    //    [TestInitialize]
    //    public void TestInitialize()
    //    {
    //        _mockMediator = new Mock<IMediator>();
    //    }

    //    [TestMethod]
    //    public void Constructor_Tests()
    //    {
    //        ConstructorTests<AccountMoneyController>.For(typeof(IMediator))
    //            .WithName("AccountMoneyController")
    //            .WithAttributes(
    //                typeof(AuthorizeAttribute),
    //                typeof(ApiControllerAttribute),
    //                typeof(ApiVersionAttribute),
    //                typeof(ProducesAttribute),
    //                typeof(RouteAttribute),
    //                typeof(ApiExplorerSettingsAttribute)
    //            )
    //            .Assert();
    //    }

    //    [TestMethod]
    //    public async Task DepositMoneyAsync_ShouldBeOfTypeOkObjectResult()
    //    {
    //        // Arrange
    //        _mockMediator.Setup(mediator => mediator.Send(It.IsAny<DepositCommand>(), It.IsAny<CancellationToken>()))
    //            .ReturnsAsync(TransactionStatus.Completed)
    //            .Verifiable();

    //        var controller = new AccountMoneyController(_mockMediator.Object);

    //        // Act
    //        var result = await controller.DepositMoneyAsync(new DepositCommand(CurrencyType.Money, 10));

    //        // Assert
    //        result.Should().BeOfType<OkObjectResult>();

    //        _mockMediator.Verify(mediator => mediator.Send(It.IsAny<DepositCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    //    }

    //    [TestMethod]
    //    public async Task DepositMoneyAsync_ShouldBeOfTypeBadRequestObjectResult()
    //    {
    //        // Arrange
    //        _mockMediator.Setup(mediator => mediator.Send(It.IsAny<DepositCommand>(), It.IsAny<CancellationToken>()))
    //            .ReturnsAsync(new ValidationResult())
    //            .Verifiable();

    //        var controller = new AccountMoneyController(_mockMediator.Object);

    //        // Act
    //        var result = await controller.DepositMoneyAsync(new DepositCommand(CurrencyType.Money, 10));

    //        // Assert
    //        result.Should().BeOfType<BadRequestObjectResult>();

    //        _mockMediator.Verify(mediator => mediator.Send(It.IsAny<DepositCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    //    }

    //    [TestMethod]
    //    public async Task WithdrawMoneyAsync_ShouldBeOfTypeOkObjectResult()
    //    {
    //        // Arrange
    //        _mockMediator.Setup(mediator => mediator.Send(It.IsAny<WithdrawCommand>(), It.IsAny<CancellationToken>()))
    //            .ReturnsAsync(TransactionStatus.Completed)
    //            .Verifiable();

    //        var controller = new AccountMoneyController(_mockMediator.Object);

    //        // Act
    //        var result = await controller.WithdrawMoneyAsync(new WithdrawCommand(MoneyWithdrawBundleType.Fifty));

    //        // Assert
    //        result.Should().BeOfType<OkObjectResult>();

    //        _mockMediator.Verify(mediator => mediator.Send(It.IsAny<WithdrawCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    //    }

    //    [TestMethod]
    //    public async Task WithdrawMoneyAsync_ShouldBeOfTypeBadRequestObjectResult()
    //    {
    //        // Arrange
    //        _mockMediator.Setup(mediator => mediator.Send(It.IsAny<WithdrawCommand>(), It.IsAny<CancellationToken>()))
    //            .ReturnsAsync(new ValidationResult())
    //            .Verifiable();

    //        var controller = new AccountMoneyController(_mockMediator.Object);

    //        // Act
    //        var result = await controller.WithdrawMoneyAsync(new WithdrawCommand(MoneyWithdrawBundleType.Fifty));

    //        // Assert
    //        result.Should().BeOfType<BadRequestObjectResult>();

    //        _mockMediator.Verify(mediator => mediator.Send(It.IsAny<WithdrawCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    //    }
    //}
}
