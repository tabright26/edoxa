// Filename: UserEmailConfirmationTokenGeneratedIntegrationEventHandlerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Notifications.Api.IntegrationEvents.Handlers;
using eDoxa.Notifications.TestHelper;
using eDoxa.Notifications.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

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
            TestMock.UserService.Setup(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            TestMock.RedirectService.Setup(redirectService => redirectService.RedirectToWebSpa(It.IsAny<string>())).Returns("testUrl").Verifiable();

            var handler = new UserEmailConfirmationTokenGeneratedIntegrationEventHandler(TestMock.UserService.Object, TestMock.RedirectService.Object);

            var integrationEvent = new UserEmailConfirmationTokenGeneratedIntegrationEvent
            {
                Code = "testCode",
                UserId = new UserId()
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            TestMock.UserService.Verify(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);
            TestMock.RedirectService.Verify(redirectService => redirectService.RedirectToWebSpa(It.IsAny<string>()), Times.Once);
        }
    }
}
