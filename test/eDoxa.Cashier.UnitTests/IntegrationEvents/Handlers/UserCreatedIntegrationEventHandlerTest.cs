// Filename: UserCreatedIntegrationEventHandlerTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.IntegrationEvents;
using eDoxa.Cashier.Api.IntegrationEvents.Handlers;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Seedwork.Domain.Misc;

using Moq;

using Xunit;

using Country = eDoxa.Grpc.Protos.Identity.Enums.Country;

namespace eDoxa.Cashier.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserCreatedIntegrationEventHandlerTest
    {
        [Fact]
        public async Task HandleAsync_WhenUserCreatedIntegrationEvent_ShouldBeCompletedTask()
        {
            // Arrange
            var mockAccountService = new Mock<IAccountService>();

            mockAccountService.Setup(accountRepository => accountRepository.CreateAccountAsync(It.IsAny<UserId>())).Verifiable();

            var handler = new UserCreatedIntegrationEventHandler(mockAccountService.Object);

            var integrationEvent = new UserCreatedIntegrationEvent
            {
                UserId = Guid.NewGuid().ToString(),
                Email = "noreply@edoxa.gg",
                Country = Country.Canada
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockAccountService.Verify(accountRepository => accountRepository.CreateAccountAsync(It.IsAny<UserId>()), Times.Once);
        }
    }
}
