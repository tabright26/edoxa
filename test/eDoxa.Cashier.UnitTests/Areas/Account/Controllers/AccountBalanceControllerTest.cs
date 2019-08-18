// Filename: AccountBalanceControllerTest.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Accounts.Controllers;
using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.UnitTests.Helpers.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Areas.Account.Controllers
{
    [TestClass]
    public sealed class AccountBalanceControllerTest
    {
        private Mock<IAccountQuery> _mockAccountQuery;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockAccountQuery = new Mock<IAccountQuery>();
        }

        [TestMethod]
        public async Task GetBalanceAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var accountFaker = new AccountFaker();
            var account = accountFaker.Generate();

            _mockAccountQuery.Setup(mediator => mediator.FindUserBalanceAsync(It.IsAny<Currency>()))
                .ReturnsAsync(account.GetBalanceFor(Currency.Money))
                .Verifiable();

            _mockAccountQuery.SetupGet(accountQuery => accountQuery.Mapper).Returns(MapperExtensions.Mapper);
            var controller = new AccountBalanceController(_mockAccountQuery.Object);

            // Act
            var result = await controller.GetByCurrencyAsync(Currency.Money);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _mockAccountQuery.Verify(accountQuery => accountQuery.FindUserBalanceAsync(It.IsAny<Currency>()), Times.Once);
        }

        [TestMethod]
        public async Task GetBalanceAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            _mockAccountQuery.Setup(accountQuery => accountQuery.FindUserBalanceAsync(It.IsAny<Currency>())).ReturnsAsync((Balance) null).Verifiable();
            _mockAccountQuery.SetupGet(accountQuery => accountQuery.Mapper).Returns(MapperExtensions.Mapper);
            var controller = new AccountBalanceController(_mockAccountQuery.Object);

            // Act
            var result = await controller.GetByCurrencyAsync(null);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            _mockAccountQuery.Verify(accountQuery => accountQuery.FindUserBalanceAsync(It.IsAny<Currency>()), Times.Never);
        }
    }
}
