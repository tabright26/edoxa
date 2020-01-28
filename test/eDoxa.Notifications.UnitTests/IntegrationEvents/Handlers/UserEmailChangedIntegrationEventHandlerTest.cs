// Filename: UserEmailChangedIntegrationEventHandlerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Notifications.Api.IntegrationEvents.Handlers;
using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Notifications.TestHelper;
using eDoxa.Notifications.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

using Moq;

using Xunit;

namespace eDoxa.Notifications.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserEmailChangedIntegrationEventHandlerTest : UnitTest
    {
        public UserEmailChangedIntegrationEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenUserEmailChangedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var userId = new UserId();
            var user = new User(userId, "test1@edoxa.gg");

            var mockUserService = new Mock<IUserService>();
            var mockLogger = new MockLogger<UserEmailChangedIntegrationEventHandler>();

            mockUserService.Setup(userService => userService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(true).Verifiable();

            mockUserService.Setup(userService => userService.FindUserAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

            mockUserService.Setup(userService => userService.UpdateUserAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(new DomainValidationResult())
                .Verifiable();

            mockUserService.Setup(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new UserEmailChangedIntegrationEventHandler(mockUserService.Object, mockLogger.Object);

            var integrationEvent = new UserEmailChangedIntegrationEvent
            {
                Email = new EmailDto
                {
                    Address = "test2@edoxa.gg",
                    Verified = true
                },
                UserId = userId
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockUserService.Verify(userService => userService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
            mockUserService.Verify(userService => userService.FindUserAsync(It.IsAny<UserId>()), Times.Once);
            mockUserService.Verify(userService => userService.UpdateUserAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
            mockUserService.Verify(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);
            mockLogger.Verify(Times.Never());
        }
    }
}
