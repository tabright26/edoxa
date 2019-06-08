// Filename: TransactionsControllerTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Abstractions;
using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Domain.Common.Enumerations;
using eDoxa.Seedwork.Testing.TestConstructor;

using FluentAssertions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Controllers
{
    [TestClass]
    public sealed class TransactionsControllerTest
    {
        private Mock<ITransactionQuery> _mockTransactionQueries;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockTransactionQueries = new Mock<ITransactionQuery>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<TransactionsController>.ForParameters(typeof(ITransactionQuery))
                .WithClassName("TransactionsController")
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
        public async Task DepositTokenAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            _mockTransactionQueries
                .Setup(mock => mock.GetTransactionsAsync(It.IsAny<CurrencyType>(), It.IsAny<TransactionType>(), It.IsAny<TransactionStatus>()))
                .ReturnsAsync(
                    new List<TransactionViewModel>
                    {
                        new TransactionViewModel()
                    }
                )
                .Verifiable();

            var controller = new TransactionsController(_mockTransactionQueries.Object);

            // Act
            var result = await controller.GetTransactionsAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockTransactionQueries.Verify(
                mock => mock.GetTransactionsAsync(It.IsAny<CurrencyType>(), It.IsAny<TransactionType>(), It.IsAny<TransactionStatus>()),
                Times.Once
            );
        }

        [TestMethod]
        public async Task DepositTokenAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            _mockTransactionQueries
                .Setup(mock => mock.GetTransactionsAsync(It.IsAny<CurrencyType>(), It.IsAny<TransactionType>(), It.IsAny<TransactionStatus>()))
                .ReturnsAsync(new List<TransactionViewModel>())
                .Verifiable();

            var controller = new TransactionsController(_mockTransactionQueries.Object);

            // Act
            var result = await controller.GetTransactionsAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _mockTransactionQueries.Verify(
                mock => mock.GetTransactionsAsync(It.IsAny<CurrencyType>(), It.IsAny<TransactionType>(), It.IsAny<TransactionStatus>()),
                Times.Once
            );
        }
    }
}
