// Filename: UserCreatedIntegrationEventHandlerTest.cs
// Date Created: 2019-10-11
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Payment.Api.IntegrationEvents.Handlers;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Payment.TestHelper;
using eDoxa.Payment.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using Moq;

using Xunit;

namespace eDoxa.Payment.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserCreatedIntegrationEventHandlerTest : UnitTest
    {
        public UserCreatedIntegrationEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenUserCreatedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var mockCustomerService = new Mock<IStripeCustomerService>();
            var mockAccountService = new Mock<IStripeAccountService>();
            var mockReferenceService = new Mock<IStripeReferenceService>();

            mockCustomerService.Setup(customerService => customerService.CreateCustomerAsync(It.IsAny<UserId>(), It.IsAny<string>())).ReturnsAsync("CustomerId").Verifiable();

            mockAccountService.Setup(
                accountService => accountService.CreateAccountAsync(
                    It.IsAny<UserId>(),
                    It.IsAny<string>(),
                    It.IsAny<Country>(),
                    It.IsAny<string>()))
            .ReturnsAsync("AccountId")
            .Verifiable();

            mockReferenceService.Setup(referenceService => referenceService.CreateReferenceAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.CompletedTask).Verifiable();

            var handler = new UserCreatedIntegrationEventHandler(mockCustomerService.Object, mockAccountService.Object, mockReferenceService.Object);

            var integrationEvent = new UserCreatedIntegrationEvent {
                UserId = Guid.NewGuid().ToString(),
                Email = "gabriel@edoxa.gg",
                Country = Grpc.Protos.Identity.Enums.CountryDto.Canada
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockCustomerService.Verify(customerService => customerService.CreateCustomerAsync(It.IsAny<UserId>(), It.IsAny<string>()), Times.Once);
            mockAccountService.Verify(accountService => accountService.CreateAccountAsync(
                    It.IsAny<UserId>(),
                    It.IsAny<string>(),
                    It.IsAny<Country>(),
                    It.IsAny<string>()), Times.Once);
            mockReferenceService.Verify(referenceService => referenceService.CreateReferenceAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
