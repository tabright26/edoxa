// Filename: UserAccountDepositIntegrationEventHandlerTest.cs
// Date Created: 2019-10-06
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Api.IntegrationEvents;
using eDoxa.Payment.Api.IntegrationEvents.Handlers;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Payment.TestHelpers;
using eDoxa.Payment.TestHelpers.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.Testing.Mocks;
using eDoxa.ServiceBus.Abstractions;

using Moq;

using Xunit;

namespace eDoxa.Payment.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserAccountDepositIntegrationEventHandlerTest: UnitTest
    {
        public UserAccountDepositIntegrationEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenUserAccountDepositIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var mockLogger = new MockLogger<UserAccountDepositIntegrationEventHandler>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();
            var mockStripeService = new Mock<IStripeInvoiceService>();

            mockServiceBusPublisher.Setup(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<IIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockStripeService.Setup(
                    stripeService => stripeService.CreateInvoiceAsync(
                        It.IsAny<string>(),
                        It.IsAny<TransactionId>(),
                        It.IsAny<long>(),
                        It.IsAny<string>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var mockCustomerService = new Mock<IStripeCustomerService>();

            mockCustomerService.Setup(customerService => customerService.HasDefaultPaymentMethodAsync(It.IsAny<string>())).ReturnsAsync(true).Verifiable();

            mockCustomerService.Setup(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>())).ReturnsAsync("CustomerId").Verifiable();

            var handler = new UserAccountDepositIntegrationEventHandler(mockLogger.Object, mockServiceBusPublisher.Object, mockStripeService.Object, mockCustomerService.Object);

            var integrationEvent = new UserAccountDepositIntegrationEvent(
                new UserId(),
                "noreply@edoxa.gg",
                new TransactionId(),
                "TransactionDescription",
                100);

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockLogger.Verify(Times.Exactly(3));

            mockServiceBusPublisher.Verify(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<IIntegrationEvent>()), Times.Once);

            mockStripeService.Verify(
                stripeService => stripeService.CreateInvoiceAsync(
                    It.IsAny<string>(),
                    It.IsAny<TransactionId>(),
                    It.IsAny<long>(),
                    It.IsAny<string>()),
                Times.Once);

            mockCustomerService.Verify(customerService => customerService.HasDefaultPaymentMethodAsync(It.IsAny<string>()), Times.Once);

            mockCustomerService.Verify(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>()), Times.Once);
        }
    }
}
