// Filename: UserInformationChangedIntegrationEventHandlerTest.cs
// Date Created: 2019-10-11
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
    public sealed class UserInformationChangedIntegrationEventHandlerTest : UnitTest
    {
        public UserInformationChangedIntegrationEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenUserInformationChangedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var mockService = new Mock<IStripeService>();
            var mockAccountService = new Mock<IStripeAccountService>();
            var mockLogger = new MockLogger<UserProfileChangedIntegrationEventHandler>();

            mockAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>())).ReturnsAsync("ConnectAccountId").Verifiable();

            mockAccountService.Setup(
                accountService => accountService.UpdateIndividualAsync(
                    It.IsAny<string>(),
                    It.IsAny<PersonUpdateOptions>()))
                .ReturnsAsync(new DomainValidationResult())
            .Verifiable();

            var handler = new UserProfileChangedIntegrationEventHandler(mockService.Object, mockAccountService.Object, mockLogger.Object);

            var integrationEvent = new UserProfileChangedIntegrationEvent
            {
                UserId = Guid.NewGuid().ToString(),
                Profile = new ProfileDto
                {
                    FirstName = "Gabriel",
                    LastName = "Roy",
                    Gender = Grpc.Protos.Identity.Enums.GenderDto.Male,
                    Dob = new DobDto
                    {
                        Day = 1,
                        Month = 2,
                        Year = 2000
                    }
                }
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockAccountService.Verify(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>()), Times.Once);
            mockAccountService.Verify(accountService => accountService.UpdateIndividualAsync(
                    It.IsAny<string>(),
                    It.IsAny<PersonUpdateOptions>()), Times.Once);
        }
    }
}
