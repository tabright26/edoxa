// Filename: UserCreatedIntegrationEventHandlerTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Payment.Api.IntegrationEvents;
using eDoxa.Payment.Api.IntegrationEvents.Handlers;
using eDoxa.Payment.Api.Providers.Stripe.Abstractions;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Testing.Mocks;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents.Handlers
{
    [TestClass]
    public sealed class UserAccountDepositIntegrationEventHandlerTest
    {
        [TestMethod]
        public async Task UserAccountDepositIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var mockLogger = new MockLogger<UserAccountDepositIntegrationEventHandler>();
            var mockServiceBus = new Mock<IServiceBusPublisher>();
            var mockStripeService = new Mock<IStripeService>();

            mockServiceBus
                .Setup(serviceBus => serviceBus.PublishAsync(It.IsAny<IIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockStripeService.Setup(stripeService =>
                    stripeService.CreateInvoiceAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new UserAccountDepositIntegrationEventHandler(mockLogger.Object, mockServiceBus.Object, mockStripeService.Object);

            var integrationEvent = new UserAccountDepositIntegrationEvent(Guid.NewGuid(), "test", "user", 100);

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockLogger.Verify(Times.Exactly(3));

            mockServiceBus.Verify(serviceBus => serviceBus.PublishAsync(It.IsAny<IIntegrationEvent>()), Times.Once);

            mockStripeService.Verify(stripeService =>
                stripeService.CreateInvoiceAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<long>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
