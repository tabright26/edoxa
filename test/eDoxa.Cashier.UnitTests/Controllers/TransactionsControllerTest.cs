// Filename: TransactionsControllerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.ObjectModel;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Cashier.TestHelper.Mocks;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Controllers
{
    public sealed class TransactionsControllerTest : UnitTest
    {
        public TransactionsControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public async Task FetchUserTransactionsAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockTransactionQuery = new Mock<ITransactionQuery>();

            mockTransactionQuery
                .Setup(
                    transactionQuery => transactionQuery.FetchUserTransactionsAsync(
                        It.IsAny<UserId>(),
                        It.IsAny<Currency>(),
                        It.IsAny<TransactionType>(),
                        It.IsAny<TransactionStatus>()))
                .ReturnsAsync(new Collection<ITransaction>())
                .Verifiable();

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            var controller = new TransactionsController(mockTransactionQuery.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = mockHttpContextAccessor.Object.HttpContext
                }
            };

            // Act
            var result = await controller.FetchUserTransactionsAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockTransactionQuery.Verify(
                transactionQuery => transactionQuery.FetchUserTransactionsAsync(
                    It.IsAny<UserId>(),
                    It.IsAny<Currency>(),
                    It.IsAny<TransactionType>(),
                    It.IsAny<TransactionStatus>()),
                Times.Once);
        }

        [Fact]
        public async Task FetchUserTransactionsAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var faker = TestData.FakerFactory.CreateTransactionFaker(null);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            var mockTransactionQuery = new Mock<ITransactionQuery>();

            mockTransactionQuery
                .Setup(
                    transactionQuery => transactionQuery.FetchUserTransactionsAsync(
                        It.IsAny<UserId>(),
                        It.IsAny<Currency>(),
                        It.IsAny<TransactionType>(),
                        It.IsAny<TransactionStatus>()))
                .ReturnsAsync(faker.FakeTransactions(5, TransactionFaker.PositiveTransaction))
                .Verifiable();

            var controller = new TransactionsController(mockTransactionQuery.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = mockHttpContextAccessor.Object.HttpContext
                }
            };

            // Act
            var result = await controller.FetchUserTransactionsAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockTransactionQuery.Verify(
                transactionQuery => transactionQuery.FetchUserTransactionsAsync(
                    It.IsAny<UserId>(),
                    It.IsAny<Currency>(),
                    It.IsAny<TransactionType>(),
                    It.IsAny<TransactionStatus>()),
                Times.Once);
        }
    }
}
