// Filename: UserDepositFailedIntegrationEventHandlerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.IntegrationEvents.Handlers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Payment.IntegrationEvents;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserDepositFailedIntegrationEventHandlerTest : UnitTest
    {
        public UserDepositFailedIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenUserDepositFailedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var userId = new UserId();

            var account = new MoneyAccountDecorator(new Account(userId, new List<Transaction>()));

            var transaction = account.Deposit(Money.OneHundred);

            transaction.MarkAsFailed();

            var mockLogger = new MockLogger<UserStripePaymentIntentPaymentFailedIntegrationEventHandler>();

            TestMock.AccountService.Setup(accountRepository => accountRepository.AccountExistsAsync(userId)).ReturnsAsync(true).Verifiable();

            TestMock.AccountService.Setup(accountRepository => accountRepository.FindAccountAsync(userId)).ReturnsAsync(account).Verifiable();

            TestMock.AccountService
                .Setup(accountService => accountService.MarkAccountTransactionAsFailedAsync(account, transaction.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(account.Transactions.First().Cast<Transaction>())
                .Verifiable();

            var handler = new UserStripePaymentIntentPaymentFailedIntegrationEventHandler(
                TestMock.AccountService.Object,
                TestMapper,
                TestMock.ServiceBusPublisher.Object,
                mockLogger.Object);

            var integrationEvent = new UserStripePaymentIntentPaymentFailedIntegrationEvent
            {
                UserId = userId,
                TransactionId = transaction.Id
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            TestMock.AccountService.Verify(accountRepository => accountRepository.AccountExistsAsync(userId), Times.Once);
            TestMock.AccountService.Verify(accountRepository => accountRepository.FindAccountAsync(userId), Times.Once);

            TestMock.AccountService.Verify(
                accountService => accountService.MarkAccountTransactionAsFailedAsync(account, transaction.Id, It.IsAny<CancellationToken>()),
                Times.Once);

            mockLogger.Verify(Times.Once());
        }
    }
}
