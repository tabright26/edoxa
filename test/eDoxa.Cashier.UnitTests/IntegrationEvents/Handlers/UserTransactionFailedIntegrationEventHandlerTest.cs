// Filename: UserTransactionFailedIntegrationEventHandlerTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.IntegrationEvents.Handlers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Grpc.Protos.Cashier.IntegrationEvents;
using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;
using eDoxa.ServiceBus.Abstractions;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserTransactionFailedIntegrationEventHandlerTest
    {
        [Fact]
        public async Task HandleAsync_WhenUserTransactionFailedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var account = new Account(new UserId());
            var moneyAccount = new MoneyAccountDecorator(account);
            var transaction = moneyAccount.Deposit(Money.Ten);

            var mockAccountService = new Mock<IAccountService>();

            mockAccountService.Setup(accountService => accountService.FindAccountAsync(It.IsAny<UserId>()))
                .ReturnsAsync(moneyAccount)
                .Verifiable();

            var result = new DomainValidationResult();

            result.AddEntityToMetadata(transaction);

            mockAccountService.Setup(accountService => accountService.MarkAccountTransactionAsFailedAsync(It.IsAny<IAccount>(), It.IsAny<TransactionId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result)
                .Verifiable();

            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            var mockLogger = new MockLogger<UserTransactionFailedIntegrationEventHandler>();

            mockServiceBusPublisher.Setup(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<UserEmailSentIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new UserTransactionFailedIntegrationEventHandler(mockAccountService.Object, mockServiceBusPublisher.Object, mockLogger.Object);

            var integrationEvent = new UserTransactionFailedIntegrationEvent
            {
                UserId = new UserId(),
                TransactionId = transaction.Id
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockAccountService.Verify(transcationRepository => transcationRepository.FindAccountAsync(It.IsAny<UserId>()), Times.Once);

            mockAccountService.Verify(accountService => accountService.MarkAccountTransactionAsFailedAsync(It.IsAny<IAccount>(), It.IsAny<TransactionId>(), It.IsAny<CancellationToken>()), Times.Once);

            mockServiceBusPublisher.Verify(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<UserEmailSentIntegrationEvent>()), Times.Once);

            mockLogger.Verify(Times.Never());
        }
    }
}
