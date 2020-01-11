// Filename: UserGameCredentialAddedIntegrationEventHandlerTest.cs
// Date Created: 2019-12-18
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Games.Dtos;
using eDoxa.Grpc.Protos.Games.Enums;
using eDoxa.Grpc.Protos.Games.IntegrationEvents;
using eDoxa.Notifications.Api.IntegrationEvents.Handlers;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Notifications.TestHelper;
using eDoxa.Notifications.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using Moq;

using Xunit;

namespace eDoxa.Notifications.UnitTests.IntegrationEvents.Handlers
{
    public sealed class UserGameCredentialAddedIntegrationEventHandlerTest : UnitTest
    {
        public UserGameCredentialAddedIntegrationEventHandlerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public async Task HandleAsync_WhenUserGameCredentialAddedIntegrationEventIsValid_ShouldBeCompletedTask()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();

            mockUserService.Setup(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new UserGameCredentialAddedIntegrationEventHandler(mockUserService.Object);

            var integrationEvent = new UserGameCredentialAddedIntegrationEvent
            {
                Credential = new GameCredentialDto
                {
                    Game = GameDto.LeagueOfLegends,
                    PlayerId = new PlayerId(),
                    UserId = new UserId()
                }
            };

            // Act
            await handler.HandleAsync(integrationEvent);

            // Assert
            mockUserService.Verify(userService => userService.SendEmailAsync(It.IsAny<UserId>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
