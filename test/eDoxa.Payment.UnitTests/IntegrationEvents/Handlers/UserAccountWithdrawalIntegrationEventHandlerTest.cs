// Filename: UserAccountWithdrawalIntegrationEventHandlerTest.cs
// Date Created: 2019-08-25
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

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Payment.UnitTests.IntegrationEvents.Handlers
{
    [TestClass]
    public sealed class UserAccountWithdrawalIntegrationEventHandlerTest
    {
        [TestMethod]
        public async Task HandleAsync_WhenUserAccountDepositIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var mockLogger = new MockLogger<UserAccountWithdrawalIntegrationEventHandler>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();
            var mockStripeService = new Mock<IStripeService>();

            mockServiceBusPublisher.Setup(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<IIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockStripeService
                .Setup(
                    stripeService => stripeService.CreateTransferAsync(
                        It.IsAny<Guid>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<long>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new UserAccountWithdrawalIntegrationEventHandler(mockLogger.Object, mockServiceBusPublisher.Object, mockStripeService.Object);

            var integrationEvent = new UserAccountWithdrawalIntegrationEvent(Guid.NewGuid(), "test", "user", 100);

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockLogger.Verify(Times.Exactly(3));

            mockServiceBusPublisher.Verify(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<IIntegrationEvent>()), Times.Once);

            mockStripeService.Verify(
                stripeService => stripeService.CreateTransferAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<long>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
        }
    }
}
