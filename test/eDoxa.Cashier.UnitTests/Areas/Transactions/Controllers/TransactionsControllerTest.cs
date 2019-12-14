//// Filename: TransactionsControllerTest.cs
//// Date Created: 2019-11-25
//// 
//// ================================================
//// Copyright © 2019, eDoxa. All rights reserved.

//using System.Collections.ObjectModel;
//using System.Threading;
//using System.Threading.Tasks;

//using eDoxa.Cashier.Api.Areas.Transactions.Controllers;
//using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;
//using eDoxa.Cashier.Domain.AggregateModels;
//using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
//using eDoxa.Cashier.Domain.Queries;
//using eDoxa.Cashier.Domain.Services;
//using eDoxa.Cashier.Requests;
//using eDoxa.Cashier.TestHelper;
//using eDoxa.Cashier.TestHelper.Fixtures;
//using eDoxa.Cashier.TestHelper.Mocks;
//using eDoxa.Seedwork.Domain;
//using eDoxa.Seedwork.Domain.Misc;

//using FluentAssertions;

//using Microsoft.AspNetCore.Mvc;

//using Moq;

//using Xunit;

//namespace eDoxa.Cashier.UnitTests.Areas.Transactions.Controllers
//{
//    public sealed class TransactionsControllerTest : UnitTest
//    {
//        public TransactionsControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
//        {
//        }

//        [Fact]
//        public async Task GetAsync_ShouldBeOfTypeNoContentResult()
//        {
//            // Arrange
//            var mockTransactionQuery = new Mock<ITransactionQuery>();
//            var mockAccountService = new Mock<IAccountService>();

//            mockTransactionQuery
//                .Setup(
//                    transactionQuery => transactionQuery.FetchUserTransactionsAsync(
//                        It.IsAny<Currency>(),
//                        It.IsAny<TransactionType>(),
//                        It.IsAny<TransactionStatus>()))
//                .ReturnsAsync(new Collection<ITransaction>())
//                .Verifiable();

//            mockTransactionQuery.SetupGet(transactionQuery => transactionQuery.Mapper).Returns(TestMapper);

//            var controller = new TransactionsController(mockTransactionQuery.Object, mockAccountService.Object, TestMapper);

//            // Act
//            var result = await controller.GetAsync();

//            // Assert
//            result.Should().BeOfType<NoContentResult>();

//            mockTransactionQuery.Verify(
//                transactionQuery =>
//                    transactionQuery.FetchUserTransactionsAsync(It.IsAny<Currency>(), It.IsAny<TransactionType>(), It.IsAny<TransactionStatus>()),
//                Times.Once);

//            mockTransactionQuery.VerifyGet(transactionQuery => transactionQuery.Mapper, Times.Once);
//        }

//        [Fact]
//        public async Task GetAsync_ShouldBeOfTypeOkObjectResult()
//        {
//            // Arrange
//            var faker = TestData.FakerFactory.CreateTransactionFaker(null);

//            var mockTransactionQuery = new Mock<ITransactionQuery>();
//            var mockAccountService = new Mock<IAccountService>();

//            mockTransactionQuery
//                .Setup(
//                    transactionQuery => transactionQuery.FetchUserTransactionsAsync(
//                        It.IsAny<Currency>(),
//                        It.IsAny<TransactionType>(),
//                        It.IsAny<TransactionStatus>()))
//                .ReturnsAsync(faker.FakeTransactions(5, TransactionFaker.PositiveTransaction))
//                .Verifiable();

//            mockTransactionQuery.SetupGet(transactionQuery => transactionQuery.Mapper).Returns(TestMapper);

//            var controller = new TransactionsController(mockTransactionQuery.Object, mockAccountService.Object, TestMapper);

//            // Act
//            var result = await controller.GetAsync();

//            // Assert
//            result.Should().BeOfType<OkObjectResult>();

//            mockTransactionQuery.Verify(
//                transactionQuery =>
//                    transactionQuery.FetchUserTransactionsAsync(It.IsAny<Currency>(), It.IsAny<TransactionType>(), It.IsAny<TransactionStatus>()),
//                Times.Once);

//            mockTransactionQuery.VerifyGet(transactionQuery => transactionQuery.Mapper, Times.Once);
//        }

//        [Fact]
//        public async Task PostAsync_ShouldBeOfTypeBadRequestObjectResult()
//        {
//            // Arrange
//            var mockTransactionQuery = new Mock<ITransactionQuery>();
//            var mockAccountService = new Mock<IAccountService>();

//            var account = new Account(new UserId());

