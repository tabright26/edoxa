// Filename: UserTransactionSuccededIntegrationEventHandlerTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.IntegrationEvents;
using eDoxa.Cashier.Api.IntegrationEvents.Handlers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.TestHelper.Mocks;
using eDoxa.ServiceBus.Abstractions;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserTransactionSuccededIntegrationEventHandlerTest
    {
        [Fact]
        public async Task HandleAsync_WhenUserTransactionSuccededIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var transaction = new Transaction(     new TransactionId(), 
                Money.Ten,
                new TransactionDescription("Description"),
                TransactionType.Deposit,
                new UtcNowDateTimeProvider());

            var mockTransactionRepository = new Mock<ITransactionRepository>();

            mockTransactionRepository.Setup(transcationRepository => transcationRepository.FindTransactionAsync(It.IsAny<TransactionId>()))
                .ReturnsAsync(transaction)
                .Verifiable();

            mockTransactionRepository.Setup(transactionRepository => transactionRepository.CommitAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();

            mockServiceBusPublisher.Setup(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<UserEmailSentIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var mockLogger = new MockLogger<UserTransactionSuccededIntegrationEventHandler>();

            var handler = new UserTransactionSuccededIntegrationEventHandler(mockTransactionRepository.Object, mockServiceBusPublisher.Object, mockLogger.Object);

            var integrationEvent = new UserTransactionSuccededIntegrationEvent(new UserId(), transaction.Id);

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockTransactionRepository.Verify(transcationRepository => transcationRepository.FindTransactionAsync(It.IsAny<TransactionId>()), Times.Once);

            mockTransactionRepository.Verify(transactionRepository => transactionRepository.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);

            mockServiceBusPublisher.Verify(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<UserEmailSentIntegrationEvent>()), Times.Once);

            mockLogger.Verify(Times.Never());
        }
    }
}
