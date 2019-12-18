// Filename: UserAddressChangedIntegrationEventHandlerTEst.cs
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
    public sealed class UserAddressChangedIntegrationEventHandlerTest : UnitTest // GABRIEL: UNIT TESTS.
    {
        public UserAddressChangedIntegrationEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenUserAddressChangedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var mockService = new Mock<IStripeService>();
            var mockAccountService = new Mock<IStripeAccountService>();
            var mockLogger = new MockLogger<UserAddressChangedIntegrationEventHandler>();

            mockAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>())).ReturnsAsync("ConnectAccountId").Verifiable();

            mockAccountService.Setup(accountService => accountService.UpdateIndividualAsync(It.IsAny<string>(), It.IsAny<PersonUpdateOptions>()))
                .ReturnsAsync(new DomainValidationResult())
                .Verifiable();

            var handler = new UserAddressChangedIntegrationEventHandler(mockService.Object, mockAccountService.Object, mockLogger.Object);

            var integrationEvent = new UserAddressChangedIntegrationEvent
            {
                UserId = Guid.NewGuid().ToString(),
                Address = new AddressDto
                {
                    Line1 = "This is address",
                    City = "Montreal",
                    PostalCode = "A1B2C3"
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
