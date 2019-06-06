// Filename: TransactionsControllerTest.cs
// Date Created: 2019-05-29
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
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Seedwork.Domain.Common.Enumerations;
using eDoxa.Testing.MSTest.Constructor;

using FluentAssertions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Tests.Controllers
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
            ConstructorTests<TransactionsController>.For(typeof(ITransactionQuery))
                .WithName("TransactionsController")
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
            _mockTransactionQueries
                .Setup(mock => mock.GetTransactionsAsync(It.IsAny<CurrencyType>(), It.IsAny<TransactionType>(), It.IsAny<TransactionStatus>()))
                .ReturnsAsync(
                    new List<TransactionDTO>
                    {
                        new TransactionDTO()
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
                .ReturnsAsync(new List<TransactionDTO>())
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
