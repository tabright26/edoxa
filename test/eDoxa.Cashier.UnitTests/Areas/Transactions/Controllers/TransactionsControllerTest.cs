// Filename: TransactionsControllerTest.cs
// Date Created: 2019-09-16
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Accounts.Services.Abstractions;
using eDoxa.Cashier.Api.Areas.Transactions.Controllers;
using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Requests;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Cashier.TestHelper.Mocks;
using eDoxa.Seedwork.Application.Validations.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using FluentValidation.Results;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Areas.Transactions.Controllers
{
    public sealed class TransactionsControllerTest : UnitTest
    {
        public TransactionsControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockTransactionQuery = new Mock<ITransactionQuery>();
            var mockAccountService = new Mock<IAccountService>();

            mockTransactionQuery
                .Setup(
                    transactionQuery => transactionQuery.FetchUserTransactionsAsync(
                        It.IsAny<Currency>(),
                        It.IsAny<TransactionType>(),
                        It.IsAny<TransactionStatus>()))
                .ReturnsAsync(new Collection<ITransaction>())
                .Verifiable();

            mockTransactionQuery.SetupGet(transactionQuery => transactionQuery.Mapper).Returns(TestMapper);

            var controller = new TransactionsController(mockTransactionQuery.Object, mockAccountService.Object);

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
            var mockAccountService = new Mock<IAccountService>();

            mockTransactionQuery
                .Setup(
                    transactionQuery => transactionQuery.FetchUserTransactionsAsync(
                        It.IsAny<Currency>(),
                        It.IsAny<TransactionType>(),
                        It.IsAny<TransactionStatus>()))
                .ReturnsAsync(faker.FakeTransactions(5, TransactionFaker.PositiveTransaction))
                .Verifiable();

            mockTransactionQuery.SetupGet(transactionQuery => transactionQuery.Mapper).Returns(TestMapper);

            var controller = new TransactionsController(mockTransactionQuery.Object, mockAccountService.Object);

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

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockTransactionQuery = new Mock<ITransactionQuery>();
            var mockAccountService = new Mock<IAccountService>();

            var account = new Account(new UserId());

            var transaction = new Transaction(
                new Money(50),
                new TransactionDescription("test"),
                TransactionType.Deposit,
                new UtcNowDateTimeProvider());

            mockAccountService
                .Setup(
                    accountService => accountService.FindUserAccountAsync(
                        It.IsAny<UserId>()))
                .ReturnsAsync(account)
                .Verifiable();

            mockAccountService
                .Setup(
                    accountService => accountService.CreateTransactionAsync(
                        It.IsAny<IAccount>(), It.IsAny<decimal>(), It.IsAny<Currency>(), It.IsAny<TransactionId>(), It.IsAny<TransactionType>(), It.IsAny<TransactionMetadata>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            mockTransactionQuery
                .Setup(
                    transactionQuery => transactionQuery.FindTransactionAsync(
                        It.IsAny<TransactionId>()))
                .ReturnsAsync(transaction)
                .Verifiable();

            mockTransactionQuery.SetupGet(transactionQuery => transactionQuery.Mapper).Returns(TestMapper);

            var controller = new TransactionsController(mockTransactionQuery.Object, mockAccountService.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await controller.PostAsync(new CreateTransactionRequest(new Guid(), "Deposit", "Money", 50));

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockAccountService.Verify(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>()), Times.Once);
            mockAccountService.Verify(accountService => accountService.CreateTransactionAsync(
                It.IsAny<IAccount>(), It.IsAny<decimal>(), It.IsAny<Currency>(), It.IsAny<TransactionId>(), It.IsAny<TransactionType>(), It.IsAny<TransactionMetadata>(), It.IsAny<CancellationToken>()), Times.Once);

            mockTransactionQuery.Verify(transactionQuery => transactionQuery.FindTransactionAsync(It.IsAny<TransactionId>()), Times.Once);

            mockTransactionQuery.VerifyGet(transactionQuery => transactionQuery.Mapper, Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockTransactionQuery = new Mock<ITransactionQuery>();
            var mockAccountService = new Mock<IAccountService>();

            mockAccountService
                .Setup(
                    accountService => accountService.FindUserAccountAsync(
                        It.IsAny<UserId>()))
                .Verifiable();

            var controller = new TransactionsController(mockTransactionQuery.Object, mockAccountService.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await controller.PostAsync(new CreateTransactionRequest(new Guid(), "Deposit", "Money", 50));

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            mockAccountService.Verify(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockTransactionQuery = new Mock<ITransactionQuery>();
            var mockAccountService = new Mock<IAccountService>();

            var account = new Account(new UserId());

            mockAccountService
                .Setup(
                    accountService => accountService.FindUserAccountAsync(
                        It.IsAny<UserId>()))
                .ReturnsAsync(account)
                .Verifiable();

            mockAccountService
                .Setup(
                    accountService => accountService.CreateTransactionAsync(
                        It.IsAny<IAccount>(), It.IsAny<decimal>(), It.IsAny<Currency>(), It.IsAny<TransactionId>(), It.IsAny<TransactionType>(), It.IsAny<TransactionMetadata>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationFailure("test", "test message").ToResult())
                .Verifiable();

            var controller = new TransactionsController(mockTransactionQuery.Object, mockAccountService.Object);

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = await controller.PostAsync(new CreateTransactionRequest(new Guid(), "Deposit", "Money", 50));

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            mockAccountService.Verify(accountService => accountService.FindUserAccountAsync(It.IsAny<UserId>()), Times.Once);
            mockAccountService.Verify(accountService => accountService.CreateTransactionAsync(
                It.IsAny<IAccount>(), It.IsAny<decimal>(), It.IsAny<Currency>(), It.IsAny<TransactionId>(), It.IsAny<TransactionType>(), It.IsAny<TransactionMetadata>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
