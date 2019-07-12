// Filename: TransactionsControllerTest.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.ObjectModel;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Area.Transaction.Controllers;
using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.UnitTests.Helpers.Extensions;
using eDoxa.Seedwork.Testing.TestConstructor;

using FluentAssertions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Area.Transaction.Controllers
{
    [TestClass]
    public sealed class TransactionsControllerTest
    {
        private Mock<ITransactionQuery> _mockTransactionQuery;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockTransactionQuery = new Mock<ITransactionQuery>();
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
            var transactionFaker = new TransactionFaker();

            _mockTransactionQuery
                .Setup(
                    transactionQuery => transactionQuery.FindUserTransactionsAsync(
                        It.IsAny<Currency>(),
                        It.IsAny<TransactionType>(),
                        It.IsAny<TransactionStatus>()
                    )
                )
                .ReturnsAsync(transactionFaker.Generate(5, TransactionFaker.PositiveTransaction))
                .Verifiable();

            _mockTransactionQuery.SetupGet(transactionQuery => transactionQuery.Mapper).Returns(MapperExtensions.Mapper);

            var controller = new TransactionsController(_mockTransactionQuery.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockTransactionQuery.Verify(
                transactionQuery =>
                    transactionQuery.FindUserTransactionsAsync(It.IsAny<Currency>(), It.IsAny<TransactionType>(), It.IsAny<TransactionStatus>()),
                Times.Once
            );
        }

        [TestMethod]
        public async Task DepositTokenAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            _mockTransactionQuery
                .Setup(
                    transactionQuery => transactionQuery.FindUserTransactionsAsync(
                        It.IsAny<Currency>(),
                        It.IsAny<TransactionType>(),
                        It.IsAny<TransactionStatus>()
                    )
                )
                .ReturnsAsync(new Collection<ITransaction>())
                .Verifiable();

            _mockTransactionQuery.SetupGet(transactionQuery => transactionQuery.Mapper).Returns(MapperExtensions.Mapper);

            var controller = new TransactionsController(_mockTransactionQuery.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _mockTransactionQuery.Verify(
                transactionQuery =>
                    transactionQuery.FindUserTransactionsAsync(It.IsAny<Currency>(), It.IsAny<TransactionType>(), It.IsAny<TransactionStatus>()),
                Times.Once
            );
        }
    }
}
