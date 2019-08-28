// Filename: UserCreatedIntegrationEventHandlerTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.IntegrationEvents;
using eDoxa.Cashier.Api.IntegrationEvents.Handlers;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents.Handlers
{
    [TestClass]
    public sealed class UserCreatedIntegrationEventHandlerTest
    {
        [TestMethod]
        public async Task HandleAsync_WhenUserCreatedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();

            mockAccountRepository.Setup(accountRepository => accountRepository.Create(It.IsAny<IAccount>())).Verifiable();

            var handler = new UserCreatedIntegrationEventHandler(mockAccountRepository.Object);

            var integrationEvent = new UserCreatedIntegrationEvent(Guid.NewGuid());

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockAccountRepository.Verify(accountRepository => accountRepository.Create(It.IsAny<IAccount>()), Times.Once);
        }
    }
}
