// Filename: AccountBalanceControllerTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Fakers;
using eDoxa.Cashier.Api.Area.Account.Controllers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.UnitTests.Helpers.Extensions;
using eDoxa.Seedwork.Testing.TestConstructor;

using FluentAssertions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Area.Account.Controllers
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
        public void Constructor_Tests()
        {
            TestConstructor<AccountBalanceController>.ForParameters(typeof(IAccountQuery))
                .WithClassName("AccountBalanceController")
                .WithClassAttributes(
                    typeof(AuthorizeAttribute),
                    typeof(ApiControllerAttribute),
                    typeof(ApiVersionAttribute),
                    typeof(ProducesAttribute),
                    typeof(RouteAttribute),
                    typeof(ApiExplorerSettingsAttribute)
                )
                .Assert();
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
