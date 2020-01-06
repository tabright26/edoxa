// Filename: UserEmailConfirmationTokenGeneratedIntegrationEventHandlerTest.cs
// Date Created: 2019-12-17
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Notifications.Api.IntegrationEvents.Handlers;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Notifications.TestHelper;
using eDoxa.Notifications.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

using Microsoft.AspNetCore.Authentication.OpenIdConnect;

using Moq;

using Xunit;

namespace eDoxa.Notifications.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserEmailConfirmationTokenGeneratedIntegrationEventHandlerTest : UnitTest
    {
        public UserEmailConfirmationTokenGeneratedIntegrationEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenUserEmailConfirmationTokenGeneratedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockRedirect = new Mock<IRedirectService>();


            mockUserService.Setup(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockRedirect.Setup(redirectService => redirectService.RedirectToWebSpa(It.IsAny<string>()))
                .Returns("testUrl")
                .Verifiable();

            var handler = new UserEmailConfirmationTokenGeneratedIntegrationEventHandler(mockUserService.Object, mockRedirect.Object);

            var integrationEvent = new UserEmailConfirmationTokenGeneratedIntegrationEvent
            {
                Code = "testCode",
                UserId = new UserId()
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockUserService.Verify(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            mockRedirect.Verify(redirectService => redirectService.RedirectToWebSpa(It.IsAny<string>()), Times.Once);
        }
    }
}
