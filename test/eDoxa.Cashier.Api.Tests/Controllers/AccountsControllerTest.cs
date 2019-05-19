// Filename: AccountsControllerTest.cs
// Date Created: 2019-05-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.Domain;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Functional;
using eDoxa.Testing.MSTest;

using FluentAssertions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Api.Tests.Controllers
{
    [TestClass]
    public sealed class AccountsControllerTest
    {
        private Mock<IAccountQueries> _mockAccountQueries;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockAccountQueries = new Mock<IAccountQueries>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<AccountsController>.For(typeof(IAccountQueries))
                                                .WithName("AccountsController")
                                                .WithAttributes(
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
        public async Task GetAccountAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            _mockAccountQueries.Setup(mediator => mediator.GetAccountAsync(It.IsAny<AccountCurrency>()))
                               .ReturnsAsync(new Option<AccountDTO>(new AccountDTO()))
                               .Verifiable();

            var controller = new AccountsController(_mockAccountQueries.Object);

            // Act
            var result = await controller.GetAccountAsync(AccountCurrency.All);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockAccountQueries.Verify(mediator => mediator.GetAccountAsync(It.IsAny<AccountCurrency>()), Times.Once);
        }

        [TestMethod]
        public async Task GetAccountAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            _mockAccountQueries.Setup(mediator => mediator.GetAccountAsync(It.IsAny<AccountCurrency>())).ReturnsAsync(new Option<AccountDTO>()).Verifiable();

            var controller = new AccountsController(_mockAccountQueries.Object);

            // Act
            var result = await controller.GetAccountAsync(AccountCurrency.All);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            _mockAccountQueries.Verify(mediator => mediator.GetAccountAsync(It.IsAny<AccountCurrency>()), Times.Once);
        }
    }
}
