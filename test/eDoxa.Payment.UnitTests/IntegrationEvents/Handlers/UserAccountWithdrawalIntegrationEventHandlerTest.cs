// Filename: UserAccountWithdrawalIntegrationEventHandlerTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Api.IntegrationEvents;
using eDoxa.Payment.Api.IntegrationEvents.Handlers;
using eDoxa.Payment.Domain.Models;
using eDoxa.Payment.Domain.Services;
using eDoxa.Seedwork.Testing.Mocks;
using eDoxa.ServiceBus.Abstractions;

using Moq;

using Xunit;

namespace eDoxa.Payment.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserAccountWithdrawalIntegrationEventHandlerTest
    {
        [Fact]
        public async Task HandleAsync_WhenUserAccountDepositIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var mockLogger = new MockLogger<UserAccountWithdrawalIntegrationEventHandler>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();
            var mockStripeService = new Mock<IStripeTransferService>();

            mockServiceBusPublisher.Setup(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<IIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockStripeService.Setup(
                    stripeService => stripeService.CreateTransferAsync(
                        It.IsAny<TransactionId>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<long>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var mockConnectAccountService = new Mock<IStripeAccountService>();

            mockConnectAccountService.Setup(customerService => customerService.GetAccountIdAsync(It.IsAny<UserId>())).ReturnsAsync("ConnectAccountId").Verifiable();

            var handler = new UserAccountWithdrawalIntegrationEventHandler(mockLogger.Object, mockServiceBusPublisher.Object, mockStripeService.Object, mockConnectAccountService.Object);

            var integrationEvent = new UserAccountWithdrawalIntegrationEvent(
                new UserId(),
                "noreply@edoxa.gg",
                new TransactionId(),
                "test",
                100);

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockLogger.Verify(Times.Exactly(3));

            mockServiceBusPublisher.Verify(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<IIntegrationEvent>()), Times.Once);

            mockStripeService.Verify(
                stripeService => stripeService.CreateTransferAsync(
                    It.IsAny<TransactionId>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<long>()),
                Times.Once);

            mockConnectAccountService.Verify(customerService => customerService.GetAccountIdAsync(It.IsAny<UserId>()), Times.Once);
        }
    }
}
