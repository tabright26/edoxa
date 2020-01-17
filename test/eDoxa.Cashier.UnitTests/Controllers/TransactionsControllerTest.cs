// Filename: TransactionsControllerTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.ObjectModel;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Controllers
{
    public sealed class TransactionsControllerTest : UnitTest
    {
     

        [Fact]
        public async Task GetAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockTransactionQuery = new Mock<ITransactionQuery>();

            mockTransactionQuery
                .Setup(
                    transactionQuery => transactionQuery.FetchUserTransactionsAsync(
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
                    transactionQuery.FetchUserTransactionsAsync(It.IsAny<Currency>(), It.IsAny<TransactionType>(), It.IsAny<TransactionStatus>()),
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
                    transactionQuery => transactionQuery.FetchUserTransactionsAsync(
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
                    transactionQuery.FetchUserTransactionsAsync(It.IsAny<Currency>(), It.IsAny<TransactionType>(), It.IsAny<TransactionStatus>()),
                Times.Once);

            mockTransactionQuery.VerifyGet(transactionQuery => transactionQuery.Mapper, Times.Once);
        }

        public TransactionsControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(testData, testMapper, testValidator)
        {
        }
    }
}
