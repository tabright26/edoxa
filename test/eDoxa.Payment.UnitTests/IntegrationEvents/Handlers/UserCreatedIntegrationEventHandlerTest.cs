// Filename: UserCreatedIntegrationEventHandlerTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Enums;
using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Payment.Api.IntegrationEvents.Handlers;
using eDoxa.Payment.Domain.Stripe.Services;
using eDoxa.Payment.TestHelper;
using eDoxa.Payment.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

using Moq;

using Xunit;

namespace eDoxa.Payment.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserCreatedIntegrationEventHandlerTest : UnitTest // GABRIEL: UNIT TESTS.
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
            var mockReferenceService = new Mock<IStripeService>();
            var mockLogger = new MockLogger<UserCreatedIntegrationEventHandler>();

            mockCustomerService.Setup(customerService => customerService.CreateCustomerAsync(It.IsAny<UserId>(), It.IsAny<string>()))
                .ReturnsAsync("CustomerId")
                .Verifiable();

            mockAccountService.Setup(
                    accountService => accountService.CreateAccountAsync(
                        It.IsAny<UserId>(),
                        It.IsAny<string>(),
                        It.IsAny<Country>(),
                        It.IsAny<string>()))
                .ReturnsAsync("AccountId")
                .Verifiable();

            mockReferenceService.Setup(referenceService => referenceService.CreateAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new DomainValidationResult())
                .Verifiable();

            var handler = new UserCreatedIntegrationEventHandler(
                mockCustomerService.Object,
                mockAccountService.Object,
                mockReferenceService.Object,
                mockLogger.Object);

            var integrationEvent = new UserCreatedIntegrationEvent
            {
                UserId = Guid.NewGuid().ToString(),
                Email = new EmailDto
                {
                    Address = "gabriel@edoxa.gg"
                },
                Country = CountryDto.Canada
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockCustomerService.Verify(customerService => customerService.CreateCustomerAsync(It.IsAny<UserId>(), It.IsAny<string>()), Times.Once);

            mockAccountService.Verify(
                accountService => accountService.CreateAccountAsync(
                    It.IsAny<UserId>(),
                    It.IsAny<string>(),
                    It.IsAny<Country>(),
                    It.IsAny<string>()),
                Times.Once);

            mockReferenceService.Verify(
                referenceService => referenceService.CreateAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Once);
        }
    }
}
