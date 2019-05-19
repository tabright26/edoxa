﻿// Filename: AccountTransactionsControllerTest.cs
// Date Created: 2019-05-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.Domain;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Testing.MSTest;

using FluentAssertions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Api.Tests.Controllers
{
    [TestClass]
    public sealed class AccountTransactionsControllerTest
    {
        private Mock<ITransactionQueries> _mockTransactionQueries;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockTransactionQueries = new Mock<ITransactionQueries>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<AccountTransactionsController>.For(typeof(ITransactionQueries))
                                                           .WithName("AccountTransactionsController")
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
        public async Task DepositTokenAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            _mockTransactionQueries.Setup(mock => mock.GetTransactionsAsync(It.IsAny<AccountCurrency>()))
                                   .ReturnsAsync(
                                       new TransactionListDTO
                                       {
                                           Items = new List<TransactionDTO>
                                           {
                                               new TransactionDTO()
                                           }
                                       }
                                   )
                                   .Verifiable();

            var controller = new AccountTransactionsController(_mockTransactionQueries.Object);

            // Act
            var result = await controller.GetTransactionsAsync(AccountCurrency.All);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockTransactionQueries.Verify(mock => mock.GetTransactionsAsync(It.IsAny<AccountCurrency>()), Times.Once);
        }

        [TestMethod]
        public async Task DepositTokenAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            _mockTransactionQueries.Setup(mock => mock.GetTransactionsAsync(It.IsAny<AccountCurrency>())).ReturnsAsync(new TransactionListDTO()).Verifiable();

            var controller = new AccountTransactionsController(_mockTransactionQueries.Object);

            // Act
            var result = await controller.GetTransactionsAsync(AccountCurrency.All);

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _mockTransactionQueries.Verify(mock => mock.GetTransactionsAsync(It.IsAny<AccountCurrency>()), Times.Once);
        }
    }
}