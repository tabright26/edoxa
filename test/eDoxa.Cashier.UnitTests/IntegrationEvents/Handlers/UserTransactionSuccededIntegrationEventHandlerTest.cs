// Filename: UserCreatedIntegrationEventHandlerTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.IntegrationEvents;
using eDoxa.Cashier.Api.IntegrationEvents.Handlers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Domain;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents.Handlers
{
    [TestClass]
    public sealed class UserTransactionSuccededIntegrationEventHandlerTest
    {
        [TestMethod]
        public async Task UserTransactionSuccededIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var mockTransactionRepository = new Mock<ITransactionRepository>();

            mockTransactionRepository
                .Setup(transcationRepository => transcationRepository.FindTransactionAsync(It.IsAny<TransactionId>()))
                .ReturnsAsync(new Transaction(Money.Ten, new TransactionDescription("Description"), TransactionType.Deposit, new UtcNowDateTimeProvider()))
                .Verifiable();

            mockTransactionRepository.Setup(transactionRepository =>
                    transactionRepository.CommitAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new UserTransactionSuccededIntegrationEventHandler(mockTransactionRepository.Object);

            var integrationEvent = new UserTransactionSuccededIntegrationEvent(Guid.NewGuid());

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockTransactionRepository.Verify(transcationRepository => transcationRepository.FindTransactionAsync(It.IsAny<TransactionId>()), Times.Once);

            mockTransactionRepository.Verify(transactionRepository => transactionRepository.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
