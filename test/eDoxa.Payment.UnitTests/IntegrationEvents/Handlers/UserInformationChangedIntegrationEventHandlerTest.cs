// Filename: UserInformationChangedIntegrationEventHandlerTest.cs
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
using Dob = eDoxa.Seedwork.Domain.Miscs.Dob;

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
            var mockAccountService = new Mock<IStripeAccountService>();

            mockAccountService.Setup(accountService => accountService.GetAccountIdAsync(It.IsAny<UserId>())).ReturnsAsync("ConnectAccountId").Verifiable();

            mockAccountService.Setup(
                accountService => accountService.UpdateIndividualAsync(
                    It.IsAny<string>(),
                    It.IsAny<PersonUpdateOptions>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

            var handler = new UserInformationChangedIntegrationEventHandler(mockAccountService.Object);

            var integrationEvent = new UserInformationChangedIntegrationEvent(
                new UserId(),
                "Gabriel", "Roy", Gender.Male, new Dob(2000,1,1));

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
