// Filename: TransactionsControllerTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.ObjectModel;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Transactions.Controllers;
using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.UnitTests.TestHelpers;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Areas.Transactions.Controllers
{
    public sealed class TransactionsControllerTest : UnitTestClass
    {
        public TransactionsControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockTransactionQuery = new Mock<ITransactionQuery>();

            mockTransactionQuery
                .Setup(
                    transactionQuery => transactionQuery.FindUserTransactionsAsync(
                        It.IsAny<Currency>(),
                        It.IsAny<TransactionType>(),
                        It.IsAny<TransactionStatus>()))
                .ReturnsAsync(new Collection<ITransaction>())
                .Verifiable();

            mockTransactionQuery.SetupGet(transactionQuery => transactionQuery.Mapper).Returns(TestMapper);

            var controller = new TransactionsController(mockTransactionQuery.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockTransactionQuery.Verify(
                transactionQuery =>
                    transactionQuery.FindUserTransactionsAsync(It.IsAny<Currency>(), It.IsAny<TransactionType>(), It.IsAny<TransactionStatus>()),
                Times.Once);

            mockTransactionQuery.VerifyGet(transactionQuery => transactionQuery.Mapper, Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var faker = TestData.FakerFactory.CreateTransactionFaker(null);

            var mockTransactionQuery = new Mock<ITransactionQuery>();

            mockTransactionQuery
                .Setup(
                    transactionQuery => transactionQuery.FindUserTransactionsAsync(
                        It.IsAny<Currency>(),
                        It.IsAny<TransactionType>(),
                        It.IsAny<TransactionStatus>()))
                .ReturnsAsync(faker.FakeTransactions(5, TransactionFaker.PositiveTransaction))
                .Verifiable();

            mockTransactionQuery.SetupGet(transactionQuery => transactionQuery.Mapper).Returns(TestMapper);

            var controller = new TransactionsController(mockTransactionQuery.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockTransactionQuery.Verify(
                transactionQuery =>
                    transactionQuery.FindUserTransactionsAsync(It.IsAny<Currency>(), It.IsAny<TransactionType>(), It.IsAny<TransactionStatus>()),
                Times.Once);

            mockTransactionQuery.VerifyGet(transactionQuery => transactionQuery.Mapper, Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockTransactionQuery = new Mock<ITransactionQuery>();

            mockTransactionQuery.Setup(
                    transactionQuery =>
                        transactionQuery.FindUserTransactionsAsync(It.IsAny<Currency>(), It.IsAny<TransactionType>(), It.IsAny<TransactionStatus>()))
                .Verifiable();

            mockTransactionQuery.SetupGet(accountQuery => accountQuery.Mapper).Verifiable();

            var controller = new TransactionsController(mockTransactionQuery.Object);

            controller.ControllerContext.ModelState.AddModelError("error", "error");

            // Act
            var result = await controller.GetAsync(Currency.Money);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockTransactionQuery.Verify(
                transactionQuery =>
                    transactionQuery.FindUserTransactionsAsync(It.IsAny<Currency>(), It.IsAny<TransactionType>(), It.IsAny<TransactionStatus>()),
                Times.Never);

            mockTransactionQuery.VerifyGet(accountQuery => accountQuery.Mapper, Times.Never);
        }
    }
}
