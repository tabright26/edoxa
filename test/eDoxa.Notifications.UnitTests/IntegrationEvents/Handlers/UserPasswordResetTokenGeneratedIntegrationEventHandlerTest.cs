// Filename: UserPasswordResetTokenGeneratedIntegrationEventHandlerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Notifications.Api.IntegrationEvents.Handlers;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Notifications.TestHelper;
using eDoxa.Notifications.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using Moq;

using Xunit;

namespace eDoxa.Notifications.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserPasswordResetTokenGeneratedIntegrationEventHandlerTest : UnitTest // GABRIEL: UNIT TESTS
    {
        public UserPasswordResetTokenGeneratedIntegrationEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact(Skip = "Must be updated.")]
        public async Task HandleAsync_WhenUserPasswordResetTokenGeneratedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            var mockRedirect = new Mock<IRedirectService>();

            mockUserService.Setup(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            mockRedirect.Setup(redirectService => redirectService.RedirectToWebSpa(It.IsAny<string>())).Returns("testUrl").Verifiable();

            var handler = new UserPasswordResetTokenGeneratedIntegrationEventHandler(mockUserService.Object, mockRedirect.Object);

            var integrationEvent = new UserPasswordResetTokenGeneratedIntegrationEvent
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
