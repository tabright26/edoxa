// Filename: UserAccountWithdrawalIntegrationEventHandlerTest.cs
// Date Created: 2019-10-06
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Api.IntegrationEvents;
using eDoxa.Payment.Api.IntegrationEvents.Handlers;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Payment.TestHelper;
using eDoxa.Payment.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.TestHelper.Mocks;
using eDoxa.ServiceBus.Abstractions;

using Moq;

using Xunit;

namespace eDoxa.Payment.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserAccountWithdrawalIntegrationEventHandlerTest: UnitTest
    {
        public UserAccountWithdrawalIntegrationEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenUserAccountWithdrawalIntegrationEventIsValid_ShouldBeSuccededTask()
        {
            // Arrange
            var mockLogger = new MockLogger<UserAccountWithdrawalIntegrationEventHandler>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();
            var mockStripeService = new Mock<IStripeTransferService>();

            mockServiceBusPublisher.Setup(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<UserTransactionSuccededIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockStripeService.Setup(
                    stripeService => stripeService.CreateTransferAsync(
                        It.IsAny<string>(),
                        It.IsAny<TransactionId>(),
                        It.IsAny<long>(),
                        It.IsAny<string>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var mockStripeAccountService = new Mock<IStripeAccountService>();

            mockStripeAccountService.Setup(stripeService => stripeService.HasAccountVerifiedAsync(It.IsAny<string>())).ReturnsAsync(true).Verifiable();

            mockStripeAccountService.Setup(customerService => customerService.GetAccountIdAsync(It.IsAny<UserId>())).ReturnsAsync("ConnectAccountId").Verifiable();

            var handler = new UserAccountWithdrawalIntegrationEventHandler(mockLogger.Object, mockServiceBusPublisher.Object, mockStripeService.Object, mockStripeAccountService.Object);

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

            mockServiceBusPublisher.Verify(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<UserTransactionSuccededIntegrationEvent>()), Times.Once);

            mockStripeService.Verify(
                stripeService => stripeService.CreateTransferAsync(
                    It.IsAny<string>(),
                    It.IsAny<TransactionId>(),
                    It.IsAny<long>(),
                    It.IsAny<string>()),
                Times.Once);

            mockStripeAccountService.Verify(stripeService => stripeService.HasAccountVerifiedAsync(It.IsAny<string>()), Times.Once);

            mockStripeAccountService.Verify(customerService => customerService.GetAccountIdAsync(It.IsAny<UserId>()), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_WhenUserAccountWithdrawalIntegrationEventIsValid_ShouldBeFailedTask()
        {
            // Arrange
            var mockLogger = new MockLogger<UserAccountWithdrawalIntegrationEventHandler>();
            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();
            var mockStripeService = new Mock<IStripeTransferService>();

            mockServiceBusPublisher.Setup(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<UserTransactionFailedIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var mockStripeAccountService = new Mock<IStripeAccountService>();

            mockStripeAccountService.Setup(stripeService => stripeService.HasAccountVerifiedAsync(It.IsAny<string>())).ReturnsAsync(false).Verifiable();

            mockStripeAccountService.Setup(customerService => customerService.GetAccountIdAsync(It.IsAny<UserId>())).ReturnsAsync("ConnectAccountId").Verifiable();

            var handler = new UserAccountWithdrawalIntegrationEventHandler(mockLogger.Object, mockServiceBusPublisher.Object, mockStripeService.Object, mockStripeAccountService.Object);

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

            mockServiceBusPublisher.Verify(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<UserTransactionFailedIntegrationEvent>()), Times.Once);

            mockStripeAccountService.Verify(stripeService => stripeService.HasAccountVerifiedAsync(It.IsAny<string>()), Times.Once);

            mockStripeAccountService.Verify(customerService => customerService.GetAccountIdAsync(It.IsAny<UserId>()), Times.Once);
        }
    }
}
