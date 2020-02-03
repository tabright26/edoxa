// Filename: UserCreatedIntegrationEventHandlerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Enums;
using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Notifications.Api.IntegrationEvents.Handlers;
using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;
using eDoxa.Notifications.TestHelper;
using eDoxa.Notifications.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

using Moq;

using Xunit;

namespace eDoxa.Notifications.UnitTests.IntegrationEvents.Handlers
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
            var mockLogger = new MockLogger<UserCreatedIntegrationEventHandler>();

            TestMock.UserService.Setup(userService => userService.UserExistsAsync(It.IsAny<UserId>())).ReturnsAsync(false).Verifiable();

            TestMock.UserService.Setup(userService => userService.CreateUserAsync(It.IsAny<UserId>(), It.IsAny<string>()))
                .ReturnsAsync(new DomainValidationResult<User>())
                .Verifiable();

            TestMock.UserService.Setup(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<object>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new UserCreatedIntegrationEventHandler(TestMock.UserService.Object, mockLogger.Object);

            var integrationEvent = new UserCreatedIntegrationEvent
            {
                UserId = new UserId(),
                Email = new EmailDto
                {
                    Address = "test@email.com",
                    Verified = true
                },
                Country = EnumCountryIsoCode.CA,
                Ip = "10.10.10.10"
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            TestMock.UserService.Verify(userService => userService.UserExistsAsync(It.IsAny<UserId>()), Times.Once);
            TestMock.UserService.Verify(userService => userService.CreateUserAsync(It.IsAny<UserId>(), It.IsAny<string>()), Times.Once);
            TestMock.UserService.Verify(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<object>()), Times.Once);
            mockLogger.Verify(Times.Never());
        }
    }
}
