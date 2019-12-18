// Filename: UserPhoneChangedIntegrationEventHandlerTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Payment.Api.IntegrationEvents.Handlers;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Payment.TestHelper;
using eDoxa.Payment.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

using Moq;

using Stripe;

using Xunit;

namespace eDoxa.Payment.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserPhoneChangedIntegrationEventHandlerTest : UnitTest // GABRIEL: UNIT TESTS.
    {
        public UserPhoneChangedIntegrationEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenUserPhoneChangedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var mockStripeService = new Mock<IStripeService>();
            var mockAccountService = new Mock<IStripeAccountService>();
            var mockLogger = new MockLogger<UserPhoneChangedIntegrationEventHandler>();

            mockStripeService.Setup(stripeService => stripeService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true);

            mockAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>())).ReturnsAsync("ConnectAccountId").Verifiable();

            mockAccountService.Setup(accountService => accountService.UpdateIndividualAsync(It.IsAny<string>(), It.IsAny<PersonUpdateOptions>()))
                .ReturnsAsync(new DomainValidationResult())
                .Verifiable();

            var handler = new UserPhoneChangedIntegrationEventHandler(mockStripeService.Object, mockAccountService.Object, mockLogger.Object);

            var integrationEvent = new UserPhoneChangedIntegrationEvent
            {
                UserId = Guid.NewGuid().ToString(),
                Phone = new PhoneDto
                {
                    Number = "514-420-6969"
                }
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockAccountService.Verify(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()), Times.Once);
            mockAccountService.Verify(accountService => accountService.UpdateIndividualAsync(It.IsAny<string>(), It.IsAny<PersonUpdateOptions>()), Times.Once);
        }
    }
}