//            mockAccountService.Setup(accountService => accountService.FindAccountAsync(It.IsAny<UserId>())).ReturnsAsync(account).Verifiable();

//            mockAccountService.Setup(
//                    accountService => accountService.CreateTransactionAsync(
//                        It.IsAny<IAccount>(),
//                        It.IsAny<decimal>(),
//                        It.IsAny<Currency>(),
//                        It.IsAny<TransactionType>(),
//                        It.IsAny<TransactionMetadata>(),
//                        It.IsAny<CancellationToken>()))
//                .ReturnsAsync(DomainValidationResult.Failure("test", "test message"))
//                .Verifiable();

//            var controller = new TransactionsController(mockTransactionQuery.Object, mockAccountService.Object, TestMapper);

//            var mockHttpContextAccessor = new MockHttpContextAccessor();

//            controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

//            // Act
//            var result = await controller.PostAsync(new CreateTransactionRequest("Deposit", "Money", 50));

//            // Assert
//            result.Should().BeOfType<BadRequestObjectResult>();
//            mockAccountService.Verify(accountService => accountService.FindAccountAsync(It.IsAny<UserId>()), Times.Once);

//            mockAccountService.Verify(
//                accountService => accountService.CreateTransactionAsync(
//                    It.IsAny<IAccount>(),
//                    It.IsAny<decimal>(),
//                    It.IsAny<Currency>(),
//                    It.IsAny<TransactionType>(),
//                    It.IsAny<TransactionMetadata>(),
//                    It.IsAny<CancellationToken>()),
//                Times.Once);
//        }

//        [Fact]
//        public async Task PostAsync_ShouldBeOfTypeNotFoundObjectResult()
//        {
//            // Arrange
//            var mockTransactionQuery = new Mock<ITransactionQuery>();
//            var mockAccountService = new Mock<IAccountService>();

//            mockAccountService.Setup(accountService => accountService.FindAccountAsync(It.IsAny<UserId>())).Verifiable();

//            var controller = new TransactionsController(mockTransactionQuery.Object, mockAccountService.Object, TestMapper);

//            var mockHttpContextAccessor = new MockHttpContextAccessor();

//            controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

//            // Act
//            var result = await controller.PostAsync(new CreateTransactionRequest("Deposit", "Money", 50));

//            // Assert
//            result.Should().BeOfType<NotFoundObjectResult>();
//            mockAccountService.Verify(accountService => accountService.FindAccountAsync(It.IsAny<UserId>()), Times.Once);
//        }

//        [Fact]
//        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
//        {
//            // Arrange
//            var mockTransactionQuery = new Mock<ITransactionQuery>();
//            var mockAccountService = new Mock<IAccountService>();

//            var account = new Account(new UserId());

//            var transaction = new Transaction(
//                new Money(50),
//                new TransactionDescription("test"),
//                TransactionType.Deposit,
//                new UtcNowDateTimeProvider());

//            var validationResult = new DomainValidationResult();

//            validationResult.AddEntityToMetadata(transaction);

//            mockAccountService.Setup(accountService => accountService.FindAccountAsync(It.IsAny<UserId>())).ReturnsAsync(account).Verifiable();

//            mockAccountService.Setup(
//                    accountService => accountService.CreateTransactionAsync(
//                        It.IsAny<IAccount>(),
//                        It.IsAny<decimal>(),
//                        It.IsAny<Currency>(),
//                        It.IsAny<TransactionType>(),
//                        It.IsAny<TransactionMetadata>(),
//                        It.IsAny<CancellationToken>()))
//                .ReturnsAsync(validationResult)
//                .Verifiable();

//            var controller = new TransactionsController(mockTransactionQuery.Object, mockAccountService.Object, TestMapper);

//            var mockHttpContextAccessor = new MockHttpContextAccessor();

//            controller.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

//            // Act
//            var result = await controller.PostAsync(new CreateTransactionRequest("Deposit", "Money", 50));

//            // Assert
//            result.Should().BeOfType<OkObjectResult>();

//            mockAccountService.Verify(accountService => accountService.FindAccountAsync(It.IsAny<UserId>()), Times.Once);

//            mockAccountService.Verify(
//                accountService => accountService.CreateTransactionAsync(
//                    It.IsAny<IAccount>(),
//                    It.IsAny<decimal>(),
//                    It.IsAny<Currency>(),
//                    It.IsAny<TransactionType>(),
//                    It.IsAny<TransactionMetadata>(),
//                    It.IsAny<CancellationToken>()),
//                Times.Once);
//        }
//    }
//}
