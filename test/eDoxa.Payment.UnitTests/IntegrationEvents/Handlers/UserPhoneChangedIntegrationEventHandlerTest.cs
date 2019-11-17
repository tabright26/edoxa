// Filename: UserPhoneChangedIntegrationEventHandlerTest.cs
// Date Created: 2019-10-11
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

using Moq;

using Stripe;

using Xunit;

namespace eDoxa.Payment.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserPhoneChangedIntegrationEventHandlerTest : UnitTest
    {
        public UserPhoneChangedIntegrationEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenUserPhoneChangedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var mockAccountService = new Mock<IStripeAccountService>();

            mockAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>())).ReturnsAsync("ConnectAccountId").Verifiable();

            mockAccountService.Setup(
                accountService => accountService.UpdateIndividualAsync(
                    It.IsAny<string>(),
                    It.IsAny<PersonUpdateOptions>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

            var handler = new UserPhoneChangedIntegrationEventHandler(mockAccountService.Object);

            var integrationEvent = new UserPhoneChangedIntegrationEvent(
                new UserId(),
                "514-420-6969");

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
