// Filename: UserAccountDepositIntegrationEventHandlerTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Payment.Api.IntegrationEvents;
using eDoxa.Payment.Api.IntegrationEvents.Handlers;
using eDoxa.Payment.Api.Providers.Stripe.Abstractions;
using eDoxa.Seedwork.Testing.Mocks;
using eDoxa.ServiceBus.Abstractions;

using Moq;

using Xunit;

namespace eDoxa.Payment.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserAccountDepositIntegrationEventHandlerTest
    {
        [Fact]
        public async Task HandleAsync_WhenUserAccountDepositIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var mockLogger = new MockLogger<UserAccountDepositIntegrationEventHandler>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();
            var mockStripeService = new Mock<IStripeService>();

            mockServiceBusPublisher.Setup(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<IIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockStripeService.Setup(
                    stripeService => stripeService.CreateInvoiceAsync(
                        It.IsAny<Guid>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<long>(),
                        It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new UserAccountDepositIntegrationEventHandler(mockLogger.Object, mockServiceBusPublisher.Object, mockStripeService.Object);

            var integrationEvent = new UserAccountDepositIntegrationEvent(
                Guid.NewGuid(),
                "test",
                "user",
                100);

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockLogger.Verify(Times.Exactly(3));

            mockServiceBusPublisher.Verify(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<IIntegrationEvent>()), Times.Once);

            mockStripeService.Verify(
                stripeService => stripeService.CreateInvoiceAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<long>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
