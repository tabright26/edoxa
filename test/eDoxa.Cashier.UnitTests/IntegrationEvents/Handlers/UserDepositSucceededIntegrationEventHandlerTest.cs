// Filename: UserDepositSucceededIntegrationEventHandlerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.IntegrationEvents.Handlers;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Payment.IntegrationEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserDepositSucceededIntegrationEventHandlerTest : UnitTest
    {
        public UserDepositSucceededIntegrationEventHandlerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenUserDepositSucceededIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var userId = new UserId();
            var account = new Account(userId, new List<Transaction>());

            var mockLogger = new MockLogger<UserStripePaymentIntentSucceededIntegrationEventHandler>();

            TestMock.AccountService.Setup(accountRepository => accountRepository.AccountExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            TestMock.AccountService.Setup(accountRepository => accountRepository.FindAccountAsync(It.IsAny<UserId>())).ReturnsAsync(account).Verifiable();

            TestMock.AccountService
                .Setup(
                    accountService => accountService.MarkAccountTransactionAsSucceededAsync(
                        It.IsAny<IAccount>(),
                        It.IsAny<TransactionId>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DomainValidationResult<ITransaction>())
                .Verifiable();

            var handler = new UserStripePaymentIntentSucceededIntegrationEventHandler(TestMock.AccountService.Object, TestMapper, TestMock.ServiceBusPublisher.Object,  mockLogger.Object);

            var integrationEvent = new UserStripePaymentIntentSucceededIntegrationEvent
            {
                UserId = userId,
                TransactionId = new TransactionId()
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            TestMock.AccountService.Verify(accountRepository => accountRepository.AccountExistsAsync(It.IsAny<UserId>()), Times.Once);
            TestMock.AccountService.Verify(accountRepository => accountRepository.FindAccountAsync(It.IsAny<UserId>()), Times.Once);

            TestMock.AccountService.Verify(
                accountService =>
                    accountService.MarkAccountTransactionAsSucceededAsync(It.IsAny<IAccount>(), It.IsAny<TransactionId>(), It.IsAny<CancellationToken>()),
                Times.Once);

            mockLogger.Verify(Times.Once());
        }
    }
}
