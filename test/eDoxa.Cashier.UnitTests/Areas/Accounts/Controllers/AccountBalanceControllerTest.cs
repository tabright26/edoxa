// Filename: AccountBalanceControllerTest.cs
// Date Created: 2019-08-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Accounts.Controllers;
using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.UnitTests.Helpers.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Areas.Accounts.Controllers
{
    [TestClass]
    public sealed class AccountBalanceControllerTest
    {
        [TestMethod]
        public async Task GetByCurrencyAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockAccountQuery = new Mock<IAccountQuery>();

            var accountFaker = new AccountFaker();

            var account = accountFaker.Generate();

            mockAccountQuery.Setup(mediator => mediator.FindUserBalanceAsync(It.IsAny<Currency>()))
                .ReturnsAsync(account.GetBalanceFor(Currency.Money))
                .Verifiable();

            mockAccountQuery.SetupGet(accountQuery => accountQuery.Mapper).Returns(MapperExtensions.Mapper);

            var controller = new AccountBalanceController(mockAccountQuery.Object);

            // Act
            var result = await controller.GetByCurrencyAsync(Currency.Money);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockAccountQuery.Verify(accountQuery => accountQuery.FindUserBalanceAsync(It.IsAny<Currency>()), Times.Once);

            mockAccountQuery.VerifyGet(accountQuery => accountQuery.Mapper, Times.Once);
        }

        [TestMethod]
        public async Task GetByCurrencyAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockAccountQuery = new Mock<IAccountQuery>();

            mockAccountQuery.Setup(mediator => mediator.FindUserBalanceAsync(It.IsAny<Currency>())).Verifiable();

            mockAccountQuery.SetupGet(accountQuery => accountQuery.Mapper).Returns(MapperExtensions.Mapper).Verifiable();

            var controller = new AccountBalanceController(mockAccountQuery.Object);

            // Act
            var result = await controller.GetByCurrencyAsync(Currency.Money);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockAccountQuery.Verify(accountQuery => accountQuery.FindUserBalanceAsync(It.IsAny<Currency>()), Times.Once);

            mockAccountQuery.VerifyGet(accountQuery => accountQuery.Mapper, Times.Once);
        }

        [TestMethod]
        public async Task GetByCurrencyAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockAccountQuery = new Mock<IAccountQuery>();

            mockAccountQuery.Setup(mediator => mediator.FindUserBalanceAsync(It.IsAny<Currency>())).Verifiable();

            mockAccountQuery.SetupGet(accountQuery => accountQuery.Mapper).Returns(MapperExtensions.Mapper).Verifiable();

            var controller = new AccountBalanceController(mockAccountQuery.Object);

            controller.ControllerContext.ModelState.AddModelError("error", "error");

            // Act
            var result = await controller.GetByCurrencyAsync(Currency.Money);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockAccountQuery.Verify(accountQuery => accountQuery.FindUserBalanceAsync(It.IsAny<Currency>()), Times.Never);

            mockAccountQuery.VerifyGet(accountQuery => accountQuery.Mapper, Times.Never);
        }
    }
}
