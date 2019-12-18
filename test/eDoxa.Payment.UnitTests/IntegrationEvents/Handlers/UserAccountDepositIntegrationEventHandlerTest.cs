//// Filename: UserAccountDepositIntegrationEventHandlerTest.cs
//// Date Created: 2019-10-06
////
//// ================================================
//// Copyright © 2019, eDoxa. All rights reserved.

//using System;
//using System.Threading.Tasks;

//using eDoxa.Grpc.Protos.Cashier.IntegrationEvents;
//using eDoxa.Grpc.Protos.Payment.IntegrationEvents;
//using eDoxa.Payment.Api.IntegrationEvents.Handlers;
//using eDoxa.Payment.Domain.Stripe.Services;
//using eDoxa.Payment.TestHelper;
//using eDoxa.Payment.TestHelper.Fixtures;
//using eDoxa.Seedwork.Domain.Misc;
//using eDoxa.Seedwork.TestHelper.Mocks;
//using eDoxa.ServiceBus.Abstractions;

//using Moq;

//using Stripe;

//using Xunit;

//namespace eDoxa.Payment.UnitTests.IntegrationEvents.Handlers
//{
//    public sealed class UserAccountDepositIntegrationEventHandlerTest: UnitTest
//    {
//        public UserAccountDepositIntegrationEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
//        {
//        }

//        [Fact]
//        public async Task HandleAsync_WhenUserAccountDepositIntegrationEventIsValid_ShouldBeSuccededTask()
//        {
//            // Arrange
//            var mockLogger = new MockLogger<UserAccountDepositIntegrationEventHandler>();
//            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();
//            var mockStripeService = new Mock<IStripeInvoiceService>();

//            mockServiceBusPublisher.Setup(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<UserTransactionSuccededIntegrationEvent>()))
//                .Returns(Task.CompletedTask)
//                .Verifiable();

//            mockStripeService.Setup(
//                    stripeService => stripeService.CreateInvoiceAsync(
//                        It.IsAny<string>(),
//                        It.IsAny<TransactionId>(),
//                        It.IsAny<long>(),
//                        It.IsAny<string>()))
//                .ReturnsAsync(new Invoice())
//                .Verifiable();

//            var mockCustomerService = new Mock<IStripeCustomerService>();

//            mockCustomerService.Setup(customerService => customerService.HasDefaultPaymentMethodAsync(It.IsAny<string>())).ReturnsAsync(true).Verifiable();

//            mockCustomerService.Setup(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>())).ReturnsAsync("CustomerId").Verifiable();

//            var handler = new UserAccountDepositIntegrationEventHandler(mockLogger.Object, mockServiceBusPublisher.Object, mockStripeService.Object, mockCustomerService.Object);

//            var integrationEvent = new UserAccountDepositIntegrationEvent
//            {
//                UserId = Guid.NewGuid().ToString(),
//                Email = "noreply@edoxa.gg",
//                TransactionId = Guid.NewGuid().ToString(),
//                Description = "TransactionDescription",
//                Amount = 100
//            };

//            // Act
//            await handler.HandleAsync(integrationEvent);

//            // Assert
//            mockLogger.Verify(Times.Exactly(3));

//            mockServiceBusPublisher.Verify(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<UserTransactionSuccededIntegrationEvent>()), Times.Once);

//            mockStripeService.Verify(
//                stripeService => stripeService.CreateInvoiceAsync(
//                    It.IsAny<string>(),
//                    It.IsAny<TransactionId>(),
//                    It.IsAny<long>(),
//                    It.IsAny<string>()),
//                Times.Once);

//            mockCustomerService.Verify(customerService => customerService.HasDefaultPaymentMethodAsync(It.IsAny<string>()), Times.Once);

//            mockCustomerService.Verify(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>()), Times.Once);
//        }

//        [Fact]
//        public async Task HandleAsync_WhenUserAccountDepositIntegrationEventIsValid_ShouldBeFailedTask()
//        {
//            // Arrange
//            var mockLogger = new MockLogger<UserAccountDepositIntegrationEventHandler>();
//            var mockServiceBusPublisher = new Mock<IServiceBusPublisher>();
//            var mockStripeService = new Mock<IStripeInvoiceService>();

//            mockServiceBusPublisher.Setup(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<UserTransactionFailedIntegrationEvent>()))
//                .Returns(Task.CompletedTask)
//                .Verifiable();

//            var mockCustomerService = new Mock<IStripeCustomerService>();

//            mockCustomerService.Setup(customerService => customerService.HasDefaultPaymentMethodAsync(It.IsAny<string>())).ReturnsAsync(false).Verifiable();

//            mockCustomerService.Setup(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>())).ReturnsAsync("CustomerId").Verifiable();

//            var handler = new UserAccountDepositIntegrationEventHandler(mockLogger.Object, mockServiceBusPublisher.Object, mockStripeService.Object, mockCustomerService.Object);

//            var integrationEvent = new UserAccountDepositIntegrationEvent
//            {
//                UserId = Guid.NewGuid().ToString(),
//                Email = "noreply@edoxa.gg",
//                TransactionId = Guid.NewGuid().ToString(),
//                Description = "TransactionDescription",
//                Amount = 100
//            };

//            // Act
//            await handler.HandleAsync(integrationEvent);

//            // Assert
//            mockLogger.Verify(Times.Exactly(3));

//            mockServiceBusPublisher.Verify(serviceBusPublisher => serviceBusPublisher.PublishAsync(It.IsAny<UserTransactionFailedIntegrationEvent>()), Times.Once);


//            mockCustomerService.Verify(customerService => customerService.HasDefaultPaymentMethodAsync(It.IsAny<string>()), Times.Once);

//            mockCustomerService.Verify(customerService => customerService.GetCustomerIdAsync(It.IsAny<UserId>()), Times.Once);
//        }
//    }
//}
