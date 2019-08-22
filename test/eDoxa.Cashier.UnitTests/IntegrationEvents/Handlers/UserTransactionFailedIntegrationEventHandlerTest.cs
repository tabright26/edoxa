// Filename: UserCreatedIntegrationEventHandlerTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Requests;
using eDoxa.Cashier.Api.IntegrationEvents;
using eDoxa.Cashier.Api.IntegrationEvents.Handlers;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;

using MediatR;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents.Handlers
{
    [TestClass]
    public sealed class UserTransactionFailedIntegrationEventHandlerTest
    {
        [TestMethod]
        public async Task UserTransactionFailedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var mockRepository = new Mock<ITransactionRepository>();

            mockRepository.Setup(transaction => transaction);

            var handler = new UserTransactionFailedIntegrationEventHandler(mockRepository.Object);

            var integrationEvent = new UserTransactionFailedIntegrationEvent(Guid.NewGuid());

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockRepository.Verify(transaction => transaction);
        }
    }
}
