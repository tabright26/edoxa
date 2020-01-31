// Filename: UserCreatedIntegrationEventHandlerTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

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
            var mockStripeService = new Mock<IStripeService>();
            var mockLogger = new MockLogger<UserCreatedIntegrationEventHandler>();

            mockStripeService.Setup(stripeService => stripeService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false);

            mockCustomerService.Setup(customerService => customerService.CreateCustomerAsync(It.IsAny<UserId>(), It.IsAny<string>()))
                .ReturnsAsync("CustomerId")
                .Verifiable();

            mockAccountService.Setup(
                    accountService => accountService.CreateAccountAsync(
                        It.IsAny<UserId>(),
                        It.IsAny<string>(),
                        It.IsAny<Country>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<int>()))
                .ReturnsAsync("AccountId")
                .Verifiable();

            mockStripeService.Setup(referenceService => referenceService.CreateAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new DomainValidationResult())
                .Verifiable();

            var handler = new UserCreatedIntegrationEventHandler(
                mockCustomerService.Object,
                mockAccountService.Object,
                mockStripeService.Object,
                mockLogger.Object);

            var integrationEvent = new UserCreatedIntegrationEvent
            {
                UserId = new UserId(),
                Email = new EmailDto
                {
                    Address = "gabriel@edoxa.gg"
                },
                Country = EnumCountryIsoCode.CA,
                Ip = "10.10.10.10",
                Dob = new DobDto
                {
                    Day = 1,
                    Month = 1,
                    Year = 2000
                }
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockStripeService.Verify(stripeService => stripeService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockCustomerService.Verify(customerService => customerService.CreateCustomerAsync(It.IsAny<UserId>(), It.IsAny<string>()), Times.Once);

            mockAccountService.Verify(
                accountService => accountService.CreateAccountAsync(
                    It.IsAny<UserId>(),
                    It.IsAny<string>(),
                    It.IsAny<Country>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()),
                Times.Once);

            mockStripeService.Verify(referenceService => referenceService.CreateAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            mockLogger.Verify(Times.Once());
        }
    }
}
